using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class CItemData : CEntityData {

	#region Fields

	[Header("Item Fields")]
	public string itemName = string.Empty;
	public Sprite avatar = null;
	public GameObject model = null;
	public int amount = 0;

	#endregion

	#region Constructor

	public CItemData (): base() {
		
	}

	#endregion

	#region Object

	public override string ToString () {
		return string.Format ("[CItemData: itemName={0}, itemAvatar={1}, itemModel={2}, itemAmount={3}]", itemName, avatar, model, amount);
	}

	#endregion

}

