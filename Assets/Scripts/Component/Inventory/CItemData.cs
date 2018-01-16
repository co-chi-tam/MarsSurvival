using UnityEngine;
using System;
using System.Collections;

public class CItemData : ScriptableObject {

	#region Fields

	protected string m_ItemName;
	public string itemName {
		get { return this.m_ItemName; }
		set { this.m_ItemName = value; }
	}

	protected Sprite m_ItemAvatar;
	public Sprite itemAvatar {
		get { return this.m_ItemAvatar; }
		set { this.m_ItemAvatar = value; }
	}

	protected int m_ItemAmount;
	public int itemAmount {
		get { return this.m_ItemAmount; }
		set { this.m_ItemAmount = value; }
	}

	#endregion

	#region Constructor

	public CItemData () {
		
	}

	#endregion

}

