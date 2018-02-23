using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Reflection;

[Serializable]
public class CGameEntityData : CEntityData {

	#region Fields

	[SerializeField]	protected string m_Description = string.Empty;
	public string description {
		get { return this.m_Description; }
		set { this.m_Description = value; }
	}
	[SerializeField]	protected string m_AvatarPath = string.Empty;
	public string avatarPath {
		get { return this.m_AvatarPath; }
		set { this.m_AvatarPath = value; }
	}
	[SerializeField]	protected string m_ModelPath = string.Empty;
	public string modelPath {
		get { return this.m_ModelPath; }
		set { this.m_ModelPath = value; }
	}

	#endregion

	#region Constructor

	public CGameEntityData () : base() {
		this.m_Description = string.Empty;
		this.m_AvatarPath = string.Empty;
		this.m_ModelPath = string.Empty;
	}

	public CGameEntityData (SerializationInfo info, StreamingContext context) : base (info, context)
	{
		
	}

	#endregion

	#region Getter && Setter

	public override bool Equals (object other)
	{
		return base.Equals (other);
	}

	public override int GetHashCode ()
	{
		int hash = 13;
		hash = (hash * 7) + description.GetHashCode();
		hash = (hash * 7) + avatarPath.GetHashCode();
		hash = (hash * 7) + modelPath.GetHashCode();
		return hash;
	}

	public override void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		base.GetObjectData (info, context);
	}

	#endregion

}
