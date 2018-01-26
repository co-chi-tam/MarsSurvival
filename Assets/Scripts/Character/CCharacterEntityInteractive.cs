using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CCharacterEntity {

	#region Other Entity

	public virtual void ResetOtherEntity(CEntity value) {
		// REMOVE ALL FOLLOWER
		var followerComponent = value.GetGameComponent<CFollowObjectComponent> ();
		if (followerComponent != null) {
			// TODO
			Debug.Log ("AAAAAAAA");
		}
	}

	public virtual void InvokeOtherEntityEnergy() {
		if (this.m_OtherEntity != null) {
			if (this.m_OtherEntity is CMachineEntity) {
				this.OnMachineComsumeItem ();
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
		// RESET ENTITY
		if (this.m_FollowerEntity != null) {
			var followerComponent = this.m_FollowerEntity.GetGameComponent<CFollowObjectComponent> ();
			if (followerComponent != null) {
				followerComponent.target = null;
			}
		}
		// SET UP ENTITY
		if (this.m_OtherEntity != null) {
			var followerComponent = this.m_OtherEntity.GetGameComponent<CFollowObjectComponent> ();
			if (followerComponent != null) {
				this.m_FollowerEntity = this.m_OtherEntity;
				followerComponent.target = value ? this.m_Transform : null;
			}
		}
	}

	public virtual void HaveSpawnObject(GameObject obj) {
		obj.transform.position = this.m_Transform.position;
	}

	#endregion

	#region Machine Entity

	protected virtual void OnMachineComsumeItem() {
		var machine = this.m_OtherEntity as CMachineEntity;
		var materials = machine.materialsPerCharge;
		// CHECK ENOUGHT MATERIAL
		var isEnoughtMaterial = true;
		for (int i = 0; i < materials.Length; i++) {
			var mat = materials [i];
			if (this.m_InventoryComponent.CheckAmountItem (
				mat.materialAmount,
				mat.materialData
			) == false) {
				isEnoughtMaterial = false;
				break;
			}
		}
		// USE ITEM
		if (isEnoughtMaterial) {
			for (int i = 0; i < materials.Length; i++) {
				var mat = materials [i];
				this.m_InventoryComponent.UseItem (
					mat.materialAmount,
					mat.materialData
				);
			}
			machine.AddEnergy ();
		}
	}

	#endregion

}
