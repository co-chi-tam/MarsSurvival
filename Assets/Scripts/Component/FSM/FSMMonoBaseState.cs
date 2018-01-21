﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Events.Utils;
using FSM;
using Ludiq.Reflection;

public class FSMMonoBaseState : MonoBehaviour, IState {

	#region Internal class

	[System.Serializable]
	public class UnityEventFloat: UnityEvent<float> {}

	#endregion

	#region Fields

	[Header("Configs")]
	[SerializeField]	protected GameObject m_Context;
	[SerializeField]	protected string m_FSMStateName;
	public string fsmStateName {
		get { return this.m_FSMStateName; }
		set { this.m_FSMStateName = value; }
	}

	[Header("Events")]
	public UnityEvent OnEnterState;
	public UnityEventFloat OnUpdateState;
	public UnityEvent OnEndState;

	#endregion

	#region Implementation MonoBehaviour

	protected virtual void Awake() {

	}

	protected virtual void Start() {
	
	}

	protected virtual void Update() {
		
	}

	protected virtual void LateUpdate() {
	
	}

	#endregion

	#region Implementation IState

	public virtual void StartState()
	{
		if (this.OnEnterState != null) {
			this.OnEnterState.Invoke ();
		}
	}

	public virtual void UpdateState(float dt)
	{
		if (this.OnUpdateState != null) {
			this.OnUpdateState.Invoke (dt);
		}
	}

	public virtual void ExitState()
	{
		if (this.OnEndState != null) {
			this.OnEndState.Invoke ();
		}
	}

	#endregion

	#region Context

	public virtual T GetContext<T>() where T : IContext {
		if (this.m_Context != null) {
			return this.m_Context.GetComponent<T> ();			
		}
		return default(T);
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
