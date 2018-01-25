using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPowerGeneratorMachineData : CEnergyMachineData {
	
	#region Fields

	#endregion

	#region Constructor

	public CPowerGeneratorMachineData (): base() {
		this.m_MachineName = "Energy generate machine";
		this.m_EnergyPoint = 100f;
		this.m_MaxEnergyPoint = 100f;
	}

	#endregion

}
