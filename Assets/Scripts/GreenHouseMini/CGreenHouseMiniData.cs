using System;
using UnityEngine;
using System.Runtime.Serialization;
using System.Reflection;

[Serializable]
public class CGreenHouseMiniData : CMachineData {

	#region Fields

	[Header("Green House Fields")]
	[SerializeField]	protected CEnergyObjectData m_Energy;
	[UpdateContinueAttribute]
	[Info (valueName = "Energy")]
	public CEnergyObjectData energy {
		get { return this.m_Energy; }
		set { this.m_Energy = value; }
	}
	[SerializeField]	protected CProductItemData m_ProductItem;
	[UpdateContinueAttribute]
	[Info (valueName = "Product time")]
	public CProductItemData productItem {
		get { return this.m_ProductItem; }
		set { this.m_ProductItem = value; }
	}

	#endregion

	#region Constructor

	public CGreenHouseMiniData (): base() {
		this.m_MachineName = "Green House Mini";
	}

	public CGreenHouseMiniData (SerializationInfo info, StreamingContext context) : base (info, context)
	{

	}

	#endregion

	#region Getter && Setter

	public override void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		base.GetObjectData (info, context);
	}

	#endregion

}
