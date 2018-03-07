using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDisasterEntity : CGameEntity {

	#region Fields

	[Header ("Configs")]
	[SerializeField]	protected float m_MapItemPercent = 100f;
	[SerializeField]	protected bool m_IsShowing = false;

	public bool IsActiveInMap {
		get { 
			this.m_IsShowing = UnityEngine.Random.Range (0f, 100f) <= this.m_MapItemPercent;
			return this.m_IsShowing;
		}
	}

	#endregion

	#region Main methods

	public virtual void ApplyRenderer() {
		this.gameObject.SetActive (true);
	}	

	#endregion

}
