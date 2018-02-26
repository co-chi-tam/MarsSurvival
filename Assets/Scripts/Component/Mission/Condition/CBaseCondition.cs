using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBaseCondition : ScriptableObject {

	[SerializeField]	protected string m_ConditionDetail;
	public string conditionDetail {
		get { return this.m_ConditionDetail; }
		set { this.m_ConditionDetail = value; }
	}

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
