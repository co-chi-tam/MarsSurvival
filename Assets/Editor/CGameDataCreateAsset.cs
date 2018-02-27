using UnityEngine;
using UnityEditor;

public class CGameDataCreateAsset
{
	#region Item

	[MenuItem("Game Data/Item/Empty Data")]
	public static void CreateItemDataAsset ()
	{
		ScriptableObjectUtility.CreateAsset<CItemData> ();
	}

	#endregion

	#region Character 

	[MenuItem("Game Data/Character Data")]
	public static void CreateCharacterDataAsset ()
	{
		ScriptableObjectUtility.CreateAsset<CCharacterData> ();
	}

	#endregion

	#region Machine

	[MenuItem("Game Data/Machine/Empty Data")]
	public static void CreateMachineDataAsset ()
	{
		ScriptableObjectUtility.CreateAsset<CMachineData> ();
	}

	[MenuItem("Game Data/Machine/Power Generator Data")]
	public static void CreatePowerGeneratorDataAsset ()
	{
		ScriptableObjectUtility.CreateAsset<CPowerGeneratorMachineData> ();
	}

	[MenuItem("Game Data/Machine/Pump Machine Data")]
	public static void CreatePumpMachineDataAsset ()
	{
		ScriptableObjectUtility.CreateAsset<CPumpMachineData> ();
	}

	[MenuItem("Game Data/Machine/Green House Machine Data")]
	public static void CreateGreenHouseMiniDataAsset ()
	{
		ScriptableObjectUtility.CreateAsset<CGreenHouseMiniData> ();
	}

	[MenuItem("Game Data/Machine/Electric Pot Machine Data")]
	public static void CreateElectricPotDataAsset ()
	{
		ScriptableObjectUtility.CreateAsset<CElectricPotData> ();
	}

	[MenuItem("Game Data/Machine/Wind Turbine Machine Data")]
	public static void CreateWindTurbineDataAsset ()
	{
		ScriptableObjectUtility.CreateAsset<CWindTurbineMachineData> ();
	}

	[MenuItem("Game Data/Machine/Mother Box Machine Data")]
	public static void CreateMotherBoxDataAsset ()
	{
		ScriptableObjectUtility.CreateAsset<CMotherBoxData> ();
	}

	#endregion

	#region Alien

	[MenuItem("Game Data/Alien/Empty Alien Data")]
	public static void CreateEmptyAlienDataAsset ()
	{
		ScriptableObjectUtility.CreateAsset<CAlienData> ();
	}

	#endregion

	#region Tool

	[MenuItem("Game Data/Tool/Empty Tool Data")]
	public static void CreateEmptyToolDataAsset ()
	{
		ScriptableObjectUtility.CreateAsset<CToolData> ();
	}

	#endregion

	#region Recipe

	[MenuItem("Game Data/Recipe/Recipe Data")]
	public static void CreateRecipeDataAsset ()
	{
		ScriptableObjectUtility.CreateAsset<CRecipeData> ();
	}

	#endregion

	#region Enviroment

	[MenuItem("Game Data/Enviroment/Empty Data")]
	public static void CreateEnviromentDataAsset ()
	{
		ScriptableObjectUtility.CreateAsset<CEnviromentData> ();
	}

	[MenuItem("Game Data/Enviroment/Plant Alien Data")]
	public static void CreatePlantAlienDataAsset ()
	{
		ScriptableObjectUtility.CreateAsset<CPlantAlienData> ();
	}

	#endregion

	#region Mission

	[MenuItem("Game Data/Mission/Mission Data")]
	public static void CreateMissionDataAsset ()
	{
		ScriptableObjectUtility.CreateAsset<CMissionData> ();
	}

	[MenuItem("Game Data/Mission/Condition/Position Condition")]
	public static void CreateV3ConditionDataAsset ()
	{
		ScriptableObjectUtility.CreateAsset<CVector3Condition> ();
	}

	[MenuItem("Game Data/Mission/Condition/Obtain Item Condition")]
	public static void CreateObtainItemConditionDataAsset ()
	{
		ScriptableObjectUtility.CreateAsset<CObtainItemCondition> ();
	}

	[MenuItem("Game Data/Mission/Condition/Interactive Machine Condition")]
	public static void CreateInteractiveMachineDataAsset ()
	{
		ScriptableObjectUtility.CreateAsset<CInteractiveMachineCondition> ();
	}

	#endregion

}