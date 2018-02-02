using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Events.Utils;
using Ludiq.Reflection;

[RequireComponent (typeof(CMoveComponent))]
public class CMoveAroundComponent : CComponent {

	#region Internal class

	[Serializable]
	public class UnityEventVector3 : UnityEvent<Vector3> {}

	#endregion

	#region Fields

	[Header ("Configs")]
	[SerializeField]	protected Transform m_Center;
	public Transform center {
		get { return this.m_Center; }
		set { this.m_Center = value; }
	}
	[SerializeField]	protected float m_Radius = 20f;
	public float radius {
		get { return this.m_Radius; }
		set { this.m_Radius = value; }
	}

	[Header("Events")]
	public UnityEventVector3 OnVector3Around;

	#endregion

	#region Main methods

	public virtual void ApplyAround() {
		if (this.m_IsActive == false)
			return;
		var randomPosition = this.GetRandomPosition (this.m_Radius);
		if (this.OnVector3Around != null) {
			this.OnVector3Around.Invoke (randomPosition);
		}
	}

	public virtual Vector3 GetRandomPosition (float radius) {
		var center = this.transform.position;
		if (this.m_Center != null) {
			center = this.m_Center.position;
		} 
		var randomVector = UnityEngine.Random.insideUnitCircle;
		var randomPosition = new Vector3 (
			center.x + randomVector.x * radius,
			center.y,
			center.z + randomVector.y * radius
		);
		return randomPosition;
	}

	#endregion

}
