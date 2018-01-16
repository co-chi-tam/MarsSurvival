using UnityEngine;
using UnityEditor;

public class CGameDataCreateAsset
{
	[MenuItem("Game Data/Item Data")]
	public static void CreateDataAsset ()
	{
		ScriptableObjectUtility.CreateAsset<CItemData> ();
	}



}