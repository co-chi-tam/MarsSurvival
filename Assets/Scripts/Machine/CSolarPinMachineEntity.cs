using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSolarPinMachineEntity : CMachineEntity {

	#region Fields

	public override bool isActive {
		get { return this.m_IsActive; }
		set { this.m_IsActive = value; }
	}

	[SerializeField]	protected bool m_IsStarted = false;
	public bool IsStarted {
		get { return this.m_IsStarted; }
		set { this.m_IsStarted = value; }
	}

	public override bool HaveEnergy {
		get { return base.HaveEnergy; }
		set { base.HaveEnergy = value; }
	}

	#endregion

}
