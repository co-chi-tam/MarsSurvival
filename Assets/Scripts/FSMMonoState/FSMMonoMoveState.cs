using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

public class FSMMonoMoveState : FSMMonoBaseState {

	#region Fields

	[Header("Configs")]
	[SerializeField]	protected Transform m_Target;
	[Header("Components")]
	[SerializeField]	protected CMoveComponent m_MoveComponent;

	protected CGameDataManager m_GameSetting;

	#endregion

	#region Implementation MonoBehaviour

	protected override void Start ()
	{
		base.Start ();
		this.m_GameSetting = CGameDataManager.GetInstance ();
	}

	#endregion

	#region Implementation IState

	public override void StartState ()
	{
		base.StartState ();
	}

	public override void UpdateState (float dt)
	{
		base.UpdateState (dt);
		if (this.m_Target == null)
			return;
		var movePoint = this.m_Target.position + this.m_GameSetting.movePoint;
		this.m_MoveComponent.targetPosition = movePoint;
		this.m_MoveComponent.SetupMove (dt);
	}

	#endregion

}
