using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Reflection;

[Serializable]
public class CEntityData : ScriptableObject, ISerializable {

	[Header("Entity Fields")]
	[SerializeField]	protected string m_EntityName = string.Empty;
	public string entityName {
		get { return this.m_EntityName; }
		set { this.m_EntityName = value; }
	}
	[SerializeField]	protected string m_EntityDisplayName = string.Empty;
	public string entityDisplayName {
		get { return this.m_EntityDisplayName; }
		set { this.m_EntityDisplayName = value; }
	}
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

	public CEntityData () : base() {
		this.m_Description = string.Empty;
		this.m_AvatarPath = string.Empty;
		this.m_ModelPath = string.Empty;
	}

	public CEntityData (SerializationInfo info, StreamingContext context)
	{
		Type type = Type.GetType((string)info.GetValue("ScriptableType", typeof(string)));
		if (type == null)
			return;
		var newData = ScriptableObject.CreateInstance(type);
		foreach (FieldInfo field in newData.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.Instance))
		{
			field.SetValue(this, info.GetValue(field.Name, field.FieldType));
		}
	}

	// Implement this method to serialize data. The method is called on serialization.
	public void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("ScriptableType", this.GetType().AssemblyQualifiedName, typeof(string));
		foreach(FieldInfo field in this.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.Instance))
		{
			info.AddValue(field.Name, field.GetValue(this), field.FieldType);
		}
	}
}
