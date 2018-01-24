using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CCharacterEntity {

	#region Other Entity

	public virtual void InvokeOtherEntityEnergy() {
		if (this.m_OtherEntity != null) {
			var dataComponent = this.m_OtherEntity.GetGameComponent<CDataComponent> ();
			if (dataComponent != null) {
				dataComponent.UpdateDataPerInvoke ("AddEnergy");
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
		if (this.m_OtherEntity != null) {
			var followerComponent = this.m_OtherEntity.GetGameComponent<CFollowObjectComponent> ();
			if (followerComponent != null) {
				followerComponent.target = value ? this.transform : null;
			}
		}
	}

	#endregion

}
