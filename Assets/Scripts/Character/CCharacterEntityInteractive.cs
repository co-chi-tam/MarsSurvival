﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CCharacterEntity {

	#region Fields

	[Header("Interactive")]
	[SerializeField]	protected CGameEntity m_OtherEntity;
	public CGameEntity otherEntity {
		get { return this.m_OtherEntity; }
		set { this.m_OtherEntity = value; }
	}
	public Transform otherEntityTransform {
		get { 
			if (this.m_OtherEntity == null)
				return null;
			return this.m_OtherEntity.transform; 
		}
	}
	[SerializeField]	protected CGameEntity m_ToolInteractiveEntity;
	public CGameEntity toolInteractiveEntity {
		get { return this.m_ToolInteractiveEntity; }
		set { this.m_ToolInteractiveEntity = value; }
	}

	#endregion

	#region Entity

	public virtual void InvokePickItem() {
		this.m_InventoryComponent.PickItem ();
	}

	public virtual void InvokeUseTool() {
		this.m_StoreToolComponent.UseTool ();
		this.IsAttack = this.m_StoreToolComponent.haveTool;
	}

	#endregion

	#region Tool

	public virtual void InvokeToolEmpty(CToolData data) {
		
	}

	public virtual void InvokeAttack(CToolData data) {
		if (this.m_ToolInteractiveEntity != null 
			&& this.m_Data.energyPoint >= data.energyConsume) {
			this.m_ToolInteractiveEntity.ApplyDamage (data.toolDamage);
			this.m_MoveComponent.Look (this.m_ToolInteractiveEntity.myTransform.position);
			this.m_Data.energyPoint -= data.energyConsume;
		}
	}

	public virtual void SetEquipTool(string toolName) {
		this.m_StoreToolComponent.LoadTool (toolName);
	}
		
	#endregion

	#region Other Entity

	public virtual void InvokeActiveOtherEntity() {
		if (this.m_OtherEntity != null) {
			if (this.m_OtherEntity is CMachineEntity) {
				this.OnActiveMachine ();
			}
		}
	}

	public virtual void InvokeOtherEntityEnergy() {
		if (this.m_OtherEntity != null) {
			if (this.m_OtherEntity is CMachineEntity) {
				this.OnMachineConsumeItem ();
			}
		}
	}

	public virtual void InvokeOtherEntityCollectItems() {
		if (this.m_OtherEntity != null) {
			if (this.m_OtherEntity is CMachineEntity) {
				this.OnMachineCollectItems ();
			}
		}
	}

	public virtual void SetStartMachine(bool value) {
		if (this.m_OtherEntity != null) {
			var machineEntity = this.m_OtherEntity as CMachineEntity;
			if (machineEntity != null) {
				machineEntity.IsStarted = value;
			}
		}
	}

	public virtual void SetOtherEntityFollowMe(bool value) {
		// SET UP ENTITY
		if (this.m_OtherEntity != null) {
			var followerComponent = this.m_OtherEntity.GetGameComponent<CFollowObjectComponent> ();
			if (followerComponent != null) {
				followerComponent.target = value ? this.m_Transform : null;
				this.m_FollowEndPointComponent.Clear ();
			}
		}
	}

	public virtual void HaveSpawnObject(GameObject obj) {
		var randomVector = Random.insideUnitCircle;
		var randomPosition = new Vector3 (
			this.m_Transform.position.x + randomVector.x * 2f,
			this.m_Transform.position.y,
			this.m_Transform.position.z + randomVector.y * 2f
		);
		obj.transform.position = randomPosition;
	}

	#endregion

	#region Machine Entity

	protected virtual void OnMachineCollectItems () {
		var machine = this.m_OtherEntity as CMachineEntity;
		var items = machine.itemCollects;
		// COLLECT TIMES
		var collectTimes = Mathf.FloorToInt (machine.collectPercent);
		if (collectTimes > 0) {
			for (int i = 0; i < items.Length; i++) {
				var mat = items [i];
				this.m_InventoryComponent.PickItem (mat.itemAmount * collectTimes, mat.itemData);
			}
			// RESET COLLECT TIME INTERVAL 
			machine.CollectItems ();
		}
	}

	protected virtual bool IsEnoughtItems(CAmountItem[] items) {
		if (items == null)
			return false;
		// CHECK ENOUGHT ITEMS
		var isEnoughtItem = true;
		for (int i = 0; i < items.Length; i++) {
			var mat = items [i];
			if (this.m_InventoryComponent.CheckAmountItem (
				mat.itemAmount,
				mat.itemData
			) == false) {
				isEnoughtItem = false;
				break;
			}
		}
		return isEnoughtItem;
	}

	protected virtual bool IsConsumeItems(CAmountItem[] items) {
		if (items == null)
			return false;
		// CONSUME ITEMS
		for (int i = 0; i < items.Length; i++) {
			var mat = items [i];
			this.m_InventoryComponent.UseItem (
				mat.itemAmount,
				mat.itemData
			);
		}
		return true;
	}

	protected virtual void OnMachineConsumeItem () {
		var machine = this.m_OtherEntity as CMachineEntity;
		var items = machine.itemsPerCharge;
		// USE ITEMS
		if (this.IsEnoughtItems (items)) {
			if (this.IsConsumeItems(items)) {
				machine.AddEnergy ();
			}
		}
	}

	protected virtual void OnActiveMachine () {
		var machine = this.m_OtherEntity as CMachineEntity;
		var items = machine.activeWithItems;
		// USE ITEMS
		if (this.IsEnoughtItems (items)) {
			if (this.IsConsumeItems(items)) {
				machine.IsActive = true;
			}
		}
	}	

	#endregion

	#region Mission

	public virtual string IsObtainItem(string itemName) {
		return this.m_InventoryComponent.CheckAmountItem (1, itemName) ? itemName : "FALSE";
	}

	public virtual string IsStartOneMachine (string methodName) {
		if (this.m_OtherEntity == null)
			return "NULL";
		var machine = this.m_OtherEntity as CMachineEntity;
		if (machine == null)
			return "NULL";
		return machine.IsStarted ? "Start" : "NULL";
	}

	public virtual string IsPullOneMachine (string methodName) {
		if (this.m_FollowEndPointComponent == null)
			return "NULL";
		return this.m_FollowEndPointComponent.followers.Count > 0 ? "Pull" : "NULL";
	}

	public virtual void ReachReward (CMissionData mission) {
		var rewards = mission.missionRewards;
		this.PickItems (rewards);
	}

	public virtual void LoadNextMission() {
		var index = this.m_Data.missionIndex + 1;
		if (index >= this.m_MissionListComponent.missionList.Length) {
			this.m_MissionComponent.data = null;
		} else {
			this.m_MissionComponent.data = this.m_MissionListComponent.missionList [index];
		}
		this.m_Data.missionIndex = index;
	}

	#endregion

	#region Inventory

	public virtual void PickItems (CAmountItem[] items) {
		for (int i = 0; i < items.Length; i++) {
			this.PickItem (items [i]);
		}
	}

	public virtual void PickItem (CAmountItem item) {
		this.m_InventoryComponent.PickItem (item.itemAmount, item.itemData);
	}

	#endregion

	#region Abs

	public virtual void InvokeOpenCarrotAbs() {
		if (this.m_Data == null)
			return;
		if (this.m_Data.rewardAbs.Length > 0) {
			var rewardAbs = this.m_Data.rewardAbs [0];
			this.PickItem (rewardAbs);
		}
	}

	#endregion

}
