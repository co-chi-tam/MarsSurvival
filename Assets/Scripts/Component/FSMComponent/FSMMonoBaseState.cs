using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

public class FSMMonoBaseState : MonoBehaviour, IState {

	#region Fields

	[SerializeField]	protected string m_FSMStateName = "EmptyName";
	public string fsmStateName {
		get { return this.m_FSMStateName; }
		set { this.m_FSMStateName = value; }
	}

	#endregion

	#region Implementation IState

	public virtual void StartState()
	{

	}

	public virtual void UpdateState(float dt)
	{

	}

	public virtual void ExitState()
	{

	}

	#endregion

	#region Main methods

	public virtual string GetName() {
		return this.m_FSMStateName;
	}

	public override string ToString ()
	{
		return this.GetName();
	}

	#endregion

}
