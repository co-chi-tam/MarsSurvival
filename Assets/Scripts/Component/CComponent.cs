using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CComponent : MonoBehaviour {

	#region Fields

	[Header("Configs")]
	[SerializeField]	protected bool m_IsActive = true;
	public bool isActive {
		get { return this.m_IsActive; }
		set { this.m_IsActive = value; }
	}

	protected Transform m_Transform;
	public virtual Transform myTransform {
		get { return this.m_Transform; }
		set { this.m_Transform = value; }
	}

	#endregion

	#region Implementation MonoBehaviour

	protected virtual void Awake() {
		this.m_Transform = this.transform;
	}

	protected virtual void Start() {
	
	}

	protected virtual void FixedUpdate() {
		
	}

	protected virtual void Update() {
	
	}

	protected virtual void LateUpdate() {
		
	}

	protected virtual void OnDestroy() {
		
	}

	protected virtual void OnApplicationQuit() {
	
	}

	protected virtual void OnApplicationFocus(bool value) {
	
	}

	protected virtual void OnApplicationPause(bool value) {
	
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

	#region Getter && Setter

	public virtual void SetActive(bool value) {
		this.m_IsActive = value;
	}

	public virtual bool GetActive() {
		return this.m_IsActive;
	}

	#endregion

}
