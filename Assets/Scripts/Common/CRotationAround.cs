using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRotationAround : MonoBehaviour {

	[SerializeField]	protected float m_Speed = 0.1f;
	[SerializeField]	protected Vector3 m_Point = new Vector3 (0f, 1f, -10f);
	[SerializeField]	protected Vector3 m_Pivot = new Vector3 (0f, 1f, 0f);

	protected Transform m_Transform;

	protected virtual void Awake() {
		this.m_Transform = this.transform;
	}

	protected virtual void Update() {
		this.m_Transform.RotateAround (this.m_Point, this.m_Pivot, this.m_Speed);
	}

}
