using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Reflection;

[Serializable]
public class CEntityData : ScriptableObject, ISerializable {

	[Header("Entity Fields")]
	[SerializeField]	protected string m_Description;

	public CEntityData () : base() {
		
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
