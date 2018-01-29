using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGreenHouseMiniEntity : CEnergyMachineEntity {

	#region Fields

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

	public override float collectPercent {
		get {
			if (this.m_GreenHouseData == null)
				return base.collectPercent;
			return this.m_GreenHouseData.productTime / this.m_GreenHouseData.totalProductTime;
		}
	}

	public override CAmountItem[] itemCollects {
		get {
			if (this.m_GreenHouseData == null)
				return base.itemCollects;
			return this.m_GreenHouseData.itemCollects;
		}
	}

	public bool isFullResource {
		get { 
			if (this.m_GreenHouseData == null)
				return false;
			return this.m_GreenHouseData.productTime >= this.m_GreenHouseData.totalProductTime; 
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

	public override void CollectItems ()
	{
		base.CollectItems ();
		this.m_DataComponent.UpdateDataPerInvoke ("ResetTime");
	}

	#endregion

	#region Getter && Setter

	public override void SetAnimation(int value) {
		base.SetAnimation (value);
	}

	#endregion

}