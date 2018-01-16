using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UICustomize;
using FSM;

public class FSMMonoIdleState : FSMMonoBaseState {

	#region Fields

	[Header("Components")]
	[SerializeField]	protected CAnimatorComponent m_Animator;

	#endregion

	#region Implementation IState

	public override void StartState ()
	{
		base.StartState ();
		this.m_Animator.ApplyAnimation ("AnimParam", 0);
	}

	#endregion

}
