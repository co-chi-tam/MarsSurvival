using System;
using UnityEngine;

public class CVector3Condition : CBaseCondition {

	[SerializeField]	protected Vector3 m_Condition = Vector3.zero;
	public Vector3 conditionV3 {
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
