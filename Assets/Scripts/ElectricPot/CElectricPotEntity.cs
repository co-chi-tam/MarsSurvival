using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CElectricPotEntity : CMachineEntity {

	#region Fields

	protected CAnimatorComponent m_AnimatorComponent;
	protected CDataComponent m_DataComponent;
	protected CElectricPotData m_ElectricPotData;

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
			if (this.m_ElectricPotData == null)
				return base.HaveEnergy; 
			return this.m_ElectricPotData.energy.energyPoint > 0f; 
		}
		set { base.HaveEnergy = value; }
	}

	public override float energyPercent {
		get { 
			if (this.m_ElectricPotData == null)
				return base.energyPercent;
			return this.m_ElectricPotData.energy.energyPoint / this.m_ElectricPotData.energy.maxEnergyPoint; 
		}
	}

	public override CAmountItem[] itemsPerCharge {
		get { 
			if (this.m_ElectricPotData == null)
				return base.itemsPerCharge; 
			return this.m_ElectricPotData.energy.itemsPerCharge;
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
		this.m_ElectricPotData = this.m_DataComponent.Get<CElectricPotData> ();
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
