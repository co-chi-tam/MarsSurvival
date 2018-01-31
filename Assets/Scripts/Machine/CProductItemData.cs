using System;
using UnityEngine;

[Serializable]
public class CProductItemData {

	#region Fields

	[Header("Product Item Fields")]
	[SerializeField]	protected float m_ProductTime = 0f;
	[Info (valueName = "Product time", valueMin = 0f, valueMax = 999f)]
	[UpdateValuePerInvoke (updateName = "ProductItem", updateMethod = "Increase", updateValuePerInvoke = 0.5f)]
	[UpdateValuePerInvoke (updateName = "ResetTime", updateMethod = "SetValue", updateValuePerInvoke = 0f)]
	public float productTime {
		get { return this.m_ProductTime; }
		set { this.m_ProductTime = value < 0f ? 0f : value > 999f ? 999f : value; }
	}
	[SerializeField]	protected float m_ProductTimeInterval = 60f;
	public float totalProductTime {
		get { return this.m_ProductTimeInterval; }
		set { this.m_ProductTimeInterval = value; }
	}
	[SerializeField]	protected CAmountItem[] m_ProductItems;
	public CAmountItem[] itemCollects {
		get { return this.m_ProductItems; }
		set { this.m_ProductItems = value; }
	}

	#endregion

	#region Constructor

	public CProductItemData () {

	}

	#endregion

}
