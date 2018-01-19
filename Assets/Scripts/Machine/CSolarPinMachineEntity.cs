using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSolarPinMachineEntity : CMachineEntity {

	#region Fields

	public override bool isActive {
		get { return this.m_IsActive; }
		set { this.m_IsActive = value; }
	}

	public override bool HaveEnergy {
		get {
			return base.HaveEnergy;
		}
	}

	#endregion

}
