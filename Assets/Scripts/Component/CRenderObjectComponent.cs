using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Events.Utils;
using Ludiq.Reflection;

public class CRenderObjectComponent : CComponent {

	#region Fields

	[Header("Configs")]
	[SerializeField]	protected bool m_IsVisible = false;
	public bool isVisible {
		get { return this.m_IsVisible; }
	}
	public bool isInvisible {
		get { return !this.m_IsVisible; }
	}

	[Header("Events")]
	public UnityEvent OnVisible;
	public UnityEvent OnInvisible;

	#endregion

	#region Implementation Component

	protected virtual void OnBecameVisible() {
		this.m_IsVisible = true;
		if (this.m_IsActive) {
			if (this.OnVisible != null) {
				this.OnVisible.Invoke ();
			}
		}
	}

	protected virtual void OnBecameInvisible() {
		this.m_IsVisible = false;
		if (this.m_IsActive) {
			if (this.OnInvisible != null) {
				this.OnInvisible.Invoke ();
			}
		}
	}

	#endregion

}
