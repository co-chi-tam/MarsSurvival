using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBaseCondition : ScriptableObject {

	public string conditionDetail;

	public override bool Equals (object other)
	{
		return base.Equals (other);
	}

	public override int GetHashCode ()
	{
		return base.GetHashCode ();
	}

	public virtual object GetValue() {
		return null;
	}

}
