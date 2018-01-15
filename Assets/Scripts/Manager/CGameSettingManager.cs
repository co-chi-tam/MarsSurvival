using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleSingleton;

public class CGameSettingManager : CMonoSingleton<CGameSettingManager> {

	#region Fields

	[Header("Game configs")]
	[SerializeField]	protected Vector3 m_MovePoint;
	public Vector3 movePoint {
		get { return this.m_MovePoint; }
		set { this.m_MovePoint = value; }
	}

	#endregion

	#region Implementation CMonoSingleton

	protected override void Awake () {
		base.Awake ();
	}

	#endregion

}
