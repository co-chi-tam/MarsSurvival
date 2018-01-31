using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Reflection;

[Serializable]
public class CSaveObjectData {

	#region Fields

	[Header("Movable Fields")]
	[SerializeField]	protected string m_Position = "0,0,0";
	[Info (valueName = "Position", valueMin = "", valueMax = "")]
	public string position {
		get { return this.m_Position; }
		set { this.m_Position = value; }
	}
	[SerializeField]	protected string m_Rotation = "0,0,0";
	public string rotation {
		get { return this.m_Rotation; }
		set { this.m_Rotation = value; }
	}

	#endregion

	#region Constructor

	public CSaveObjectData () {
		
	}

	#endregion

}
