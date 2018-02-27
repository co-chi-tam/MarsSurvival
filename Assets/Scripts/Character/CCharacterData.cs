using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Reflection;

[Serializable]
public class CCharacterData : CGameEntityData {

	#region Fields

	[Header("Character Fields")]
	[SerializeField]	protected string m_CharacterName;
	public string characterName {
		get { return this.m_CharacterName; }
		set { this.m_CharacterName = value; }
	}

	[SerializeField]	protected float m_MoveSpeed;
	public float moveSpeed {
		get { return this.m_MoveSpeed; }
		set { this.m_MoveSpeed = value; }
	}

	// ENERGY
	[SerializeField]	protected float m_EnergyPoint;
	[Info(valueName = "Energy point", valueMin = 0f, valueMax = 9999f)]
	[UpdateValuePerSecond(updateMethod = "Decrease", updateValuePerSecond = 0.1f)]
	[UpdateValuePerInvoke(updateName = "AddEnergy", updateMethod = "Increase", updateValuePerInvoke = 1.5f)]
	[UpdateValuePerInvoke(updateName = "UpdatePerAttack", updateMethod = "Decrease", updateValuePerInvoke = 1f)]
	public float energyPoint {
		get { return this.m_EnergyPoint; }
		set { this.m_EnergyPoint = value < 0f ? 0f : value > this.m_MaxEnergyPoint ? this.m_MaxEnergyPoint : value; }
	}

	[SerializeField]	protected float m_MaxEnergyPoint;
	public float maxEnergyPoint {
		get { return this.m_MaxEnergyPoint; }
		set { this.m_MaxEnergyPoint = value; }
	}

	// WATER
	[SerializeField]	protected float m_WaterPoint = 50f;
	[Info(valueName = "Water point", valueMin = 0f, valueMax = 9999f)]
	[UpdateValuePerSecond(updateMethod = "Decrease", updateValuePerSecond = 0.15f)]
	[UpdateValuePerInvoke(updateName = "AddWater", updateMethod = "Increase", updateValuePerInvoke = 2.5f)]
	[UpdateValuePerInvoke(updateName = "UpdatePerAttack", updateMethod = "Decrease", updateValuePerInvoke = 1.5f)]
	public float waterPoint {
		get { return this.m_WaterPoint; }
		set { this.m_WaterPoint = value < 0f ? 0f : value > this.m_MaxWaterPoint ? this.m_MaxWaterPoint : value; }
	}
	[SerializeField]	protected float m_MaxWaterPoint = 100f;
	public float maxWaterPoint {
		get { return this.m_MaxWaterPoint; }
		set { this.m_MaxWaterPoint = value; }
	}

	// FOOD
	[SerializeField]	protected float m_FoodPoint = 75f;
	[Info(valueName = "Food point", valueMin = 0f, valueMax = 9999f)]
	[UpdateValuePerSecond(updateMethod = "Decrease", updateValuePerSecond = 0.12f)]
	[UpdateValuePerInvoke(updateName = "AddFood", updateMethod = "Increase", updateValuePerInvoke = 2f)]
	[UpdateValuePerInvoke(updateName = "UpdatePerAttack", updateMethod = "Decrease", updateValuePerInvoke = 0.5f)]
	public float foodPoint {
		get { return this.m_FoodPoint; }
		set { this.m_FoodPoint = value < 0f ? 0f : value > this.m_MaxFoodPoint ? this.m_MaxFoodPoint : value; }
	}
	[SerializeField]	protected float m_MaxFoodPoint = 100f;
	public float maxFoodPoint {
		get { return this.m_MaxFoodPoint; }
		set { this.m_MaxFoodPoint = value; }
	}

	// HEALTH
	[SerializeField]	protected float m_HealthPoint = 100f;
	[Info (valueName = "Health point", valueMin = 0, valueMax = 9999f)]
	public float healthPoint {
		get { return this.m_HealthPoint; }
		set { this.m_HealthPoint = value < 0f ? 0f : value > this.m_MaxHealthPoint ? this.m_MaxHealthPoint : value; }
	}
	[SerializeField]	protected float m_MaxHealthPoint = 100f;
	public float maxHealthPoint {
		get { return this.m_MaxHealthPoint; }
		set { this.m_MaxHealthPoint = value; }
	}

	[SerializeField]	protected List<CItemData> m_Items;
	[Info(valueName = "Inventory")]
	public List<CItemData> items {
		get { return this.m_Items; }
		set { this.m_Items = new List<CItemData> (value); }
	}

	[Header("Tool Fields")]
	[SerializeField]	protected string m_CurrentTool;
	public string currentTool {
		get { return this.m_CurrentTool; }
		set { this.m_CurrentTool = value; }
	}

	[Header("Mission")]
	[SerializeField]	protected int m_MissionIndex = 0;
	public int missionIndex {
		get { return this.m_MissionIndex; }
		set { this.m_MissionIndex = value; }
	}
	[SerializeField]	protected CMissionData[] m_ListMissions;
	public CMissionData[] listMissions {
		get { return this.m_ListMissions; }
		set { this.m_ListMissions = value; }
	}

	#endregion

	#region Constructor

	public CCharacterData (): base() {
		this.characterName 		= "Empty name";
		this.moveSpeed 			= 5f;
		this.m_EnergyPoint 		= 70f;
		this.m_MaxEnergyPoint 	= 100f;
		this.m_WaterPoint 		= 50f;
		this.m_MaxWaterPoint	= 100f;
		this.m_FoodPoint 		= 75f;
		this.m_MaxFoodPoint 	= 100f;
		this.m_Items 			= new List<CItemData> ();
	}

	public CCharacterData (SerializationInfo info, StreamingContext context) : base(info, context) {

	}

	#endregion

	#region Override

	public override string ToString ()
	{
		return string.Format ("[CCharacterData] {0} - {1} - {2}", characterName, moveSpeed, energyPoint);
	}

	#endregion

}
