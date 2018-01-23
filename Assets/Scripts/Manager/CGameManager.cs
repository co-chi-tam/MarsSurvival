using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleSingleton;
using Ludiq.Reflection;

public class CGameManager : CMonoSingleton<CGameManager> {

	#region Fields

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
