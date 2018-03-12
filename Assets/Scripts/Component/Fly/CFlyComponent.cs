using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFlyComponent : CComponent {

	#region Fields

	[Header("Configs")]
	[SerializeField]	protected float m_AmbientSpeed = 10f;
	public float AmbientSpeed {
		get { return this.m_AmbientSpeed; }
		set { this.m_AmbientSpeed = value; }
	}

	[SerializeField]	protected float m_RotationSpeed = 20f;
	public float RotationSpeed {
		get { return this.m_RotationSpeed; }
		set { this.m_RotationSpeed = value; }
	}

	[Range (-1f, 1f)]
	[SerializeField]	protected float m_Pitch = 0f;
	public float pitch {
		get { return this.m_Pitch; }
		set { this.m_Pitch = value; }
	}

	[Range (-1f, 1f)]
	[SerializeField]	protected float m_Yaw = 0f;
	public float yaw {
		get { return this.m_Yaw; }
		set { this.m_Yaw = value; }
	}

	[Range (-1f, 1f)]
	[SerializeField]	protected float m_Roll = 0f;
	public float roll {
		get { return this.m_Roll; }
		set { this.m_Roll = value; }
	}

	protected Vector3 m_FlyVector3 = new Vector3 (0f, 0f, 0f);
	public Vector3 flyVector3 {
		get { 
			this.m_FlyVector3.x = this.m_Yaw;
			this.m_FlyVector3.y = this.m_Pitch;
			this.m_FlyVector3.z = this.m_Roll;
			return this.m_FlyVector3;
		} 
		set { 
			this.m_Yaw 	= value.x;
			this.m_Pitch = value.z;
			this.m_Roll = value.x;
			this.m_FlyVector3 = value;
		}
	}

	#endregion

	#region Implementation Component

	protected override void Start ()
	{
		base.Start ();
	}

	protected virtual void Update()
	{        
		base.Update ();
		if (this.m_IsActive == false)
			return;
		this.FlyForward (Time.deltaTime);
	}

	#endregion

	#region Main methods

	public virtual void FlyForward(float dt) {
		var addRot = Quaternion.identity;

		var pitchVal = this.m_Pitch * (dt * this.m_RotationSpeed);
		var yawVal = this.m_Yaw * (dt * this.m_RotationSpeed);
		var rollVal = this.m_Roll * (dt * this.m_RotationSpeed);
		addRot.eulerAngles = new Vector3 (-pitchVal, yawVal, -rollVal);

		this.m_Transform.rotation *= addRot;
		Vector3 addPos = Vector3.forward;
		addPos = this.m_Transform.rotation * addPos;
		this.m_Transform.position += addPos * (dt * this.m_AmbientSpeed);
	}

	#endregion

}
