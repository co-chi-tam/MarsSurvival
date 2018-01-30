using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPowerGeneratorMachineData : CMachineData {
	
	#region Fields

	[Header("Power Generator Fields")]
	[SerializeField]	protected CEnergyObjectData m_Energy;
	[UpdateContinueAttribute]
	[Info (valueName = "Energy")]
	public CEnergyObjectData energy {
		get { return this.m_Energy; }
		set { this.m_Energy = value; }
	}

	#endregion

	#region Constructor

	public CPowerGeneratorMachineData (): base() {
		this.m_MachineName = "Energy generate machine";
	}

	#endregion

}
