using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CMachineEntity : CGameEntity {

	#region Fields

	protected CFollowObjectComponent m_FollowObjectComponent;
	private CMachineData m_MachineData;

	[SerializeField]	protected bool m_HaveEnergy;
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

	public virtual CRecipeData[] toolRecipes {
		get { return new CRecipeData[0]; }
	}
	public virtual CRecipeData currentRecipe {
		get { return null; }
		set {  }
	}
	public virtual bool IsProductToolCompleted {
		get { return false; }
	}

	#endregion

	#region Implementation Entity

	protected override void Awake ()
	{
		base.Awake ();
		this.m_FollowObjectComponent = this.GetGameComponent<CFollowObjectComponent> ();
	}

	protected override void Start ()
	{
		base.Start ();
		this.m_MachineData = this.GetGameComponent<CDataComponent> ().Get<CMachineData> ();
	}

	#endregion

	#region Main methods

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
