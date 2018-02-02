using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Ludiq.Reflection;

[RequireComponent(typeof(CPhysicDetectComponent))]
public class CEntityDetectComponent : CComponent {

	#region Internal class

	[System.Serializable]
	public class UnityEventEntity : UnityEvent<CEntity> {}

	#endregion

	#region Fields

	[Header("Configs")]
	[Tooltip("Current entity nearest.")]
	[SerializeField]	protected CEntity m_NearestEntity;
	public CEntity currentEntity {
		get { return this.m_NearestEntity; }
		set { this.m_NearestEntity = value; }
	}
	public bool isDetected {
		get { return this.m_NearestEntity != null; }
	}
	public Vector3 currentEntityPosition {
		get { 
			if (this.m_NearestEntity == null)
				return Vector3.zero;
			return this.m_NearestEntity.myTransform.position;
		}
	}

	[Header("Events")]
	public UnityEventEntity OnEntityDetected;
	public UnityEventEntity OnFree;

	protected CPhysicDetectComponent m_PhysicDetectComponent;

	#endregion

	#region Implementation Component

	protected override void Awake ()
	{
		base.Awake ();
		this.m_PhysicDetectComponent = this.GetComponent<CPhysicDetectComponent> ();
		this.InitDetect ();
	}

	#endregion

	#region Main methods

	protected virtual void InitDetect() {
		this.m_PhysicDetectComponent.OnDetected.AddListener (() => {
			this.DetectEntity();
		});
		this.m_PhysicDetectComponent.OnFree.AddListener (() => {
			this.FreeEntity();
		});
	}

	public virtual void DetectEntity() {
		if (this.m_IsActive == false)
			return;
		var detectCount = this.m_PhysicDetectComponent.colliderCount;
		var minDistance = float.MaxValue;
		CEntity currentEntity = null;
		if (detectCount != 0) {
			for (int i = 0; i < detectCount; i++) {
				var collider = this.m_PhysicDetectComponent.sampleColliders [i];
				var entity = collider.GetComponent<CEntity> ();
				if (entity != null) {
					var direction = entity.transform.position - this.m_PhysicDetectComponent.detectTransform.position;
					if (direction.sqrMagnitude < minDistance) {
						currentEntity = entity;
						minDistance = direction.sqrMagnitude;
					}
				}
			}
			if (this.OnEntityDetected != null) {
				this.OnEntityDetected.Invoke (currentEntity);
			}
		}
		this.m_NearestEntity = currentEntity;
	}

	public virtual void FreeEntity() {
		if (this.m_IsActive == false)
			return;
		var detectCount = this.m_PhysicDetectComponent.colliderCount;
		if (detectCount == 0) {
			if (this.OnFree != null) {
				this.OnFree.Invoke (null);
			}
			this.m_NearestEntity = null;
		}
	}

	#endregion

}
