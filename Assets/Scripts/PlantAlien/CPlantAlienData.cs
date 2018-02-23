using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlantAlienData : CEnviromentData {

	[Header("Plant Alien Fields")]
	[SerializeField]	protected float m_CurrentDamage = 0;
	public override float currentDamage {
		get { return this.m_CurrentDamage; }
		set { this.m_CurrentDamage = value; }
	}
	[SerializeField]	protected float m_MaximumDamage = 1;
	public override float maximumDamage {
		get { return this.m_MaximumDamage; }
		set { this.m_MaximumDamage = value;  }
	}
	[SerializeField]	protected CAmountItem[] m_ResourceContains;
	public override CAmountItem[] resourceContains {
		get { return this.m_ResourceContains; }
		set { this.m_ResourceContains = value; }
	}

}
