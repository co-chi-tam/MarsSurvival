using System.Collections;
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
	[SerializeField]	protected float m_MoveSpeedThreshold = 1f;
	public float moveSpeedThreshold {
		get { return this.m_MoveSpeedThreshold; }
		set { this.m_MoveSpeedThreshold = value; }
	}
	[SerializeField]	protected float m_RotationSpeed = 5f;
	public float rotationSpeed {
		get { return this.m_RotationSpeed; }
		set { this.m_RotationSpeed = value; }
	}
	protected float m_PreviousMoveSpeed = 5f;
	[SerializeField]	protected float m_MinDistance = 0.1f;
	public float minDistance {
		get { return this.m_MinDistance; }
		set { this.m_MinDistance = value; }
	}
	public Vector3 currentPosition {
		get { return this.transform.position; }
		set { this.transform.position = value; }
	}
	public Vector3 currentRotation {
		get { return this.transform.rotation.eulerAngles; }
		set { this.transform.rotation = Quaternion.Euler (value); }
	}
	public float currentRotationAngle {
		get { return this.m_RotationAngle; }
		set { 
			this.m_RotationAngle = value; 
			var dirRotation = Quaternion.AngleAxis (this.m_RotationAngle, Vector3.up);
			this.m_Transform.rotation = dirRotation;
		}
	}

	[SerializeField]	protected Vector3 m_TargetPosition;
	public Vector3 targetPosition {
		get { return this.m_TargetPosition; }
		set { 
			value.y = this.transform.position.y;
			this.m_TargetPosition = value; 
		}
	}

	public override Transform myTransform {
		get { return base.myTransform; }
		set { base.myTransform = value; }
	}

	[Header("Events")]
	public UnityEvent OnNearestTarget;
	public UnityEvent OnMove;

	protected Vector3 m_MovePoint;
	protected float m_RotationAngle;
	protected Vector3 m_DirNormal;

	#endregion

	#region Implementation Component

	protected override void Awake ()
	{
		base.Awake ();
		this.m_PreviousMoveSpeed = this.m_MoveSpeed;
		this.SetupPosition (this.transform.position, Quaternion.identity);
	}

	protected override void Update ()
	{
		base.Update ();
		if (this.m_IsActive) {
			this.SetupMove (Time.deltaTime);
		}
		this.UpdateStepOnGround (Time.deltaTime);
	}

	#endregion

	#region Main methods

	public virtual void SetupPosition(Vector3 position, Quaternion rotation) {
		this.transform.position = this.m_MovePoint = this.m_TargetPosition = position;
		this.transform.rotation = rotation;
	}

	public virtual void UpdateStepOnGround(float dt) {
		this.m_DirNormal = Vector3.up;
		var top = this.m_Top != null ? this.m_Top.position : this.m_Transform.up;
		var bottom = this.m_Bottom != null ? this.m_Bottom.position : this.m_Transform.position;
		RaycastHit hitInfo;
		if (Physics.Raycast (top, -Vector3.up, out hitInfo, Mathf.Infinity, this.m_Ground)) {
			// Position
			var feet = bottom;
			feet.x = this.m_MovePoint.x;
			feet.y = Mathf.Lerp (feet.y, hitInfo.point.y, this.m_MoveSpeed * dt);
			feet.z = this.m_MovePoint.z;
			this.m_MovePoint = feet;
			// Rotation
			this.m_DirNormal = hitInfo.normal;
		}
		// Position
		this.m_Transform.position = this.m_MovePoint;
		// Rotation
//		var dirRotation = Quaternion.AngleAxis (this.m_RotationPoint, this.m_DirNormal);
//		var normalGround = Quaternion.FromToRotation (Vector3.up, this.m_DirNormal);
//		var combineRot = dirRotation * normalGround;
//		this.m_Transform.rotation = Quaternion.Lerp (
//											this.m_Transform.rotation, 
//											combineRot,
//											this.m_RotationSpeed * dt);
		var dirRotation = Quaternion.AngleAxis (this.m_RotationAngle, Vector3.up);
		this.m_Transform.rotation = Quaternion.Lerp (
			this.m_Transform.rotation, 
			dirRotation,
			this.m_RotationSpeed * dt);
	}

	public virtual void Look (Vector3 position) {
		var direction = position - this.m_Transform.position;
		this.m_RotationAngle = Mathf.Atan2 (direction.x, direction.z) * Mathf.Rad2Deg;
	}

	public virtual void SetupMove(float dt) {
		this.m_TargetPosition.y = this.m_Transform.position.y;
		var direction = this.m_TargetPosition - this.m_Transform.position;
		if (direction.sqrMagnitude > this.m_MinDistance * this.m_MinDistance * this.m_MoveSpeed) {
			// Position
			this.m_MovePoint = this.m_Transform.position + direction.normalized 
				* (this.m_MoveSpeed * this.m_MoveSpeedThreshold) 
				* dt;
			// Rotation
			this.m_RotationAngle = Mathf.Atan2 (direction.x, direction.z) * Mathf.Rad2Deg;
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
		return this.IsNearestTarget(this.m_TargetPosition);
	}

	public virtual bool IsNearestTarget(Vector3 target) {
		var direction = target - this.m_Transform.position;
		return direction.sqrMagnitude < this.m_MinDistance * this.m_MinDistance * this.m_MoveSpeed;
	}

	public override void Reset ()
	{
		base.Reset ();
		this.m_TargetPosition = this.transform.position;
		this.m_MoveSpeed = this.m_PreviousMoveSpeed;
	}

	#endregion

}
