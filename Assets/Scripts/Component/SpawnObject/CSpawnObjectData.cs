using System;
using UnityEngine;

[Serializable]
public class CSpawnObjectData {

	[Header("Fields")]
	[SerializeField]	protected string m_ObjectName;
	public string objectName {
		get { return this.m_ObjectName; }
		set { this.m_ObjectName = value; }
	}

	[SerializeField]	protected GameObject m_ObjectPrefab;
	public GameObject objectPrefab {
		get { return this.m_ObjectPrefab; }
		set { this.m_ObjectPrefab = value; }
	}

}
