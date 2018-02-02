using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

public class FSMMonoAlienAttackState : FSMMonoBaseState {

	#region Fields

	[Header("Attack")]
	[SerializeField]	protected CGameEntity m_Owner;
	[SerializeField]	protected CEntityDetectComponent m_EntityDetect;
	[SerializeField]	protected CMoveComponent m_MoveComponent;

	#endregion

	#region Implementation MonoState

	public override void StartState ()
	{
		base.StartState ();
	}

	public override void UpdateState (float dt)
	{
		base.UpdateState (dt);
		var enemy = this.m_EntityDetect.currentEntity;
		if (enemy != null) {
			this.m_MoveComponent.targetPosition = enemy.myTransform.position;
			this.m_MoveComponent.SetupMove (dt);
			this.m_Owner.SetAnimation (this.m_MoveComponent.IsNearestTarget () ? 2 : 1);
		}
	}

	#endregion

}
