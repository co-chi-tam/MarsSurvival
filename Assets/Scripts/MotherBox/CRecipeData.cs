using System;
using UnityEngine;

[Serializable]
public class CRecipeData : ScriptableObject {

	[SerializeField]	protected string m_RecipeName;
	public string recipeName {
		get { return this.m_RecipeName; }
		set { this.m_RecipeName = value; }
	}
	[SerializeField]	protected CGameEntityData m_ResuleData;
	public CGameEntityData resultData {
		get { return this.m_ResuleData; }
		set { this.m_ResuleData = value; }
	}
	[SerializeField]	protected CAmountItem[] m_RecipeIngredients;
	public CAmountItem[] ingredients {
		get { return this.m_RecipeIngredients; }
		set { this.m_RecipeIngredients = value; }
	}

}
