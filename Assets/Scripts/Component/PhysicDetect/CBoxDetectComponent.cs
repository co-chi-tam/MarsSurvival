using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBoxDetectComponent : CPhysicDetectComponent {

	#region Fields

	[Header("Box size")]
	[SerializeField]	protected Vector3 m_Size;

	#endregion

	#region Implementation Component

#if UNITY_EDITOR

	protected override void OnDrawGizmosSelected() {
		Gizmos.color = this.m_SphereColor;
		if (this.m_DetectTransform != null) {
			Gizmos.DrawWireCube (this.m_DetectTransform.position, this.m_Size);
		} else {
			Gizmos.DrawWireCube (this.transform.position, this.m_Size);
		}
	}

#endif

	#endregion

	#region Main methods

	public override void DetectObjects() {
		this.m_ColliderCount = Physics.OverlapBoxNonAlloc (
			this.m_DetectTransform.position, 
			this.m_Size / 2f,
			this.m_SampleColliders,
			Quaternion.identity,
			this.m_DetectLayerMask);
		if (this.m_ColliderCount != 0) {
			// DETECTED
			if (this.OnDetected != null) {
				this.OnDetected.Invoke ();				
			}
		} else {
			// FREE
			if (this.OnFree != null) {
				this.OnFree.Invoke ();
			}
		}
		// CHANGED
		if (this.m_PreviousCount != this.m_ColliderCount) {
			if (this.OnChanged != null) {
				this.OnChanged.Invoke ();				
			}
			this.m_PreviousCount = this.m_ColliderCount;
		}
	}

	#endregion

}
