using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMotherBoxEntity : CMachineEntity {

	#region Fields

	protected CMotherBoxData m_Data;

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
		get { return base.HaveEnergy; }
		set { base.HaveEnergy = value; }
	}

//	public override CRecipeData[] toolRecipes {
//		get {
//			if (this.m_Data == null)
//				return base.toolRecipes;
//			return this.m_Data.toolRecipes;
//		}
//	}
//
//	public override CRecipeData currentRecipe {
//		get {
//			if (this.m_Data == null)
//				return base.currentRecipe;
//			return this.m_Data.currentRecipe;
//		}
//		set {
//			if (this.m_Data == null)
//				return;
//			this.m_Data.currentRecipe = value;
//		}
//	}
//
//	public override bool IsProductToolCompleted {
//		get { 
//			if (this.m_Data == null)
//				return base.IsProductToolCompleted;
//			return this.m_Data.productTime >= this.m_Data.totalProductTime
//				&& this.m_Data.currentRecipe != null;
//		}
//	}	

	#endregion

	#region Implementation Entity

	public override void Init ()
	{
		base.Init ();
	}

	protected override void Awake ()
	{
		base.Awake ();
	}

	protected override void Start ()
	{
		base.Start ();
		this.m_Data = this.m_DataComponent.Get<CMotherBoxData> ();
	}

	protected override void LateUpdate ()
	{
		base.LateUpdate ();
	}

	#endregion

	#region Main methods



	#endregion

}
