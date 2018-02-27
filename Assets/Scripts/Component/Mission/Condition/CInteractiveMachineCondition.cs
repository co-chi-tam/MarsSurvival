using System;
using UnityEngine;

public class CInteractiveMachineCondition : CBaseCondition{

	public string conditionName = string.Empty;

	public override int GetHashCode ()
	{
		return this.conditionName.GetHashCode();
	}

	public override object GetValue ()
	{
		return this.conditionName;
	}

}
