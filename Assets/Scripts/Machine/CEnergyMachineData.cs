using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnergyMachineData : CMachineData {

	#region Fields

	[Header("Energy Fields")]
	[SerializeField]	protected float m_EnergyPoint = 100f;
	[Info(valueName = "Energy point", valueMin = 0f, valueMax = 100f)]
	[UpdateValuePerInvoke(updateName = "AddEnergy", updateMethod = "Increase", updateValuePerInvoke = 10f)]
	[UpdateValuePerInvoke(updateName = "UseEnergy", updateMethod = "Decrease", updateValuePerInvoke = 0.5f)]
	public float energyPoint {
		get { return this.m_EnergyPoint; }
		set { this.m_EnergyPoint = value < 0f ? 0f : value > this.m_MaxEnergyPoint ? this.m_MaxEnergyPoint : value; }
	}

	[SerializeField]	protected float m_MaxEnergyPoint = 100f;
	[Info(valueName = "Max energy point", valueMin = 0f, valueMax = 100f)]
	public float maxEnergyPoint {
		get { return this.m_MaxEnergyPoint; }
		set { this.m_MaxEnergyPoint = value; }
	}

	[SerializeField]	protected CAmountItem[] m_ItemsPerCharge;
	public CAmountItem[] itemsPerCharge {
		get { return this.m_ItemsPerCharge; }
		set { this.m_ItemsPerCharge = value; }
	}

	#endregion

	#region Constructor

	public CEnergyMachineData (): base() {
		this.m_MachineName = "Energy machine";
		this.m_EnergyPoint = 100f;
		this.m_MaxEnergyPoint = 100f;
	}

	#endregion

}
