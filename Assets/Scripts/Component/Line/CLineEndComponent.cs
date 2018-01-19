﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Events.Utils;

public class CLineEndComponent : CComponent {

	#region Internal class

	[System.Serializable]
	public class UnityEventGameObject: UnityEvent<GameObject> {}

	#endregion

	#region Fields

	[Header("Events")]
	public UnityEventGameObject OnConnected;
	public UnityEvent OnFree;

	protected bool m_IsConnected = false;
	protected GameObject m_ConnectedGameObject = null;

	#endregion

	#region Implementation CComponent

	protected override void LateUpdate ()
	{
		base.LateUpdate ();
		if (this.m_IsActive == false)
			return;
		if (this.m_IsConnected) {
			if (this.OnConnected != null) {
				this.OnConnected.Invoke (this.m_ConnectedGameObject);
			}
			this.m_IsConnected = false;
		} else {
			if (this.OnFree != null) {
				this.OnFree.Invoke ();
			}
			this.m_ConnectedGameObject = null;
		}
	}

	#endregion

	#region Main methods

	public virtual bool ConnectedLine(GameObject value) {
		this.m_ConnectedGameObject = value;
		this.m_IsConnected = true;
		return this.m_IsConnected;
	}

	#endregion

}
