using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CMachineEntity : CGameEntity {

	#region Fields

	protected CDataComponent m_DataComponent;
	protected CMoveComponent m_MoveComponent;
	protected CFollowObjectComponent m_FollowObjectComponent;
	protected CFSMComponent m_FSMComponent;
	protected CMachineData m_MachineData;

	[SerializeField]	protected bool m_HaveEnergy = false;
	public virtual bool HaveEnergy {
		get { return this.m_HaveEnergy; }
		set { this.m_HaveEnergy = value; }
	}

	[SerializeField]	protected bool m_IsStarted = false;
	public virtual bool IsStarted {
		get { return this.m_IsStarted; }
		set { this.m_IsStarted = value; }
	}

	public override bool IsActive {
		get { return base.IsActive; }
		set { base.IsActive = value; }
	}

	public bool isFollowing {
		get { 
			return this.m_FollowObjectComponent != null 
			&& this.m_FollowObjectComponent.target != null; 
		}
	}

	public virtual float energyPercent {
		get { return 0f; }
	}

	public virtual CAmountItem[] itemsPerCharge {
		get { return new CAmountItem[0]; }
	}

	public virtual float collectPercent {
		get { return 0f; }
	}

	public virtual CAmountItem[] itemCollects {
		get { return new CAmountItem[0]; }
	}

	public virtual CAmountItem[] activeWithItems {
		get { 
			if (this.m_MachineData == null)
				return new CAmountItem[0];
			return this.m_MachineData.activeWithItems; 
		}
	}

//	public virtual CRecipeData[] toolRecipes {
//		get { return new CRecipeData[0]; }
//	}
//	public virtual CRecipeData currentRecipe {
//		get { return null; }
//		set {  }
//	}
//	public virtual bool IsProductToolCompleted {
//		get { return false; }
//	}

	#endregion

	#region Implementation Entity

	public override void Init ()
	{
		base.Init ();
		this.m_MachineData = this.m_DataComponent.Get<CMachineData> ();
		this.m_MoveComponent.SetupPosition (this.m_MachineData.position.ToV3 (), Quaternion.identity);
		this.m_MoveComponent.currentRotationAngle = this.m_MachineData.rotation;
		this.IsActive = this.m_MachineData.isActive;
		this.IsStarted = this.m_MachineData.isStart;
		this.HaveEnergy = this.m_MachineData.isHaveEnergy;
		this.m_FSMComponent.SetState (this.m_MachineData.currentState);
	}

	protected override void Awake ()
	{
		base.Awake ();
		this.m_DataComponent 			= this.GetGameComponent <CDataComponent> ();
		this.m_MoveComponent 			= this.GetGameComponent<CMoveComponent> ();
		this.m_FollowObjectComponent 	= this.GetGameComponent<CFollowObjectComponent> ();
		this.m_FSMComponent 			= this.GetGameComponent <CFSMComponent> ();
	}

	protected override void Start ()
	{
		base.Start ();
		this.Init ();
	}

	protected override void LateUpdate ()
	{
		base.LateUpdate ();
	}

	#endregion

	#region Main methods

	public virtual void SaveEntity () {
		// DATA
		this.m_MachineData.position = this.m_MoveComponent.currentPosition.ToString();
		this.m_MachineData.rotation = this.m_MoveComponent.currentRotationAngle;
		this.m_MachineData.isActive = this.IsActive;
		this.m_MachineData.isStart = this.IsStarted;
		this.m_MachineData.isHaveEnergy = this.HaveEnergy;
		this.m_MachineData.currentState = this.m_FSMComponent.GetState ();
	}

	public override void AddEnergy() {
		base.AddEnergy ();
	}

	public override void CollectItems() {
		base.CollectItems ();
	}

	#endregion

	#region Getter && Setter

	public override void SetAnimation(int value) {
		base.SetAnimation (value);
	}

	public virtual string[] GetJobs() {
		if (this.m_MachineData == null) 
			return new string[0];
		return this.m_MachineData.machineJobs;
	}

	#endregion

}
