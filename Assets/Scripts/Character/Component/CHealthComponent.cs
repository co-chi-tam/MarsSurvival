using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Ludiq.Reflection;

public class CHealthComponent : CComponent {

	#region Internal Class

	[System.Serializable]
	public class UnityEventFloat : UnityEvent<float> {}

	#endregion

	#region Fields

	[Header("Configs")]
	[SerializeField]	protected float m_HealthValue = 100f;
	public float healthValue {
		get { return this.m_HealthValue; }
		set { this.m_HealthValue = value < 0f ? 0f : value > this.m_MaxHealthValue ? this.m_MaxHealthValue : value; }
	}
	[SerializeField]	protected float m_MaxHealthValue = 100f;
	public float maxHealthValue {
		get { return this.m_MaxHealthValue; }
		set { this.m_MaxHealthValue = value; }
	}
	public float healthPercent {
		get { return this.m_HealthValue / this.m_MaxHealthValue; }
	}
	[SerializeField]	protected float m_ResistValue = 0f;
	public float resistValue {
		get { return this.m_ResistValue; }
		set { this.m_ResistValue = value < 0f ? 0f : value; }
	}

	[Header("Events")]
	public UnityEventFloat OnApplyDamage;
	public UnityEvent OnLowHealth;

	#endregion

	#region Main methods

	public virtual void ApplyDamage(float value) {
		var clampValue = Mathf.Clamp (value - this.resistValue, 1f, 9999f);
		this.healthValue -= clampValue;
		if (this.OnApplyDamage != null) {
			this.OnApplyDamage.Invoke (clampValue);
		}
		if (this.healthValue <= 0f) {
			if (this.OnLowHealth != null) {
				this.OnLowHealth.Invoke ();
			}
		}
	}

	#endregion

	#region Getter && Setter

	public virtual void SetHealth(float min, float max) {
		this.m_HealthValue = min;
		this.m_MaxHealthValue = max;
	}

	#endregion

}
