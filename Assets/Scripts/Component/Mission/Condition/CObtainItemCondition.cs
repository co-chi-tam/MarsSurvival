using System;
using UnityEngine;

public class CObtainItemCondition : CBaseCondition {

	public string conditionStr = string.Empty;

	public override int GetHashCode ()
	{
		return this.conditionStr.GetHashCode();
	}

	public override object GetValue ()
	{
		return this.conditionStr;
	}

}
