using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Events.Utils;
using Ludiq.Reflection;

[Serializable]
public class FSMMonoBaseCondition {

	#region Fields

	public string conditionName;

	[Filter(Fields = true, Properties = true)]
	public UnityMember conditionVariable;

	#endregion

	#region Constructor

	public FSMMonoBaseCondition ()
	{
		
	}

	#endregion

}
