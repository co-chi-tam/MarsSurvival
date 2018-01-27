using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CCharacterEntity {
	 
	#region Fields

	public override bool IsActive {
		get { return this.m_IsActive; }
		set { this.m_IsActive = value; }
	}

	public virtual bool HaveEnergy {
		get {
			if (this.m_Data == null)
				return true;
			return this.m_Data.energyPoint > 0f;
		}
	}

	public virtual bool IsAlive {
		get {
			if (this.m_Data == null)
				return true;
			return this.m_Data.energyPoint > 0f
				&& this.m_Data.foodPoint > 0f
				&& this.m_Data.waterPoint > 0f;
		}
	}

	// ENERGY
	public virtual float energyPoint {
		get {
			if (this.m_Data == null)
				return 0f;
			return this.m_Data.energyPoint;
		} 
		set { 
			if (this.m_Data == null)
				return;
			this.m_Data.energyPoint = value;
		}
	}
	public virtual float energyPointPercent {
		get {
			if (this.m_Data == null)
				return 0f;
			return this.m_Data.energyPoint / this.m_Data.maxEnergyPoint;
		} 
	}

	// WATER
	public virtual float waterPoint {
		get {
			if (this.m_Data == null)
				return 0f;
			return this.m_Data.waterPoint;
		}
		set { 
			if (this.m_Data == null)
				return; 
			this.m_Data.waterPoint = value;
		}
	}
	public float waterPointPercent {
		get { 
			if (this.m_Data == null)
				return 0f;
			return this.m_Data.waterPoint / this.m_Data.maxWaterPoint;
		}
	}

	// FOOD
	public virtual float foodPoint {
		get {
			if (this.m_Data == null)
				return 0f;
			return this.m_Data.foodPoint;
		}
		set { 
			if (this.m_Data == null)
				return; 
			this.m_Data.foodPoint = value;
		}
	}
	public float foodPointPercent {
		get { 
			if (this.m_Data == null)
				return 0f;
			return this.m_Data.foodPoint / this.m_Data.maxFoodPoint;
		}
	}
	public virtual bool IsEatTooMuch {
		get { 
			if (this.m_Data == null)
				return false;
			return this.m_Data.foodPoint >= this.m_Data.maxFoodPoint; 
		}
	}

	protected Vector3 m_DeltaMovePoint = Vector3.zero;
	public Vector3 deltaMovePoint {
		get { return this.m_DeltaMovePoint; }
		set { this.m_DeltaMovePoint = value; }
	}
	public bool IsStand {
		get { return this.m_DeltaMovePoint != Vector3.zero; }
		set { this.m_DeltaMovePoint = value ? Vector3.zero : Vector3.right; }
	}

	public List<CItemData> inventoryItems {
		get { 
			if (this.m_Data == null)
				return new List<CItemData>();
			return this.m_Data.items; 
		}
		set {
			if (this.m_Data == null)
				return;
			this.m_Data.items = value;
		}
	}

	[SerializeField]	protected CEntity m_OtherEntity;
	public CEntity otherEntity {
		get { return this.m_OtherEntity; }
		set { 
			this.m_OtherEntity = value;  
		}
	}
	public Transform otherEntityTransform {
		get { 
			if (this.m_OtherEntity == null)
				return null;
			return this.m_OtherEntity.transform; 
		}
	}

	#endregion

}
