using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTopSurfaceComponent : CComponent {

	#region Fields

	[Header("Configs")]
	[SerializeField]	protected float m_RotationSpeed = 5f;

	[Header("Target")]
	[SerializeField]	protected LayerMask m_Ground;
	[SerializeField]	protected Transform m_Top;
	[SerializeField]	protected Transform m_Bottom;

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
		if (this.m_Top != null && this.m_Bottom != null) {
			RaycastHit hitInfo;
			if (Physics.Raycast (this.m_Top.position, -Vector3.up, out hitInfo, Mathf.Infinity, this.m_Ground)) {
				// Position
				var feet = this.m_Bottom.position;
				feet.x = this.m_Transform.position.x;
				feet.y = hitInfo.point.y;
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

			}
		}

	}

	#endregion

}
