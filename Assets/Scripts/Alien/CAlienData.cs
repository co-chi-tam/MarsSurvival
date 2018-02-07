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

	#endregion

	#region Constructor

	public CAlienData (): base() {
		
	}

	#endregion

	#region Override

	#endregion

}
