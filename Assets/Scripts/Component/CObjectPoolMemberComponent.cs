using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Events.Utils;
using Ludiq.Reflection;

[DisallowMultipleComponent]
public class CObjectPoolMemberComponent : CComponent {

	#region Internal Class

	[System.Serializable]
	public class UnityEventGameObject: UnityEvent<GameObject> {}

	#endregion

	#region Fields

	[Header("Configs")]
	[SerializeField]	protected string m_MemberName = "Empty";
	public string memberName {
		get { return this.m_MemberName; }
		set { this.m_MemberName = value; }
	}

	[Header("Events")]
	public UnityEvent OnSet;
	public UnityEventGameObject OnGet;

	protected CObjectPoolManager m_ObjectPoolManager;

	#endregion

	#region Implementation Component

	protected override void Start ()
	{
		base.Start ();
		this.m_ObjectPoolManager = CObjectPoolManager.GetInstance ();
	}

	#endregion

	#region Main methods

	public virtual void Set() {
		this.m_ObjectPoolManager.Set (this.m_MemberName, this);
		if (this.OnSet != null) {
			this.OnSet.Invoke ();
		}
	}

	public virtual CObjectPoolMemberComponent Get(string name) {
		var member = this.m_ObjectPoolManager.Get (name);
		if (member != null) {
			member.StartMember ();
		}
		return member;
	}

	public virtual void StartMember() {
		if (this.OnGet != null) {
			this.OnGet.Invoke (this.gameObject);
		}
	}

	#endregion

}
