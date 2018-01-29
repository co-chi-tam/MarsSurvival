using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CCharacterEntity {

	#region Fields

	[SerializeField]	protected CEntity m_OtherEntity;
	public CEntity otherEntity {
		get { return this.m_OtherEntity; }
		set { 
			this.m_OtherEntity = value;  
		}
	}
	public Transform otherEntityTransform {
		get { 
			if (this.m_OtherEntity == null)
				return null;
			return this.m_OtherEntity.transform; 
		}
	}

	#endregion

	#region Other Entity

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
		obj.transform.position = this.m_Transform.position;
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

	protected virtual void OnMachineConsumeItem () {
		var machine = this.m_OtherEntity as CMachineEntity;
		var items = machine.itemsPerCharge;
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
		// USE ITEMS
		if (isEnoughtItem) {
			for (int i = 0; i < items.Length; i++) {
				var mat = items [i];
				this.m_InventoryComponent.UseItem (
					mat.itemAmount,
					mat.itemData
				);
			}
			machine.AddEnergy ();
		}
	}

	#endregion

}
