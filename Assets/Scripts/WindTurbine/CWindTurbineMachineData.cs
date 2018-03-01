using System;
using UnityEngine;
using System.Runtime.Serialization;
using System.Reflection;

[Serializable]
public class CWindTurbineMachineData : CMachineData {

	#region Fields

	#endregion

	#region Constructor

	public CWindTurbineMachineData (): base() {
		this.m_MachineName = "Wind Turbine";
	}

	public CWindTurbineMachineData (SerializationInfo info, StreamingContext context) : base (info, context)
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
