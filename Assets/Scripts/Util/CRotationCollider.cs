using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRotationCollider : MonoBehaviour {

	[SerializeField]	protected Transform m_Target;
	[SerializeField]	protected Vector3 m_Center = new Vector3 (0f, 2f, 0f);
	[SerializeField]	protected float m_Speed = 20f;
	[SerializeField]	protected float m_Radius = 0.01f;
	[SerializeField]	protected LayerMask m_LayerMask;

	protected int m_MaximumCollider = 20;
	protected Collider[] m_Colliders;

	protected virtual void Awake() {
		this.m_Colliders = new Collider[this.m_MaximumCollider];
	}

	protected virtual void LateUpdate() {
		this.OnDetectCollider ();
	}

	public virtual void OnDetectCollider() {
		var colliderCount = Physics.OverlapSphereNonAlloc (
			this.m_Target.position, 
			this.m_Radius, 
			this.m_Colliders, 
			this.m_LayerMask);
		if (colliderCount > 0) {
			var closestPoint = this.m_Colliders [0].transform.position + this.m_Center;
			var direction = closestPoint - this.m_Target.position;
			this.UpdateRotation (Quaternion.LookRotation (-direction));
		} else {
			this.UpdateRotation (Quaternion.Euler (Vector3.zero));
		}
	}

	protected virtual void UpdateRotation(Quaternion value) {
		this.m_Target.rotation = Quaternion.Lerp 
			(
				this.m_Target.rotation,
				value,
				Time.deltaTime * this.m_Speed
			);
	}

}
