﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CPhysicDetectComponent : CComponent {

	#region Fields

	[Header("Data")]
	[SerializeField]	protected Transform m_DetectTransform;
	public Transform detectTransform {
		get { return this.m_DetectTransform; }
		set { this.m_DetectTransform = value; }
	}
	[SerializeField]	protected float m_DetectRadius = 10f;
	public float detectRadius {
		get { return this.m_DetectRadius; }
		set { this.m_DetectRadius = value; }
	}

	[Header("Detect")]
	[SerializeField]	protected LayerMask m_DetectLayerMask = 0;
	[SerializeField]	protected int m_ColliderCount;
	public int colliderCount {
		get { return this.m_ColliderCount; }
		protected set { this.m_ColliderCount = value; }
	}
	protected int m_PreviousCount = 0;
	[SerializeField]	protected int m_MaximumDetect = 20;
	public int maximumDetect {
		get { return this.m_MaximumDetect; }
		set { 
			this.m_MaximumDetect = value; 
			this.m_SampleColliders = new Collider[value];
		}
	}
	[SerializeField]	protected Collider[] m_SampleColliders;
	public Collider[] sampleColliders {
		get { return this.m_SampleColliders; }
		protected set { 
			this.m_SampleColliders = value; 
			this.m_MaximumDetect = value.Length; 
		}
	}

	[Header("Event")]
	public UnityEvent OnFree;
	public UnityEvent OnDetected;
	public UnityEvent OnChanged;

#if UNITY_EDITOR
	public Color m_SphereColor = Color.red;
#endif

	#endregion

	#region Implementation Component

	protected override void Awake ()
	{
		base.Awake ();
		this.m_SampleColliders = new Collider[this.m_MaximumDetect];
		this.m_PreviousCount = 0;
	}

	protected override void LateUpdate ()
	{
		base.LateUpdate ();
		if (this.m_IsActive) {
			this.DetectObjects ();
		}
	}

#if UNITY_EDITOR

	protected virtual void OnDrawGizmosSelected() {
		Gizmos.color = this.m_SphereColor;
		if (this.m_DetectTransform != null) {
			Gizmos.DrawWireSphere (this.m_DetectTransform.position, this.m_DetectRadius);
		} else {
			Gizmos.DrawWireSphere (this.transform.position, this.m_DetectRadius);
		}
	}

#endif

	#endregion

	#region Main methods

	public virtual void DetectObjects() {
		this.m_ColliderCount = Physics.OverlapSphereNonAlloc (
			this.m_DetectTransform.position, 
			this.m_DetectRadius, 
			this.m_SampleColliders,
			this.m_DetectLayerMask);  
		if (this.m_ColliderCount != 0) {
			// DETECTED
			if (this.OnDetected != null) {
				this.OnDetected.Invoke ();				
			}
		} else {
			// FREE
			if (this.OnFree != null) {
				this.OnFree.Invoke ();
			}
		}
		// CHANGED
		if (this.m_PreviousCount != this.m_ColliderCount) {
			if (this.OnChanged != null) {
				this.OnChanged.Invoke ();				
			}
			this.m_PreviousCount = this.m_ColliderCount;
		}
	}

	public override void Reset ()
	{
		base.Reset ();
		this.m_SampleColliders = new Collider[this.m_MaximumDetect];
		this.m_PreviousCount = -1;
	}

	#endregion

}
