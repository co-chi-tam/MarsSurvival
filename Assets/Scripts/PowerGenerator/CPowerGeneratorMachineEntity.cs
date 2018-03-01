using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPowerGeneratorMachineEntity : CMachineEntity {

	#region Fields

	protected CPowerGeneratorMachineData m_PowerGeneratorData;
	protected CAnimatorComponent m_AnimatorComponent;

	public override bool IsActive {
		get { return this.m_IsActive; }
		set { this.m_IsActive = value; }
	}

	public override bool IsStarted {
		get {
			return base.IsStarted; }
		set { base.IsStarted = value; }
	}


	public override float energyPercent {
		get {
			if (this.m_PowerGeneratorData == null)
				return base.energyPercent;
			return this.m_PowerGeneratorData.energy.energyPoint / this.m_PowerGeneratorData.energy.maxEnergyPoint;
		}
	}

	public override CAmountItem[] itemsPerCharge {
		get {
			if (this.m_PowerGeneratorData == null)
				return base.itemsPerCharge;
			return this.m_PowerGeneratorData.energy.itemsPerCharge;
		}
	}

	public override bool HaveEnergy {
		get {
			if (this.m_PowerGeneratorData == null)
				return base.HaveEnergy;
			return this.m_PowerGeneratorData.energy.energyPoint > 0f;
		}
		set { base.HaveEnergy = value; }
	}

	#endregion

	#region Implementation Entity

	public override void Init ()
	{
		base.Init ();
		this.m_PowerGeneratorData = this.m_MachineData as CPowerGeneratorMachineData;
	}

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

	public override string[] GetJobs ()
	{
		if (this.m_PowerGeneratorData == null)
			return base.GetJobs ();
		return this.m_PowerGeneratorData.machineJobs;
	}

	public override void SetAnimation(int value) {
		base.SetAnimation (value);
	}

	#endregion

}
