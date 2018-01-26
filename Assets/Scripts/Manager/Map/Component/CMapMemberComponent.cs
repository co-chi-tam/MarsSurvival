using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Ludiq.Reflection;

[DisallowMultipleComponent]
public class CMapMemberComponent : MonoBehaviour {

	[Header("Configs")]
	[SerializeField]	protected bool m_IsCenterMap = false;
 
	protected CMapManager m_MapManager;

	protected virtual void Start ()
	{
		this.m_MapManager = CMapManager.GetInstance ();
		if (this.m_IsCenterMap) {
			this.m_MapManager.target = this.transform;
		}
	}

	public virtual void ApplyRandomPosition(float radius) {
		this.transform.position = this.GetRandomPosition (radius);
	}

	public virtual Vector3 GetRandomPosition(float radius) {
		return this.m_MapManager.GetRandomPosition (radius);
	}

}
