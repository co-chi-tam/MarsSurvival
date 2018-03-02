using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPumpMachineEntity : CMachineEntity {

	#region Fields

	protected CAnimatorComponent m_AnimatorComponent;
	protected CLineEndComponent m_LineEndComponent;

	public override bool IsActive {
		get { return this.m_IsActive; }
		set { this.m_IsActive = value; }
	}

	public override bool IsStarted {
		get { return base.IsStarted; }
		set { base.IsStarted = value; }
	}

	public override bool HaveEnergy {
		get { return base.HaveEnergy; }
		set { base.HaveEnergy = value; }
	}

	#endregion

	#region Implementation Entity

	public override void Init ()
	{
		base.Init ();
	}

	protected override void Awake ()
	{
		base.Awake ();
		this.m_AnimatorComponent = this.GetGameComponent<CAnimatorComponent> ();
		this.m_LineEndComponent = this.GetGameComponent<CLineEndComponent> ();
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
