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

}