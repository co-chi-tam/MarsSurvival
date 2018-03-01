using System;
using UnityEngine;
using System.Runtime.Serialization;
using System.Reflection;

[Serializable]
public class CElectricPotData : CMachineData {

	#region Fields

	[Header("Electric Pot Fields")]
	[SerializeField]	protected CEnergyObjectData m_Energy;
	[UpdateContinueAttribute]
	[Info (valueName = "Energy")]
	public CEnergyObjectData energy {
		get { return this.m_Energy; }
		set { this.m_Energy = value; }
	}

	#endregion

	#region Constructor

	public CElectricPotData (): base() {
		this.m_MachineName = "Electric pot machine";
	}

	public CElectricPotData (SerializationInfo info, StreamingContext context) : base (info, context)
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
