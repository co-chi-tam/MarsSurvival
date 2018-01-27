using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Reflection;

[Serializable]
public class CMovableEntityData : CEntityData {

	#region Fields

	[Header("Movable Fields")]
	[SerializeField]	protected string m_Position = "0,0,0";
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

	public CMovableEntityData (): base() {
		
	}

	public CMovableEntityData (SerializationInfo info, StreamingContext context) : base(info, context) {

	}

	#endregion

	#region ToString

	public override string ToString ()
	{
		return string.Format ("[CMovableEntityData: position={0}, rotation={1}]", position, rotation);
	}

	#endregion

}
