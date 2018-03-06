using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class CAlienData : CGameEntityData {

	#region Fields

	[Header("Alien Fields")]
	[SerializeField]	protected float m_AttackDamage = 1f;
	public float attackDamage {
		get { return this.m_AttackDamage; }
		set { this.m_AttackDamage = value; }
	}
	[SerializeField]	protected float m_AttackSpeed = 2f;
	public float attackSpeed {
		get { return this.m_AttackSpeed; }
		set { this.m_AttackSpeed = value; }
	}

	// HEALTH
	[SerializeField]	protected float m_HealthPoint = 100f;
	[Info (valueName = "Health point", valueMin = 0, valueMax = 9999f)]
	public float healthPoint {
		get { return this.m_HealthPoint; }
		set { this.m_HealthPoint = value < 0f ? 0f : value > this.m_MaxHealthPoint ? this.m_MaxHealthPoint : value; }
	}
	[SerializeField]	protected float m_MaxHealthPoint = 100f;
	public float maxHealthPoint {
		get { return this.m_MaxHealthPoint; }
		set { this.m_MaxHealthPoint = value; }
	}

	#endregion

	#region Constructor

	public CAlienData (): base() {
		
	}

	#endregion

	#region Override

	#endregion

}
