using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CTopSurfaceComponent : CComponent {

	#region Internal class

	[System.Serializable]
	public class UnityEventVector3: UnityEvent<Vector3> {}

	#endregion

	#region Fields

	[Header("Configs")]
	[SerializeField]	protected float m_RotationSpeed = 5f;
	[SerializeField]	protected float m_HightOffset = 0.2f;

	[Header("Target")]
	[SerializeField]	protected LayerMask m_Ground;
	[SerializeField]	protected Transform m_Top;
	[SerializeField]	protected Transform m_Bottom;

	[Header("Events")]
	public UnityEventVector3 OnEnterSurface;
	public UnityEventVector3 OnUpdateSurface;
	public UnityEvent OnOutOfSurface;

	protected Vector3 m_HitSurface = Vector3.zero;

	#endregion

	#region Implementation Component

	protected override void LateUpdate ()
	{
		base.LateUpdate ();
		if (this.m_IsActive) {
			this.UpdateStepOnGround (Time.deltaTime);
		}
	}

	#endregion

	#region Main methods

	public virtual void UpdateStepOnGround(float dt) {
		var top = this.m_Top != null ? this.m_Top.position : this.m_Transform.up;
		var bottom = this.m_Bottom != null ? this.m_Bottom.position : this.transform.position;
		RaycastHit hitInfo;
		if (Physics.Raycast (top, -Vector3.up, out hitInfo, Mathf.Infinity, this.m_Ground)) {
			// Position
			var feet = bottom;
			feet.x = this.m_Transform.position.x;
			feet.y = hitInfo.point.y + hitInfo.normal.y * this.m_HightOffset;
			feet.z = this.m_Transform.position.z;
			this.m_Transform.position = feet;
			// Rotation
			if (this.m_Transform.up != hitInfo.normal) {
				var normalGround = Quaternion.FromToRotation (Vector3.up, hitInfo.normal);
				this.m_Transform.rotation = Quaternion.Lerp (
					this.m_Transform.rotation, 
					normalGround,
					this.m_RotationSpeed * dt);
			}
			// Events
			if (hitInfo.normal != this.m_HitSurface) {
				if (this.OnEnterSurface != null) {
					this.OnEnterSurface.Invoke (hitInfo.normal);
				}
				this.m_HitSurface = hitInfo.normal;
			}
			if (this.OnUpdateSurface != null) {
				this.OnUpdateSurface.Invoke (hitInfo.normal);
			}
		} else {
			if (this.OnOutOfSurface != null) {
				this.OnOutOfSurface.Invoke ();
			}
		}
	}

	#endregion

}
