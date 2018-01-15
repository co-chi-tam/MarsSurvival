using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

public class CFSMComponent : CComponent {

	#region Fields

	[Header("FSM Configs")]
	[SerializeField]	protected TextAsset m_FSMTextAsset;
#if UNITY_EDITOR
	[SerializeField]	protected string m_CurrentStateName;
#endif
	[Header("States")]
	[SerializeField]	protected FSMMonoBaseState[] m_CurrentStates;
	public FSMMonoBaseState[] currentStates {
		get { return this.m_CurrentStates; }
		set { this.m_CurrentStates = value; }
	}
	[Header("Conditions")]
	[SerializeField]	protected FSMMonoBaseCondition[] m_CurrentConditions;
	public FSMMonoBaseCondition[] currentConditions {
		get { return this.m_CurrentConditions; }
		set { this.m_CurrentConditions = value; }
	}

	protected FSMManager m_FSMManager;

	#endregion

	#region Implementation Component

	protected override void Awake ()
	{
		base.Awake ();
		this.InitState ();
	}

	protected override void Update ()
	{
		base.Update ();
		if (this.m_IsActive == false)
			return;
		this.m_FSMManager.UpdateState (Time.deltaTime);
#if UNITY_EDITOR
		this.m_CurrentStateName = this.m_FSMManager.currentStateName;
#endif
	}

	#endregion

	#region Main methods

	protected virtual void InitState() {
		this.m_FSMManager = new FSMManager ();
		// State
		for (int i = 0; i < this.m_CurrentStates.Length; i++) {
			var state = this.m_CurrentStates [i];
			this.ApplyState (state.GetName (), state); 
		}
		// Condition
		for (int i = 0; i < this.m_CurrentConditions.Length; i++) {
			var condition = this.m_CurrentConditions [i];
			this.ApplyCondition (condition.conditionName, condition.conditionVariable.Get<bool>);
		}
		// Load FSM
		this.m_FSMManager.LoadFSM (this.m_FSMTextAsset.text);
	}

	public override void UpdateFromOwner (float dt)
	{
		base.UpdateFromOwner (dt);
	}

	#endregion

 	#region State

	public virtual void ApplyState(IState fsmState) {
		this.m_FSMManager.RegisterState (fsmState.GetName(), fsmState);
	}

	public virtual void ApplyState(string name, IState fsmState) {
		this.m_FSMManager.RegisterState (name, fsmState);
	}

	public virtual void ApplyStates(Dictionary<string, IState> fsmStates) {
		foreach (var item in fsmStates) {
			var state = item.Value;
			this.m_FSMManager.RegisterState (state.GetName(), state);
		}
	}

	#endregion

	#region Conditions

	public virtual void ApplyCondition(string name, Func<bool> condition) {
		this.m_FSMManager.RegisterCondition (name, condition);
	}

	public virtual void ApplyConditions(Dictionary<string, Func<bool>> fsmConditions) {
		foreach (var item in fsmConditions) {
			this.m_FSMManager.RegisterCondition (item.Key, item.Value);
		}
	}

	#endregion

}
