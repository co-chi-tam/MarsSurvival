using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGreenHouseMiniData : CEnergyMachineData {

	#region Fields

	[Header("Green House Fields")]
	[SerializeField]	protected float m_ProductTime = 0f;
	[Info (valueName = "Product time", valueMin = 0f, valueMax = 100f)]
	[UpdateValuePerInvoke (updateName = "ProductItem", updateMethod = "Increase", updateValuePerInvoke = 0.5f)]
	[UpdateValuePerInvoke (updateName = "ResetTime", updateMethod = "SetValue", updateValuePerInvoke = 0f)]
	public float productTime {
		get { return this.m_ProductTime; }
		set { this.m_ProductTime = value; }
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

	public CGreenHouseMiniData (): base() {
		this.m_MachineName = "Green House Mini";
	}

	#endregion

}
