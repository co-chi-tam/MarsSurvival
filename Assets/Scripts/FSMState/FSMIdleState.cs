using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

public class FSMIdleState : FSMBaseState {

	public FSMIdleState (IContext context) : base (context) {
		this.m_FSMStateName = "FSMIdleState";
	}

}
