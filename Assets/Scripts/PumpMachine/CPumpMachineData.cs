using System;
using UnityEngine;
using System.Runtime.Serialization;
using System.Reflection;

[Serializable]
public class CPumpMachineData : CMachineData {

	#region Fields

	#endregion

	#region Constructor

	public CPumpMachineData (): base() {
		this.m_MachineName = "Pump machine";
	}

	public CPumpMachineData (SerializationInfo info, StreamingContext context) : base (info, context)
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
