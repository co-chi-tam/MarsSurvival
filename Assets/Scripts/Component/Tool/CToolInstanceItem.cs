using System;
using UnityEngine;

[Serializable]
public class CToolInstanceItem {

	[Header("Fields")]
	public string toolName;
	public CToolData toolData;
	public CToolComponent toolPrefab;

}
