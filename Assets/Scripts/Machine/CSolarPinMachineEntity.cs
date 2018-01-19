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

	public virtual bool IsFullEnergy {
		get {
			return this.m_Data.powerPoint >= this.m_Data.maxPowerPoint;
		}
	}

	[SerializeField]	protected bool m_IsCharging = false;
	public bool IsCharging {
		get { return this.m_IsCharging; }
		set { this.m_IsCharging = value; }
	}

	[SerializeField]	protected bool m_IsStarted = false;
	public bool IsStarted {
		get { return this.m_IsStarted; }
		set { this.m_IsStarted = value; }
	}

	#endregion

}
