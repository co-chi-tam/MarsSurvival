using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class CEntityData : ScriptableObject {

	[Header("Entity Fields")]
	[SerializeField]	protected string m_Description;

	public CEntityData () : base() {
		
	}

}
