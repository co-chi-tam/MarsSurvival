using System;
using UnityEngine;

[Serializable]
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
