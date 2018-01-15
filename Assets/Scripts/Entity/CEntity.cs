using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEntity : MonoBehaviour {

	#region Fields

	[Header("Components")]
	[SerializeField]	protected CComponent[] m_Components;

	[SerializeField]	protected bool m_IsActive;
	public virtual bool isActive {
		get { return this.m_IsActive; }
		set { this.m_IsActive = value; }
	}

	#endregion

	#region Implementation MonoBehaviour

	protected virtual void Awake() {
		this.m_Components = this.GetComponents<CComponent> ();
	}

	protected virtual void Start() {

	}

	protected virtual void FixedUpdate() {

	}

	protected virtual void Update() {
		if (this.m_IsActive == false)
			return;
		for (int i = 0; i < this.m_Components.Length; i++) {
			this.m_Components [i].UpdateFromOwner (Time.deltaTime);
		}
	}

	protected virtual void LateUpdate() {

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
