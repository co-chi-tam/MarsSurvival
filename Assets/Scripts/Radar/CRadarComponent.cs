using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (CPhysicDetectComponent))]
public class CRadarComponent : CComponent {

	protected CPhysicDetectComponent m_PhysicDetectComponent;

	protected override void Awake ()
	{
		base.Awake ();
		this.m_PhysicDetectComponent = this.GetComponent <CPhysicDetectComponent> ();
	}

}
