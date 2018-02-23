using System;
using UnityEngine;
using UnityEngine.Events;
using Ludiq.Reflection;

public class CGameEntity : CEntity {

	#region Internal class

	[Serializable]
	public class UnityEventFloat : UnityEvent<float> {}

	#endregion

	#region Fields

	[Header("Events")]
	public UnityEventFloat OnApplyDamage;

	protected int m_AnimationInt;
	public int animationInt {
		get { return this.m_AnimationInt; }
		set { this.m_AnimationInt = value; }
	}

	#endregion

	#region Main methods

	public virtual void AddEnergy() {

	}

	public virtual void CollectItems() {

	}

	public virtual void ApplyDamage(float value) {
		if (this.OnApplyDamage != null) {
			this.OnApplyDamage.Invoke (value);
		}
	}

	public virtual void AttackAnotherEntity() {
		
	}

	public virtual void ResetEntity() {
	
	}

	#endregion

	#region Getter && Setter

	public virtual void SetAnimation (int value)
	{
		this.m_AnimationInt = value;
	}

	#endregion

}
