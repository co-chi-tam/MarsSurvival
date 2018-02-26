using System;
using UnityEngine;

public class CObtainItemCondition : CBaseCondition {

	[SerializeField]	protected string m_Condition = string.Empty;
	public string conditionStr {
		get { return this.m_Condition; }
		protected set { this.m_Condition = value; }
	}

	public override bool Equals (object other)
	{
		return base.Equals (other);
	}

	public override int GetHashCode ()
	{
		return this.m_Condition.GetHashCode();
	}

	public override object GetValue ()
	{
		return this.m_Condition;
	}

}
