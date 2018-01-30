using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGreenHouseMiniEntity : CMachineEntity {

	#region Fields

	protected CDataComponent m_DataComponent;
	protected CGreenHouseMiniData m_GreenHouseData;
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
			if (this.m_GreenHouseData == null)
				return base.energyPercent;
			return this.m_GreenHouseData.energy.energyPoint / this.m_GreenHouseData.energy.maxEnergyPoint;
		}
	}

	public override CAmountItem[] itemsPerCharge {
		get {
			if (this.m_GreenHouseData == null)
				return base.itemsPerCharge;
			return this.m_GreenHouseData.energy.itemsPerCharge;
		}
	}

	public override float collectPercent {
		get {
			if (this.m_GreenHouseData == null)
				return base.collectPercent;
			return this.m_GreenHouseData.productItem.productTime / this.m_GreenHouseData.productItem.totalProductTime;
		}
	}

	public override CAmountItem[] itemCollects {
		get {
			if (this.m_GreenHouseData == null)
				return base.itemCollects;
			return this.m_GreenHouseData.productItem.itemCollects;
		}
	}

	public bool isFullResource {
		get { 
			if (this.m_GreenHouseData == null)
				return false;
			return this.m_GreenHouseData.productItem.productTime >= this.m_GreenHouseData.productItem.totalProductTime; 
		}
	}

	public override bool HaveEnergy {
		get {
			if (this.m_GreenHouseData == null)
				return base.HaveEnergy;
			return this.m_GreenHouseData.energy.energyPoint > 0f;
		}
		set { base.HaveEnergy = value; }
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
		this.m_GreenHouseData = this.m_DataComponent.Get<CGreenHouseMiniData> ();
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

	public override void CollectItems ()
	{
		base.CollectItems ();
		this.m_DataComponent.UpdateDataPerInvoke ("ResetTime");
	}

	#endregion

	#region Getter && Setter

	public override string[] GetJobs ()
	{
		if (this.m_GreenHouseData == null)
			return base.GetJobs ();
		return this.m_GreenHouseData.machineJobs;
	}

	public override void SetAnimation(int value) {
		base.SetAnimation (value);
	}

	#endregion

}