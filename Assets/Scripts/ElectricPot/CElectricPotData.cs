using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CElectricPotData : CMachineData {

	#region Fields

	[Header("Electric Pot Fields")]
	[SerializeField]	protected CEnergyObjectData m_Energy;
	[UpdateContinueAttribute]
	[Info (valueName = "Energy")]
	public CEnergyObjectData energy {
		get { return this.m_Energy; }
		set { this.m_Energy = value; }
	}

	#endregion

	#region Constructor

	public CElectricPotData (): base() {
		this.m_MachineName = "Electric pot machine";
	}

	#endregion

}
