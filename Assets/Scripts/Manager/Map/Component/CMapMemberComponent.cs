using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Ludiq.Reflection;

[DisallowMultipleComponent]
public class CMapMemberComponent : MonoBehaviour {

	#region Fields

	[Header("Configs")]
	[SerializeField]	protected bool m_IsCenterMap = false;
 
	protected CMapManager m_MapManager;

	#endregion

	#region Implemetation MonoBehaviour

	protected virtual void Start ()
	{
		this.m_MapManager = CMapManager.GetInstance ();
		if (this.m_IsCenterMap) {
			this.m_MapManager.target = this.transform;
		}
	}

	#endregion

	#region Main methods

	public virtual void ApplyRandomPosition(float radius) {
		this.transform.position = this.GetRandomPosition (radius);
	}

	public virtual Vector3 GetRandomPosition(float radius) {
		return this.m_MapManager.GetRandomPosition (radius);
	}

	#endregion

}
