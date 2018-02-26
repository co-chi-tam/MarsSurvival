using System;
using UnityEngine;

[Serializable]
public class CMissionData : ScriptableObject {

	[SerializeField]	protected CMissionItemData m_Mission;
	public CMissionItemData mission {
		get { return this.m_Mission; }
		set { this.m_Mission = value; }
	}

}
