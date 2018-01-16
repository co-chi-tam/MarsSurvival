using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class CItemData : ScriptableObject {

	#region Fields

	[Header("Fields")]
	[SerializeField]	protected string m_ItemName;
	public string itemName {
		get { return this.m_ItemName; }
		set { this.m_ItemName = value; }
	}

	[SerializeField]	protected Sprite m_ItemAvatar;
	public Sprite itemAvatar {
		get { return this.m_ItemAvatar; }
		set { this.m_ItemAvatar = value; }
	}

	[SerializeField]	protected GameObject m_ItemModel;
	public GameObject itemModel {
		get { return this.m_ItemModel; }
		set { this.m_ItemModel = value; }
	}

	[SerializeField]	protected int m_ItemAmount;
	public int itemAmount {
		get { return this.m_ItemAmount; }
		set { this.m_ItemAmount = value; }
	}

	#endregion

	#region Constructor

	public CItemData () {
		this.m_ItemName = string.Empty;
		this.m_ItemAvatar = null;
		this.m_ItemAmount = 0;
	}

	#endregion

	#region Object

	public override string ToString ()
	{
		return string.Format ("[CItemData: itemName={0}, itemAvatar={1}, itemModel={2}, itemAmount={3}]", itemName, itemAvatar, itemModel, itemAmount);
	}

	#endregion

}

