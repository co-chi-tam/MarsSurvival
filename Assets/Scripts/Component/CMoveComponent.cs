﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CMoveComponent : CComponent {

	#region Fields

	[Header("Target")]
	[SerializeField]	protected LayerMask m_Ground = -1;
	[SerializeField]	protected Transform m_Top;
	[SerializeField]	protected Transform m_Bottom;

	[Header ("Value")]
	[SerializeField]	protected float m_MoveSpeed = 5f;
	public float moveSpeed {
		get { return this.m_MoveSpeed; }
		set { this.m_MoveSpeed = value; }
	}
	[SerializeField]	protected float m_RotationSpeed = 5f;
	public float rotationSpeed {
		get { return this.m_RotationSpeed; }
		set { this.m_RotationSpeed = value; }
	}
	protected float m_PreviousMoveSpeed;
	[SerializeField]	protected float m_MinDistance = 0.1f;
	public Vector3 currentPosition {
		get { 
			return this.transform.position; 
		}
		set { 
			this.transform.position = value;
		}
	}
	[SerializeField]	protected Vector3 m_TargetPosition;
	public Vector3 targetPosition {
		get { return this.m_TargetPosition; }
		set { this.m_TargetPosition = value; }
	}
	[SerializeField]	protected bool m_AutoMove = true;
	public bool autoMove {
		get { return this.m_AutoMove; }
		set { this.m_AutoMove = value; }
	}

	[Header("Events")]
	public UnityEvent OnNearestTarget;
	public UnityEvent OnMove;

	protected Transform m_Transform;
	protected Vector3 m_MovePoint;
	protected float m_RotationPoint;

	#endregion

	#region Implementation Component

	protected override void Awake ()
	{
		base.Awake ();
		this.m_Transform = this.transform;
		this.m_PreviousMoveSpeed = this.m_MoveSpeed;
		this.m_TargetPosition = this.transform.position;
	}

	protected override void Update ()
	{
		base.Update ();
		if (this.m_AutoMove) {
			this.SetupMove (Time.deltaTime);
		}
	}

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
		var dirNormal = Vector3.up;
		if (this.m_Top != null && this.m_Bottom != null) {
			RaycastHit hitInfo;
			if (Physics.Raycast (this.m_Top.position, -Vector3.up, out hitInfo, Mathf.Infinity, this.m_Ground)) {
				// Position
				var feet = this.m_Bottom.position;
				feet.x = this.m_MovePoint.x;
				feet.y = hitInfo.point.y;
				feet.z = this.m_MovePoint.z;
				this.m_MovePoint = feet;
				// Rotation
				dirNormal = hitInfo.normal;
			}
		}
		// Position
		this.m_Transform.position = this.m_MovePoint;
		// Rotation
		if (dirNormal != Vector3.up) {
			var dirRotation = Quaternion.AngleAxis (this.m_RotationPoint, dirNormal);
			var normalGround = Quaternion.FromToRotation (Vector3.up, dirNormal);
			var combineRot = dirRotation * normalGround;
			this.m_Transform.rotation = Quaternion.Lerp (
											this.m_Transform.rotation, 
											combineRot,
											this.m_RotationSpeed * dt);
		} else {
			this.m_Transform.rotation = Quaternion.Lerp (
											this.m_Transform.rotation, 
											Quaternion.AngleAxis (this.m_RotationPoint, dirNormal),
											this.m_RotationSpeed * dt);
		}

	}

	public virtual void SetupMove(float dt) {
		var direction = this.m_TargetPosition - this.m_Transform.position;

		if (direction.sqrMagnitude > this.m_MinDistance * this.m_MinDistance * this.m_MoveSpeed) {
			// Position
			this.m_MovePoint = this.m_Transform.position + direction.normalized * this.m_MoveSpeed * dt;
			// Rotation
			this.m_RotationPoint = Mathf.Atan2 (direction.x, direction.z) * Mathf.Rad2Deg;
			// Events
			if (this.OnMove != null) {
				this.OnMove.Invoke ();
			}
		} else {
			// Events
			if (this.OnNearestTarget != null) {
				this.OnNearestTarget.Invoke ();
			}
		}
	}

	public virtual bool IsNearestTarget() {
		var direction = this.m_TargetPosition - this.m_Transform.position;
		return direction.sqrMagnitude <= this.m_MinDistance * this.m_MinDistance * this.m_MoveSpeed;
	}

	public override void Reset ()
	{
		base.Reset ();
		this.m_TargetPosition = this.m_Transform.position;
		this.m_MoveSpeed = this.m_PreviousMoveSpeed;
	}

	#endregion

}
