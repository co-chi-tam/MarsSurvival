using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGameEntity : CEntity {

	#region Fields

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

	}

	public virtual void AttackAnotherEntity() {
		
	}

	#endregion

	#region Getter && Setter

	public virtual void SetAnimation (int value)
	{
		this.m_AnimationInt = value;
	}

	#endregion

}
