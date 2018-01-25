using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CCharacterEntity {

	#region For fun Entity

	protected float m_WasEatPoint = 0;
	public bool IsEatTooMuch {
		get { 
			if (this.m_Data == null)
				return false;
			return this.m_WasEatPoint > this.m_Data.maxFoodPoint; 
		}
	}

	public virtual void NeedPooping() {
		this.m_SpawnObjectComponent.SpawnGameObject ("Soil");
	}

	public virtual void WasEatFood(object value) {
		if (value is float) {
			this.m_WasEatPoint += (float)value;
			if (this.m_WasEatPoint > this.m_Data.maxFoodPoint) {
				this.NeedPooping ();
				this.m_WasEatPoint = 0f;
			}
		}
	}

	#endregion

}
