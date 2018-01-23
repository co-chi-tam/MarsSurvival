using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPowerGeneratorMachineEntity : CMachineEntity {

	#region Fields

	protected CAnimatorComponent m_AnimatorComponent;
	private CPowerGeneratorMachineData m_Data;

	public override bool IsActive {
		get { return this.m_IsActive; }
		set { this.m_IsActive = value; }
	}

	public override bool IsStarted {
		get {
			return base.IsStarted; }
		set { base.IsStarted = value; }
	}

	public override bool HaveEnergy {
		get { 
			if (this.m_Data == null)
				return false;
			return this.m_Data.powerPoint > 1f;
		}
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
		this.m_Data = this.GetGameComponent<CDataComponent>().Get<CPowerGeneratorMachineData>();
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

	#region Getter && Setter

	public override void SetAnimation(int value) {
		base.SetAnimation (value);
	}

	#endregion

}
