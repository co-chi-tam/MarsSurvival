using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Ludiq.Reflection;

[DisallowMultipleComponent]
public class CFollowObjectEndPointComponent : CComponent {

	#region Internal class

	[System.Serializable]
	public class UnityEventFollowObject : UnityEvent<CFollowObjectComponent> {}

	#endregion

	#region Fields

	[Header("Configs")]
	[SerializeField]	protected int m_MaximumFollower = 1;
	protected List<CFollowObjectComponent> m_Followers;
	public List<CFollowObjectComponent> followers {
		get { return this.m_Followers; }
		set { this.m_Followers = value; }
	}

	[Header("Events")]
	public UnityEventFollowObject OnActive;
	public UnityEvent OnFree;

	#endregion

	#region Implementation Component

	protected override void Awake ()
	{
		base.Awake ();
		this.m_Followers = new List<CFollowObjectComponent> ();
	}

	protected override void LateUpdate ()
	{
		base.LateUpdate ();
		if (this.m_Followers.Count == 0) {
			this.OnFreePoint ();
		}
	}

	#endregion

	#region Main methods

	public virtual bool AddFollower(CFollowObjectComponent value) {
		if (this.m_Followers.Contains (value))
			return false;
		if (this.m_Followers.Count >= this.m_MaximumFollower) {
			this.m_Followers [0].target = null;
			this.m_Followers.RemoveAt (0);
		}
		this.m_Followers.Add (value);
		return true;
	}

	public virtual void OnActivePoint(CFollowObjectComponent value) {
		if (this.OnActive != null && value != null) {
			this.OnActive.Invoke (value);
		}
	}

	public virtual void OnFreePoint() {
		if (this.OnFree != null) {
			this.OnFree.Invoke ();
		}
	}

	#endregion

}
