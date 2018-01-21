using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

public class FSMMonoMoveState : FSMMonoBaseState {

	#region Fields

	[Header("Configs")]
	[SerializeField]	protected Transform m_Target;
	[SerializeField]	protected CCharacterEntity m_CharacterData;
	[SerializeField]	protected CMoveComponent m_MoveComponent;

	#endregion

	#region Implementation MonoBehaviour

	protected override void Start ()
	{
		base.Start ();
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
		var movePoint = this.m_Target.position + this.m_CharacterData.deltaMovePoint;
		this.m_MoveComponent.targetPosition = movePoint;
		this.m_MoveComponent.SetupMove (dt);
	}

	#endregion

}
