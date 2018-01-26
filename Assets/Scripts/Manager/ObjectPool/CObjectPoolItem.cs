using System;
using UnityEngine;

[Serializable]
public class CObjectPoolItem {

	[Header("Fields")]
	public int itemMaximum = 10;
	public string itemName;
	public CObjectPoolMemberComponent itemPrefab;

}
