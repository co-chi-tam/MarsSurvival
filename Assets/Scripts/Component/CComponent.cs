using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CComponent : MonoBehaviour {

	#region Fields

	protected bool m_IsActive = false;
	public bool isActive {
		get { return this.m_IsActive; }
		set { this.m_IsActive = value; }
	}

	#endregion

	#region Implementation MonoBehaviour

	protected virtual void Awake() {
		
	}

	protected virtual void Start() {
	
	}

	protected virtual void FixedUpdate() {
		
	}

	protected virtual void Update() {
	
	}

	protected virtual void LateUpdate() {
		
	}

	#endregion

	#region Main methods

	public virtual void Init() {
		
	}

	public virtual void UpdateFromOwner(float dt) {
		
	}

	public virtual void Reset() {
	
	}

	#endregion

}
