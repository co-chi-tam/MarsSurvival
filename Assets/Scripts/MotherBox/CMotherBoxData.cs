using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Reflection;

[Serializable]
public class CMotherBoxData : CMachineData {

	#region Fields

//	[Header("Mother Box Fields")]
//	[SerializeField]	protected CRecipeData[] m_ToolRecipes;
//	[Info (valueName = "Tool recipes")]
//	public CRecipeData[] toolRecipes {
//		get { return this.m_ToolRecipes; }
//		set { this.m_ToolRecipes = value; }
//	}
//	protected CRecipeData m_CurrentRecipe;
//	[Info (valueName = "Current Tool")]
//	[UpdateContinueAttribute]
//	public CRecipeData currentRecipe {
//		get { return this.m_CurrentRecipe; }
//		set { this.m_CurrentRecipe = ScriptableObject.Instantiate (value); }
//	}
//	[SerializeField]	protected float m_ProductTime = 0f;
//	[Info (valueName = "Product time", valueMin = 0f, valueMax = 999f)]
//	[UpdateValuePerInvoke (updateName = "ProductTool", updateMethod = "Increase", updateValuePerInvoke = 1f)]
//	[UpdateValuePerInvoke (updateName = "ResetProductTime", updateMethod = "SetValue", updateValuePerInvoke = 0f)]
//	public float productTime {
//		get { return this.m_ProductTime; }
//		set { this.m_ProductTime = value < 0f ? 0f : value > this.m_TotalProductTime ? this.m_TotalProductTime : value; }
//	}
//	[SerializeField]	protected float m_TotalProductTime = 60f;
//	public float totalProductTime {
//		get { return this.m_TotalProductTime; }
//		set { this.m_TotalProductTime = value; }
//	}

	#endregion

	#region Constructor

	public CMotherBoxData () : base ()
	{
		
	}

	public CMotherBoxData (SerializationInfo info, StreamingContext context) : base (info, context)
	{

	}

	#endregion

}
