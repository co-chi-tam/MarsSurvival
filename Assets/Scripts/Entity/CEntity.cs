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
	protected int m_AnimationInt;
	public int animationInt {
		get { return this.m_AnimationInt; }
		set { this.m_AnimationInt = value; }
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

	public virtual void SetAnimation(int value) {
		this.m_AnimationInt = value;
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
