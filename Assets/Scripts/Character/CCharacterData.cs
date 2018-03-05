﻿using UnityEngine;
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

	// OXYGEN
	[SerializeField]	protected float m_OxygenPoint = 50f;
	[Info(valueName = "Oxygen point", valueMin = 0f, valueMax = 9999f)]
	[UpdateValuePerSecond (updateMethod = "Increase", updateValuePerSecond = 2.5f)]
	[UpdateValuePerInvoke (updateName = "LowOxygen", updateMethod = "Decrease", updateValuePerInvoke = 5f)]
	public float oxygenPoint {
		get { return this.m_OxygenPoint; }
		set { this.m_OxygenPoint = value < 0f ? 0f : value > this.m_MaxOxygenPoint ? this.m_MaxOxygenPoint : value; }
	}
	[SerializeField]	protected float m_MaxOxygenPoint = 100f;
	public float maxOxygenPoint {
		get { return this.m_MaxOxygenPoint; }
		set { this.m_MaxOxygenPoint = value; }
	}

	// ENERGY
	[SerializeField]	protected float m_EnergyPoint = 50f;
	[Info(valueName = "Energy point", valueMin = 0f, valueMax = 9999f)]
	[UpdateValuePerSecond(updateMethod = "Decrease", updateValuePerSecond = 0.25f)]
	[UpdateValuePerInvoke(updateName = "AddEnergy", updateMethod = "Increase", updateValuePerInvoke = 1.5f)]
	public float energyPoint {
		get { return this.m_EnergyPoint; }
		set { this.m_EnergyPoint = value < 0f ? 0f : value > this.m_MaxEnergyPoint ? this.m_MaxEnergyPoint : value; }
	}
	[SerializeField]	protected float m_MaxEnergyPoint = 100f;
	public float maxEnergyPoint {
		get { return this.m_MaxEnergyPoint; }
		set { this.m_MaxEnergyPoint = value; }
	}

	// FOOD
	[SerializeField]	protected float m_FoodPoint = 75f;
	[Info(valueName = "Food point", valueMin = 0f, valueMax = 9999f)]
	[UpdateValuePerSecond(updateMethod = "Decrease", updateValuePerSecond = 0.2f)]
	[UpdateValuePerInvoke(updateName = "AddFood", updateMethod = "Increase", updateValuePerInvoke = 2f)]
	[UpdateValuePerInvoke(updateName = "UpdatePerAttack", updateMethod = "Decrease", updateValuePerInvoke = 2f)]
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
	[UpdateValuePerSecond(updateMethod = "Increase", updateValuePerSecond = 1f)]
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
	[SerializeField]	protected string m_CurrentTool = "Empty";
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

	[Header("Reward Abs")]
	[SerializeField]	protected CAmountItem[] m_RewardAbs;
	public CAmountItem[] rewardAbs {
		get { return this.m_RewardAbs; }
		set { this.m_RewardAbs = value; }
	}

	#endregion

	#region Constructor

	public CCharacterData (): base() {
		this.m_CharacterName	= "Empty name";
		this.m_MoveSpeed		= 5f;
		this.m_OxygenPoint 		= 50f;
		this.m_MaxOxygenPoint 	= 100f;
		this.m_EnergyPoint 		= 70f;
		this.m_MaxEnergyPoint 	= 100f;
		this.m_FoodPoint 		= 75f;
		this.m_MaxFoodPoint 	= 100f;
		this.m_Items 			= new List<CItemData> ();
	}

	public CCharacterData (SerializationInfo info, StreamingContext context) : base (info, context) {

	}

	#endregion

	#region Override

	public override string ToString ()
	{
		return string.Format ("[CCharacterData] {0} - {1} - {2}", characterName, moveSpeed, energyPoint);
	}

	#endregion

}
