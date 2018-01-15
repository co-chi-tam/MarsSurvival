﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UICustomize;
using FSM;

public class FSMMonoMoveState : FSMMonoBaseState {

	#region Fields

	[SerializeField]	protected Transform m_Target;
	[SerializeField]	protected CMoveComponent m_MoveComponent;
	[SerializeField]	protected CAnimatorComponent m_Animator;

	protected CGameSettingManager m_GameSetting;

	#endregion

	#region Implementation MonoBehaviour

	protected override void Start ()
	{
		base.Start ();
		this.m_GameSetting = CGameSettingManager.GetInstance ();
	}

	#endregion

	#region Implementation IState

	public override void StartState ()
	{
		base.StartState ();
		this.m_Animator.ApplyAnimation ("AnimParam", 1);
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
