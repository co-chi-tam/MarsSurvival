using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Reflection;

[Serializable]
public class CItemData : CEntityData {

	#region Fields

	[Header("Item Fields")]
	public string itemName 		= string.Empty;
	public string itemDisplayName	= string.Empty;
	public string avatarPath 	= string.Empty;
	public string modelPath 	= string.Empty;
	public int amount 			= 0;

	#endregion

	#region Constructor

	public CItemData (): base() {
		
	}

	public CItemData (SerializationInfo info, StreamingContext context) : base(info, context) {

	}

	#endregion

	#region Object

	public override bool Equals (object other)
	{
		return base.Equals (other);
	}

	public override int GetHashCode ()
	{
		int hash = 13;
		hash = (hash * 7) + itemName.GetHashCode();
		hash = (hash * 7) + itemDisplayName.GetHashCode();
		hash = (hash * 7) + avatarPath.GetHashCode();
		hash = (hash * 7) + modelPath.GetHashCode();
		return hash;
	}

	public override string ToString ()
	{
		return string.Format ("[CItemData]");
	}

	#endregion

}

