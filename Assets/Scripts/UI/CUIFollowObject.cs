using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Ludiq.Reflection;

public class CUIFollowObject : MonoBehaviour {

	#region Fields

	[Header ("Configs")]
	[SerializeField]	protected bool m_IsActive = true;
	public bool isActive {
		get { return this.m_IsActive; }
		set { this.m_IsActive = value; }
	}
	[SerializeField]	protected Transform m_Follow;
	public Transform follow {
		get { return this.m_Follow; }
		set { this.m_Follow = value; }
	}
	[SerializeField]	protected Vector3 m_OffsetPosition;

	[Header("Events")]
	public UnityEvent OnFollow;
	public UnityEvent OnFree;

	protected Transform m_Transform;

	#endregion

	#region Implementation MonoBehaviour

	protected virtual void Awake() {
		this.m_Transform = this.transform;
	}

	protected virtual void LateUpdate() {
		if (this.m_IsActive == false)
			return;
		if (this.m_Follow == null) {
			if (this.OnFree != null) {
				this.OnFree.Invoke ();
			}
		} else {
			var position = Camera.main.WorldToScreenPoint (this.m_Follow.transform.position);
			this.m_Transform.position = position + this.m_OffsetPosition;
			if (this.OnFollow != null) {
				this.OnFollow.Invoke ();
			}
		}
	}

	#endregion

}
