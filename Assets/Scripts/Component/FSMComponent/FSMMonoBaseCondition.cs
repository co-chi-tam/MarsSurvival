using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Events.Utils;
using Ludiq.Reflection;

[Serializable]
public class FSMMonoBaseCondition {

	public string conditionName;

	[Filter(Fields = true, Properties = true)]
	public UnityMember conditionVariable;

	public FSMMonoBaseCondition ()
	{
		
	}

}
