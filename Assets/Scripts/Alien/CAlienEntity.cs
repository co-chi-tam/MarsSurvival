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
		var enemy = this.m_EntityDetectComponent.currentEntity;
		if (enemy != null) {
			
		}
	}

	#endregion

	#region Getter && Setter

	public override void SetAnimation(int value) {
		base.SetAnimation (value);
	}

	#endregion

}
