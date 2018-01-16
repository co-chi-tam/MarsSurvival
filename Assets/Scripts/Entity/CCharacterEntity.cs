﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UICustomize;
using FSM;

public class CCharacterEntity : CEntity, IContext {

	[SerializeField]	protected CInventoryComponent m_Inventory;

	public override bool isActive {
		get { return this.m_IsActive; }
		set { this.m_IsActive = value; }
	}

	protected override void Start ()
	{
		base.Start ();
	}

	protected override void Update ()
	{
		base.Update ();
		if (Input.GetKeyDown (KeyCode.A)) {
			this.m_Inventory.PickItem ();
		}
	}

	protected override void LateUpdate ()
	{
		base.LateUpdate ();
	}

}
