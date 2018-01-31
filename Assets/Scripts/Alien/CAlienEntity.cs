using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAlienEntity : CEntity {

	#region Fields

	protected CAnimatorComponent m_AnimatorComponent;
	protected CMoveComponent m_MoveComponent;

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
	}

	protected override void Start ()
	{
		base.Start ();
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

	#endregion

	#region Getter && Setter

	public override void SetAnimation(int value) {
		base.SetAnimation (value);
	}

	#endregion

}
