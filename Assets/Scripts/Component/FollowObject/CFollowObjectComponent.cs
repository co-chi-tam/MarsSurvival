﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CMoveComponent))]
public class CFollowObjectComponent : CComponent {

	#region Fields

	[SerializeField]	protected Transform m_Target;
	public Transform target {
		get { return this.m_Target; }
		set { 
			this.m_Target = value; 
			if (value != null) {
				this.m_EndPointComponent = value.GetComponent<CFollowObjectEndPointComponent> ();
			}
		}
	}
	[SerializeField]	protected float m_MinDistance = 1f;
	public float minDistance {
		get { return this.m_MinDistance; }
		set { this.m_MinDistance = value; }
	}

	protected CMoveComponent m_MoveComponent;
	protected CFollowObjectEndPointComponent m_EndPointComponent;

	#endregion

	#region Implementation Component 

	protected override void Awake ()
	{
		base.Awake ();
		this.m_MoveComponent = this.GetComponent<CMoveComponent> ();
		this.m_MoveComponent.minDistance = this.m_MinDistance;
	}

	protected override void Update ()
	{
		base.Update ();
		if (this.m_IsActive
		    && this.m_Target != null) {
			this.m_MoveComponent.targetPosition = this.m_Target.position;
			if (this.m_EndPointComponent != null) {
				this.m_EndPointComponent.OnActivePoint (this);
			}
		} else {
			if (this.m_EndPointComponent != null) {
				this.m_EndPointComponent.OnFreePoint ();
			}
		}
	}

	#endregion

}