using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnergyMachineEntity : CMachineEntity {

	#region Fields

	protected CDataComponent m_DataComponent;
	private CEnergyMachineData m_EnergyMachineData;

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
			if (this.m_EnergyMachineData == null)
				return false;
			return this.m_EnergyMachineData.energyPoint > 1f;
		}
		set { base.HaveEnergy = value; }
	}

	public override float energyPercent {
		get { 
			if (this.m_EnergyMachineData == null)
				return base.energyPercent;
			return this.m_EnergyMachineData.energyPoint / this.m_EnergyMachineData.maxEnergyPoint;
		}
	}

	public override CItemMaterial[] materialsPerCharge {
		get {
			if (this.m_EnergyMachineData == null)
				return base.materialsPerCharge;
			return this.m_EnergyMachineData.materialsPerCharge;
		}
	}

	#endregion

	#region Implementation Entity

	protected override void Awake ()
	{
		base.Awake ();
		this.m_DataComponent = this.GetGameComponent<CDataComponent> ();
	}

	protected override void Start ()
	{
		base.Start ();
		this.m_EnergyMachineData = this.m_DataComponent.Get<CEnergyMachineData>();
	}

	protected override void LateUpdate ()
	{
		base.LateUpdate ();
	}

	#endregion

	#region Main methods

	public override void AddEnergy ()
	{
		base.AddEnergy ();
		this.m_DataComponent.UpdateDataPerInvoke ("AddEnergy");
	}

	#endregion

}
