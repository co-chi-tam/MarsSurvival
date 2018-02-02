using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Events.Utils;
using Ludiq.Reflection;

[Serializable]
public class CToolMethodItem {

	#region Fields

	public string methodName;

	[Filter(Fields = true, Properties = true, Methods = true)]
	public UnityMember method;

	#endregion

	#region Constructor

	public CToolMethodItem ()
	{

	}

	#endregion

}
