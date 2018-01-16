using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CItemEntity : CEntity {

	#region Fields

	[SerializeField]	protected CItemData m_ItemData;
	public CItemData itemData {
		get { return this.m_ItemData; }
		set { this.m_ItemData = value; }
	}

	#endregion

}

