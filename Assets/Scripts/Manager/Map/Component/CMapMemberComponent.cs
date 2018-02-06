﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Ludiq.Reflection;

[DisallowMultipleComponent]
public class CMapMemberComponent : CComponent {

	#region Fields

	[Header("Configs")]
	[SerializeField]	protected bool m_IsCenterMap = false;
	protected Vector3 m_CenterPosition;
	public Vector3 centerPosition {
		get { return this.m_CenterPosition; }
		set { this.m_CenterPosition = value; }
	}
 
	protected CMapManager m_MapManager;

	#endregion

	#region Implemetation MonoBehaviour

	protected override void Start ()
	{
		base.Start ();
		this.m_MapManager = CMapManager.GetInstance ();
		if (this.m_IsCenterMap) {
			this.m_MapManager.target = this.transform;
		}
	}

	protected override void LateUpdate() {
		base.LateUpdate ();
		if (this.m_MapManager == null)
			return;
		this.m_CenterPosition = this.m_MapManager.centerPosition;
	}

	#endregion

	#region Main methods

	public virtual void ApplyRandomPosition(float radius) {
		this.transform.position = this.GetRandomPosition (radius);
	}

	public virtual Vector3 GetRandomPosition(float radius) {
		return this.m_MapManager.GetRandomPosition (radius);
	}

	#endregion

}
