﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPowerGeneratorMachineData : CMachineData {
	
	#region Fields

	[Header("Fields")]
	[SerializeField]	protected float m_PowerPoint = 100f;
	[Info(valueName = "Energy point", valueMin = 0f, valueMax = 100f)]
	[UpdateValuePerInvoke(updateName = "AddEnergy", updateMethod = "Increase", updateValuePerInvoke = 20f)]
	[UpdateValuePerInvoke(updateName = "UseEnergy", updateMethod = "Decrease", updateValuePerInvoke = 0.5f)]
	public float energyPoint {
		get { return this.m_PowerPoint; }
		set { this.m_PowerPoint = value < 0f ? 0f : value > this.m_MaxPowerPoint ? this.m_MaxPowerPoint : value; }
	}

	[SerializeField]	protected float m_MaxPowerPoint = 100f;
	[Info(valueName = "Max energy point", valueMin = 0f, valueMax = 100f)]
	public float maxEnergyPoint {
		get { return this.m_MaxPowerPoint; }
		set { this.m_MaxPowerPoint = value; }
	}

	#endregion

	#region Constructor

	public CPowerGeneratorMachineData (): base() {
		this.m_MachineName = "Power generate machine";
		this.m_PowerPoint = 100f;
		this.m_MaxPowerPoint = 100f;
	}

	#endregion

	#region Override

	public override string ToString ()
	{
		return string.Format ("[CPowerGeneratorMachineData: energyPoint={0}, maxEnergyPoint={1}]", energyPoint, maxEnergyPoint);
	}

	#endregion

}
