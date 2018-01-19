using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSolarPinMachineEntity : CMachineEntity {

	#region Fields

	protected CAnimatorComponent m_AnimatorComponent;
	protected int m_Animation = 0;

	public override bool isActive {
		get { return this.m_IsActive; }
		set { this.m_IsActive = value; }
	}

	[SerializeField]	protected bool m_IsStarted = false;
	public bool IsStarted {
		get { return this.m_IsStarted; }
		set { this.m_IsStarted = value; }
	}

	public override bool HaveEnergy {
		get { return base.HaveEnergy; }
		set { base.HaveEnergy = value; }
	}

	#endregion

	#region Implementation Entity

	protected override void Awake ()
	{
		base.Awake ();
		this.m_AnimatorComponent = this.GetGameComponent<CAnimatorComponent> ();
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
			this.m_Animation
		);
	}

	#endregion

	#region Getter && Setter

	public virtual void SetAnimation(int value) {
		this.m_Animation = value;
	}

	#endregion

}
