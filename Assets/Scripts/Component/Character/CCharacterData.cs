using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class CCharacterData : ScriptableObject {

	#region Internal Class

	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, 
		AllowMultiple = true)]
	public class MarkerValueAttribute: Attribute {

		public string valueName {
			get;
			set;
		}

		public MarkerValueAttribute ()
		{
			
		}

		public override string ToString ()
		{
			return string.Format ("[MarkerValueAttribute: valueName={0}]", valueName);
		}

	}

	#endregion

	#region Fields

	[Header("Fields")]
	[MarkerValue(valueName = "Character Name")]
	public string characterName;
	public float moveSpeed;
	public float currentSolarPoint;
	public float maxSolarPoint;

	#endregion

	#region Constructor

	public CCharacterData (): base() {

	}

	#endregion


}
