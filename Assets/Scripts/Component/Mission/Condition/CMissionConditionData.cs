﻿using System;
using UnityEngine;

[Serializable]
public class CMissionConditionData {

	[SerializeField]	protected string m_ConditionMethod;
	public string conditionMethod {
		get { return this.m_ConditionMethod; }
		set { this.m_ConditionMethod = value; }
	}

	[SerializeField]	protected ScriptableObject m_ConditionValue;
	public ScriptableObject conditionValue {
		get { return this.m_ConditionValue; }
		set { this.m_ConditionValue = value; }
	}

}
