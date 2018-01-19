using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent()]
public class CEntity : MonoBehaviour {

	#region Fields

	[Header("Components")]
	[SerializeField]	protected CComponent[] m_Components = new CComponent[0];

	[SerializeField]	protected bool m_IsActive;
	public virtual bool isActive {
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
		if (this.m_IsActive == false)
			return;
		
	}

	protected virtual void LateUpdate() {

	}

	#endregion

	#region Main methods

	protected virtual void UpdateComponents() {
		for (int i = 0; i < this.m_Components.Length; i++) {
			this.m_Components [i].UpdateFromOwner (Time.deltaTime);
		}
	}

	#endregion

	#region Getter && Setter

	public virtual T GetGameComponent<T>() where T : CComponent {
		for (int i = 0; i < this.m_Components.Length; i++) {
			if (this.m_Components [i].GetType () == typeof(T))
				return this.m_Components [i] as T;
		}
		return default(T);
	}

	public virtual void SetActive(bool value) {
		this.m_IsActive = value;
	}

	public virtual bool GetActive() {
		return this.m_IsActive;
	}

	#endregion

}
