using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class CMachineData : ScriptableObject {

	#region Fields

	[Header("Fields")]
	public string machineName;

	[SerializeField]	protected float m_PowerPoint;
	[Info(valueName = "Power point", valueMin = 0f, valueMax = 9999)]
	[UpdateValuePerInvoke(updateName = "UsePower", updateMethod = "Decrease", updateValuePerInvoke = 1.5f)]
	[UpdateValuePerInvoke(updateName = "ChargingPower", updateMethod = "Increase", updateValuePerInvoke = 1.5f)]
	public float powerPoint {
		get { return this.m_PowerPoint; }
		set { this.m_PowerPoint = value < 0 ? 0 : value > this.m_MaxPowerPoint ? this.m_MaxPowerPoint : value; }
	}

	[SerializeField]	protected float m_MaxPowerPoint;
	public float maxPowerPoint {
		get { return this.m_MaxPowerPoint; }
		set { this.m_MaxPowerPoint = value; }
	}

	#endregion

	#region Constructor

	public CMachineData (): base() {
		this.machineName 		= "Empty name";
		this.m_PowerPoint 		= 0f;
		this.m_MaxPowerPoint 	= 0f;
	}

	#endregion

	#region Override

	public override string ToString ()
	{
		return string.Format ("[CMachineData] machineName: {0}", this.machineName);
	}

	#endregion

}
