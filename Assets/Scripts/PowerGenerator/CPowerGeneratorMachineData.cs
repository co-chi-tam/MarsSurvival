using System;
using System.Runtime.Serialization;
using System.Reflection;
using UnityEngine;

[Serializable]
public class CPowerGeneratorMachineData : CMachineData {
	
	#region Fields

	[Header("Power Generator Fields")]
	[SerializeField]	protected CEnergyObjectData m_Energy;
	[UpdateContinueAttribute]
	[Info (valueName = "Energy")]
	public CEnergyObjectData energy {
		get { return this.m_Energy; }
		set { this.m_Energy = value; }
	}

	#endregion

	#region Constructor

	public CPowerGeneratorMachineData (): base() {
		this.m_MachineName = "Energy generate machine";
	}

	public CPowerGeneratorMachineData (SerializationInfo info, StreamingContext context) : base (info, context)
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
