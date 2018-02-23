using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CCharacterEntity {

	#region For fun Entity

	protected float m_WasConsumeFood = 0;

	public virtual void NeedPooping() {
		var poop = this.m_ObjectPoolMemberComponent.Get ("Dirt");
		if (poop != null) {
			poop.transform.position = this.m_Transform.position;
		}
	}

	// POOPING WHEN EAT TOO MUCH
	public virtual void WasEatFood(object value) {
		if (value is float) {
			var floatValue = (float) value;
			if (floatValue < 0f) {
				this.m_WasConsumeFood += floatValue;
				if (this.m_WasConsumeFood < -(this.m_Data.maxFoodPoint / 2f)) {
					this.NeedPooping ();
					this.m_WasConsumeFood = 0f;
				}
			}
		}
	}

	#endregion

}
