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
			return this.m_Data.healthPoint > 0f
				&& this.m_Data.oxygenPoint > 0f
				&& this.m_Data.foodPoint > 0f
				&& this.m_Data.waterPoint > 0f;
		}
	}

	#endregion

	#region Info point

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
				return 1f;
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
				return 1f;
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
				return 1f;
			return this.m_Data.foodPoint / this.m_Data.maxFoodPoint;
		}
	}

	// OXYGEN
	public virtual float oxygenPoint {
		get {
			if (this.m_Data == null)
				return 0f;
			return this.m_Data.oxygenPoint;
		}
		set { 
			if (this.m_Data == null)
				return; 
			this.m_Data.oxygenPoint = value;
		}
	}
	public float oxygenPointPercent {
		get { 
			if (this.m_Data == null)
				return 1f;
			return this.m_Data.oxygenPoint / this.m_Data.maxOxygenPoint;
		}
	}

	#endregion

	#region More fields

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
	public bool IsMove {
		get { return this.m_DeltaMovePoint != Vector3.zero; }
		set { this.m_DeltaMovePoint = value ? Vector3.right : Vector3.zero; }
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

	public Vector3 mapPosition {
		get { 
			if (this.m_MapMemberComponent == null)
				return this.transform.position;
			return this.m_MapMemberComponent.centerPosition; 
		}
	}

	public bool HaveTool {
		get { 
			if (this.m_StoreToolComponent == null)
				return false;
			return this.m_StoreToolComponent.haveTool;
		}
	}

	public CToolData currentToolData {
		get {
			if (this.m_StoreToolComponent == null)
				return null;
			return this.m_StoreToolComponent.currentToolData;
		}
	}

	#endregion

	#region Mission

	public string missionFullDetail {
		get { 
			if (this.m_MissionComponent == null) 
				return string.Empty;
			return this.m_MissionComponent.conditionFullDetail;
		}

	}

	#endregion

}
