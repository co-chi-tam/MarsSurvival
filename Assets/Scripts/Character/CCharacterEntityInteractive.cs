using System.Collections;
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
		if (this.m_ToolInteractiveEntity != null) {
			this.m_ToolInteractiveEntity.ApplyDamage (data.toolDamage);
			this.m_MoveComponent.Look (this.m_ToolInteractiveEntity.myTransform.position);
			this.m_DataComponent.UpdateDataPerInvoke ("UpdatePerAttack");
		}
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

	#endregion

}
