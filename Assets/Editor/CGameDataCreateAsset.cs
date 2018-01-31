using UnityEngine;
using UnityEditor;

public class CGameDataCreateAsset
{
	[MenuItem("Game Data/Item Data")]
	public static void CreateItemDataAsset ()
	{
		ScriptableObjectUtility.CreateAsset<CItemData> ();
	}

	[MenuItem("Game Data/Character Data")]
	public static void CreateCharacterDataAsset ()
	{
		ScriptableObjectUtility.CreateAsset<CCharacterData> ();
	}

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

}