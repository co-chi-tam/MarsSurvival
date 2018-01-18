using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class CCharacterData : ScriptableObject {

	#region Fields

	[Header("Fields")]
	public string characterName;
	public float moveSpeed;

	[SerializeField]	protected float m_SolarPoint;
	[MarkerValue(valueName = "Solar Point", updateMethod = "Decrease", updateValuePerSecond = 0.5f)]
	public float solarPoint {
		get { return this.m_SolarPoint; }
		set { this.m_SolarPoint = value < 0f ? 0f : value > this.m_MaxSolarPoint ? this.m_MaxSolarPoint : value; }
	}

	[SerializeField]	protected float m_MaxSolarPoint;
	public float maxSolarPoint {
		get { return this.m_MaxSolarPoint; }
		set { this.m_MaxSolarPoint = value; }
	}

	#endregion

	#region Constructor

	public CCharacterData (): base() {

	}

	#endregion

	#region Override

	public override string ToString ()
	{
		return string.Format ("[CCharacterData] {0} - {1} - {2}", characterName, moveSpeed, solarPoint);
	}

	#endregion


}
