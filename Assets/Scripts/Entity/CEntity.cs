using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent()]
public class CEntity : MonoBehaviour {

	#region Fields

	[Header("Components")]
	[SerializeField]	protected CComponent[] m_Components = new CComponent[0];

	[Header("Entity")]
	[SerializeField]	protected bool m_IsActive;
	public virtual bool IsActive {
		get { return this.m_IsActive; }
		set { this.m_IsActive = value; }
	}

	protected Transform m_Transform;
	public Transform myTransform {
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
		if (this.m_IsActive == false)
			return;
		
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

	protected virtual void UpdateComponents() {
		for (int i = 0; i < this.m_Components.Length; i++) {
			this.m_Components [i].UpdateFromOwner (Time.deltaTime);
		}
	}

	#endregion

	#region Getter && Setter

	public virtual T FindGameComponent<T>(Transform value) where T : CComponent {
		var childComponent = value.GetComponent<T> ();
		if (childComponent != null)
			return childComponent;
		var childCount = value.childCount;
		for (int i = 0; i < childCount; i++) {
			var child = value.GetChild (i);
			childComponent = child.GetComponent<T> ();
			if (childComponent != null) {
				return childComponent;
			} else {
				this.FindGameComponent<T> (child);		
			}
		}
		return default (T);
	}

	public virtual T GetGameComponent<T>() where T : CComponent {
		for (int i = 0; i < this.m_Components.Length; i++) {
			if (this.m_Components [i] == null)
				continue;
			var component = this.m_Components [i].GetComponent<T> ();
			if (component != null) {
				return component as T;
			}
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

	#region Implementation Object

	public override bool Equals (object other)
	{
		return base.Equals (other);
	}

	public override int GetHashCode ()
	{
		return base.GetHashCode ();
	}

	#endregion

}
