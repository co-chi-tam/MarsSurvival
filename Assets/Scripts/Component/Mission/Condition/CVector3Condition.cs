using System;
using UnityEngine;

public class CVector3Condition : CBaseCondition {

	public Vector3 conditionV3 = Vector3.zero;

	public override int GetHashCode ()
	{
		return this.conditionV3.GetHashCode();
	}

	public override object GetValue ()
	{
		return this.conditionV3;
	}

}
