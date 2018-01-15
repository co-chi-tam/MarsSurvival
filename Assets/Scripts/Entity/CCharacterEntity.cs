using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UICustomize;
using FSM;

public class CCharacterEntity : CEntity, IContext {

	[SerializeField]	protected CFSMComponent m_FSMComponent;

	public override bool isActive {
		get { return this.m_IsActive; }
		set { this.m_IsActive = value; }
	}

	protected int m_AnimParam = 0;

	protected override void Start ()
	{
		base.Start ();
		this.m_FSMComponent.isActive = true;
	}

	protected override void Update ()
	{
		base.Update ();
		if (Input.GetKey (KeyCode.S)) {
			this.m_AnimParam = 2;
		}
		if (Input.GetKey (KeyCode.A)) {
			this.m_AnimParam = 3;
		}
		if (Input.GetKey (KeyCode.D)) {
			this.m_AnimParam = 4;
		}
	}

	protected override void LateUpdate ()
	{
		base.LateUpdate ();
	}

}
