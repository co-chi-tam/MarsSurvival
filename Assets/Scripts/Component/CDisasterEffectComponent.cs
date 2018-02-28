using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Ludiq.Reflection;

[RequireComponent(typeof(CPhysicDetectComponent))]
public class CDisasterEffectComponent : CComponent {

	#region Internal class 

	[Serializable]
	public class UnityEventFloat: UnityEvent<float> {}

	#endregion

	#region Fields

	[Header("Events")]
	[SerializeField]	protected float m_DisasterValue = 0.5f;
	public float disasterValue {
		get { return this.m_DisasterValue; }
		set { this.m_DisasterValue = value; }
	}
	[SerializeField]	protected float m_FreeValue = 1f;
	public float freeValue {
		get { return this.m_FreeValue; }
		set { this.m_FreeValue = value; }
	}

	[Header("Events")]
	public UnityEventFloat OnDisasterEnter;
	public UnityEventFloat OnDisasterOut;

	protected CPhysicDetectComponent m_PhysicDetectComponent;

	#endregion

	#region Implementation Component

	protected override void Awake ()
	{
		base.Awake ();
		this.m_PhysicDetectComponent = this.GetComponent<CPhysicDetectComponent> ();
	}

	protected override void Start ()
	{
		base.Start ();
		this.m_PhysicDetectComponent.OnDetected.AddListener (() => {
			if (this.OnDisasterEnter != null) {
				this.OnDisasterEnter.Invoke (this.m_DisasterValue);
			}
		});
		this.m_PhysicDetectComponent.OnFree.AddListener (() => {
			if (this.OnDisasterOut != null) {
				this.OnDisasterOut.Invoke (this.m_FreeValue);
			}
		});
	}

	#endregion


}
