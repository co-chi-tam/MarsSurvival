using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSpaceshipData : CEntityData {

	[Header("Energy Fields")]
	[SerializeField]	protected float m_EnergyPoint = 50f;
	[Info(valueName = "Energy point", valueMin = 0f, valueMax = 999f)]
	[UpdateValuePerSecond (updateMethod = "Decrease", updateValuePerSecond = 1.5f)]
	public virtual float energyPoint {
		get { return this.m_EnergyPoint; }
		set { this.m_EnergyPoint = value < 0f ? 0f : value > 999f ? 999f : value; }
	}
	[SerializeField]	protected float m_MaxEnergyPoint = 100f;
	[Info(valueName = "Max energy point", valueMin = 0f, valueMax = 999f)]
	public virtual float maxEnergyPoint {
		get { return this.m_MaxEnergyPoint; }
		set { this.m_MaxEnergyPoint = value; }
	}

}
