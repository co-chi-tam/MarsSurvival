using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

public class CFSMComponent : CComponent {

	#region Fields

	[Header("FSM Data")]
	[SerializeField]	protected TextAsset m_FSMTextAsset;
#if UNITY_EDITOR
	[SerializeField]	protected string m_CurrentStateName;
#endif

	protected FSMManager m_FSMManager;

	#endregion

	#region Implementation Component

	protected override void Awake ()
	{
		base.Awake ();
		this.m_FSMManager = new FSMManager ();
//		this.m_FSMManager.LoadFSM (this.m_FSMTextAsset.text);
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

	public override void UpdateFromOwner (float dt)
	{
		base.UpdateFromOwner (dt);

	}

	#endregion

	#region State

	public virtual void ApplyState(IState fsmState) {
		this.m_FSMManager.RegisterState (fsmState.GetName(), fsmState);
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
