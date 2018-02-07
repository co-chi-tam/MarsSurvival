using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAlienEntity : CGameEntity {

	#region Fields

	protected CAnimatorComponent m_AnimatorComponent;
	protected CMoveComponent m_MoveComponent;
	protected CEntityDetectComponent m_EntityDetectComponent;
	protected CDataComponent m_DataComponent;
	protected CAlienData m_Data;

	public override bool IsActive {
		get { return base.IsActive; }
		set { base.IsActive = value; }
	}

	public virtual bool IsMoving {
		get { 
			return !this.m_MoveComponent.IsNearestTarget(); }
		set { 
			this.m_MoveComponent.targetPosition = value 
				? this.transform.position + this.transform.forward 
				: this.transform.position;
		}
	}

	[SerializeField]	protected CGameEntity m_OtherEntity;
	public CGameEntity otherEntity {
		get { return this.m_OtherEntity; }
		set { this.m_OtherEntity = value; }
	}

	#endregion

	#region Implementation Entity

	protected override void Awake ()
	{
		base.Awake ();
		this.m_AnimatorComponent = this.GetGameComponent<CAnimatorComponent> ();
		this.m_MoveComponent = this.GetGameComponent<CMoveComponent> ();
		this.m_EntityDetectComponent = this.GetGameComponent<CEntityDetectComponent> ();
		this.m_DataComponent = this.GetGameComponent<CDataComponent> ();
	}

	protected override void Start ()
	{
		base.Start ();
		this.m_Data = this.m_DataComponent.Get<CAlienData> ();
	}

	protected override void LateUpdate ()
	{
		base.LateUpdate ();
		// ANIMATION
		this.m_AnimatorComponent.ApplyAnimation (
			"AnimParam", 
			this.m_AnimationInt
		);
	}

	#endregion

	#region Main methods

	public override void ApplyDamage(float value) {
		base.ApplyDamage (value);
	}

	public override void AttackAnotherEntity() {
		base.AttackAnotherEntity ();
		if (this.m_OtherEntity != null) {
			this.m_OtherEntity.ApplyDamage (this.m_Data.attackDamage);
		}
	}

	public virtual void MoveToAnotherEntity(float dt) {
		if (this.m_OtherEntity != null) {
			var otherPosition = this.m_OtherEntity.myTransform.position;
			this.m_MoveComponent.targetPosition = otherPosition;
			this.m_MoveComponent.SetupMove (dt);
//			this.m_MoveComponent.Look (otherPosition);
		}
	}

	#endregion

	#region Getter && Setter

	public override void SetAnimation(int value) {
		base.SetAnimation (value);
	}

	#endregion

}
