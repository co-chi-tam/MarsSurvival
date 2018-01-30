using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleSingleton;
using Ludiq.Reflection;
using FSM;

public class CGameManager : CMonoSingleton<CGameManager>, IContext {

	#region Fields

	[Header("Fields")]
	[SerializeField]	protected bool m_IsStarted = true;
	public bool isStarted {
		get { return this.m_IsStarted; }
		set { this.m_IsStarted = value; }
	}

	[SerializeField]	protected bool m_IsCharacterDeath = false;
	public bool isCharacterDeath {
		get { return this.m_IsCharacterDeath; }
		set { this.m_IsCharacterDeath = value; }
	}

	protected Dictionary<string, Action> m_GameEvents;

	#endregion

	#region Implementation CMonoSingleton

	protected override void Awake () {
		base.Awake ();
		this.m_GameEvents = new Dictionary<string, Action> ();
	}

	#endregion

	#region Events

	public virtual void RegisterCallback(string name, Action callback) {
		if (this.m_GameEvents.ContainsKey (name) == false) {
			this.m_GameEvents.Add (name, callback);
		} 
	} 

	public virtual void InvokeCallback(string name) {
		if (this.m_GameEvents.ContainsKey (name)) {
			this.m_GameEvents[name].Invoke();
		} 
	}

	#endregion

}
