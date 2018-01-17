using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UICustomize;
using FSM;

public class CCharacterEntity : CEntity, IContext {

	#region Internal Class

	public enum ECharacterAnimation: int
	{
		Idle = 0,
		Move = 1,
		Charge = 2,
		Talk = 3,
		Action = 4,

		Death = 10
	}

	#endregion

	#region Fields

	[Header("Info")]
	[SerializeField]	protected CAnimatorComponent m_AnimatorComponent;
	[SerializeField]	protected ECharacterAnimation m_CurrentAnimation = ECharacterAnimation.Idle;
	public int currentAnimation {
		get { return (int) this.m_CurrentAnimation; }
		set { this.m_CurrentAnimation = (ECharacterAnimation)value; }
	}
	[SerializeField]	protected CMoveComponent m_MoveComponent;
	public Vector3 position {
		get { return this.m_MoveComponent.currentPosition; }
		set { this.m_MoveComponent.currentPosition = value; }
	}
	public Vector3 rotation {
		get { return this.m_MoveComponent.currentRotation; }
		set { this.m_MoveComponent.currentRotation = value; }
	}

	public override bool isActive {
		get { return this.m_IsActive; }
		set { this.m_IsActive = value; }
	}

	#endregion

	#region Implementation Entity

	protected override void Start ()
	{
		base.Start ();
	}

	protected override void Update ()
	{
		base.Update ();
	}

	protected override void LateUpdate ()
	{
		base.LateUpdate ();
		this.m_AnimatorComponent.ApplyAnimation ("AnimParam", (int)this.m_CurrentAnimation);
	}

	#endregion

	#region Getter && Setter

	public virtual void SetAnimation(int value) {
		this.m_CurrentAnimation = (ECharacterAnimation)value;
	}

	#endregion

}
