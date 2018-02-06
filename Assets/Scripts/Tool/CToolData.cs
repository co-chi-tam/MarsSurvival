using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Reflection;
using UnityEngine;

public class CToolData : CGameEntityData {

	#region Fields

	[Header("Tool Fields")]
	[SerializeField]	protected string m_ToolMethod;
	public string toolMethod {
		get { return this.m_ToolMethod; }
		set { this.m_ToolMethod = value; }
	}
	[SerializeField]	protected float m_ToolDamage = 1f;
	public float toolDamage {
		get { return this.m_ToolDamage; }
		set { this.m_ToolDamage = value; }
	}

	#endregion

	#region Constructor

	public CToolData () : base()
	{
		this.m_ToolMethod = "Empty";
	}

	public CToolData (SerializationInfo info, StreamingContext context) : base (info, context)
	{

	}

	#endregion

	#region Override

	public override int GetHashCode ()
	{
		return base.GetHashCode ();
	}

	public override bool Equals (object other)
	{
		if (other is CToolData) {
			var otherData = other as CToolData;
			return otherData.entityName == this.entityName;
		}
		return base.Equals (other);
	}

	#endregion

}
