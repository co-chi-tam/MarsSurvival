using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CAnimatorComponent : CComponent {

	#region Fields

	protected Dictionary<string, Action> m_AnimatorEvents;
	protected Animator m_Animator;

	#endregion

	#region Implementation Component

	protected override void Awake ()
	{
		base.Awake ();
		this.m_AnimatorEvents = new Dictionary<string, Action> ();
		this.m_Animator = this.GetComponent<Animator> ();
	}

	#endregion

	#region Main methods

	public virtual void RegisterCallback(string name, Action callback) {
		if (this.m_AnimatorEvents.ContainsKey (name) == false) {
			this.m_AnimatorEvents.Add (name, callback);
		} 
	} 

	public virtual void InvokeCallback(string name) {
		if (this.m_AnimatorEvents.ContainsKey (name)) {
			this.m_AnimatorEvents[name].Invoke();
		} 
	}

	public virtual void ApplyAnimation (string name, object param)
	{
		if (this.m_Animator == null)
			return;
		if (param is int) {
			this.m_Animator.SetInteger (name, (int)param);
		} else if (param is bool) {
			this.m_Animator.SetBool (name, (bool)param);
		} else if (param is float) {
			this.m_Animator.SetFloat (name, (float)param);
		} else if (param == null) {
			this.m_Animator.SetTrigger (name);
		}
	}

	#endregion

	#region Getter && Setter 

	public void SetTrigger(string value) {
		this.m_Animator.SetTrigger(value);
	}

	#endregion

}
