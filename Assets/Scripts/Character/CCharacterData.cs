using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class CCharacterData : ScriptableObject {

	#region Fields

	[Header("Fields")]
	[SerializeField]	protected string m_CharacterName;
	public string characterName {
		get { return this.m_CharacterName; }
		set { this.m_CharacterName = value; }
	}

	[SerializeField]	protected float m_MoveSpeed;
	public float moveSpeed {
		get { return this.m_MoveSpeed; }
		set { this.m_MoveSpeed = value; }
	}

	[SerializeField]	protected float m_SolarPoint;
	[Info(valueName = "Solar point", valueMin = 0f, valueMax = 9999)]
	[UpdateValuePerSecond(updateMethod = "Decrease", updateValuePerSecond = 1f)]
	[UpdateValuePerInvoke(updateName = "ChargingSolar", updateMethod = "Increase", updateValuePerInvoke = 1.5f)]
	public float solarPoint {
		get { return this.m_SolarPoint; }
		set { this.m_SolarPoint = value < 0f ? 0f : value > this.m_MaxSolarPoint ? this.m_MaxSolarPoint : value; }
	}

	[SerializeField]	protected float m_MaxSolarPoint;
	public float maxSolarPoint {
		get { return this.m_MaxSolarPoint; }
		set { this.m_MaxSolarPoint = value; }
	}

	[SerializeField]	protected List<CItemData> m_Items;
	public List<CItemData> items {
		get { return this.m_Items; }
		set { this.m_Items = new List<CItemData> (value); }
	}

	#endregion

	#region Constructor

	public CCharacterData (): base() {
		this.characterName 		= "Empty name";
		this.moveSpeed 			= 5f;
		this.m_SolarPoint 		= 0f;
		this.m_MaxSolarPoint 	= 0f;
		this.m_Items 			= new List<CItemData> ();
	}

	#endregion

	#region Override

	public override string ToString ()
	{
		return string.Format ("[CCharacterData] {0} - {1} - {2}", characterName, moveSpeed, solarPoint);
	}

	#endregion

}
