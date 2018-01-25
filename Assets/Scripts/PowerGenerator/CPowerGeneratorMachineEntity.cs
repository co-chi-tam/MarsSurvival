using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPowerGeneratorMachineEntity : CMachineEntity {

	#region Fields

	protected CAnimatorComponent m_AnimatorComponent;
	protected CDataComponent m_DataComponent;
	private CPowerGeneratorMachineData m_PowerGeneratorData;

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
			if (this.m_PowerGeneratorData == null)
				return false;
			return this.m_PowerGeneratorData.energyPoint > 1f;
		}
		set { base.HaveEnergy = value; }
	}

	public override float energyPercent {
		get { 
			if (this.m_PowerGeneratorData == null)
				return base.energyPercent;
			return this.m_PowerGeneratorData.energyPoint / this.m_PowerGeneratorData.maxEnergyPoint;
		}
	}

	public override CItemMaterial[] materialsPerCharge {
		get {
			if (this.m_PowerGeneratorData == null)
				return base.materialsPerCharge;
			return this.m_PowerGeneratorData.materialsPerCharge;
		}
	}

	#endregion

	#region Implementation Entity

	protected override void Awake ()
	{
		base.Awake ();
		this.m_AnimatorComponent = this.GetGameComponent<CAnimatorComponent> ();
		this.m_DataComponent = this.GetGameComponent<CDataComponent> ();
	}

	protected override void Start ()
	{
		base.Start ();
		this.m_PowerGeneratorData = this.m_DataComponent.Get<CPowerGeneratorMachineData>();
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

	public override void AddEnergy ()
	{
		base.AddEnergy ();
		this.m_DataComponent.UpdateDataPerInvoke ("AddEnergy");
	}

	#endregion

	#region Getter && Setter

	public override void SetAnimation(int value) {
		base.SetAnimation (value);
	}

	#endregion

}
