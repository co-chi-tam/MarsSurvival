using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UICustomize;
using FSM;

public class CCharacterEntity : CEntity, IContext {

	[SerializeField]	protected UIJoytick m_Joytick;
	[SerializeField]	protected CMoveComponent m_MoveComponent;
	[SerializeField]	protected CAnimatorComponent m_Animator;
	[SerializeField]	protected CFSMComponent m_FSMComponent;

	public override bool isActive {
		get { return this.m_IsActive; }
		set { this.m_IsActive = value; }
	}

	protected int m_AnimParam = 0;

	protected override void Start ()
	{
		base.Start ();
		this.m_MoveComponent.isActive = true;
		this.m_FSMComponent.isActive = true;
	}

	protected override void Update ()
	{
		base.Update ();
		if (this.m_Joytick != null) {
			var movePoint = this.transform.position + this.m_Joytick.InputDirectionXZ;
			this.m_MoveComponent.targetPosition = movePoint;
			this.m_AnimParam = this.m_MoveComponent.IsNearestTarget () ? 0 : 1;
		}
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
		this.m_Animator.ApplyAnimation ("AnimParam", this.m_AnimParam);
	}

}
