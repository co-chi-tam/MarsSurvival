using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

public class FSMMonoApplyDamageTargetState : FSMMonoBaseState {

	protected float m_AttackSpeed = 2f;
	public override bool IsAfterShortTime {
		get {
			this.m_CountDownTimer += Time.deltaTime;
			return this.m_CountDownTimer >= this.m_AttackSpeed;
		}
	}

	public override void StartState ()
	{
		base.StartState ();
		this.m_AttackSpeed = 2f;
	}

}
