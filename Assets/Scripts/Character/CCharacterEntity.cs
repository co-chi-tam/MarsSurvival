using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UICustomize;
using FSM;

public class CCharacterEntity : CEntity, IContext {

	#region Internal Class

	public enum ECharacterAnimation: int
	{
		Idle = 0,
		Move = 1,
		Charge = 2,
		Talk = 3,
		Action = 4,

		Death = 10
	}

	#endregion

	#region Fields

	protected CAnimatorComponent m_AnimatorComponent;
	protected CDataComponent m_DataComponent;
	protected CInventoryComponent m_InventoryComponent;

	protected CCharacterData m_Data;

	public override bool IsActive {
		get { return this.m_IsActive; }
		set { this.m_IsActive = value; }
	}

	public virtual bool HaveEnergy {
		get {
			if (this.m_Data == null)
				return true;
			return this.m_Data.solarPoint > 0f;
		}
	}

	public virtual float solarPoint {
		get {
			if (this.m_Data == null)
				return 0f;
			return this.m_Data.solarPoint;
		} 
		set { 
			if (this.m_Data == null)
				return;
			this.m_Data.solarPoint = value;
		}
	}

	public virtual float solarPointPercent {
		get {
			if (this.m_Data == null)
				return 0f;
			return this.m_Data.solarPoint / this.m_Data.maxSolarPoint;
		} 
		set { 
			if (this.m_Data == null)
				return;
			this.m_Data.solarPoint = value;
		}
	}

	public virtual bool IsCharging {
		get { return this.m_AnimationInt == 2; } 
		set { this.m_AnimationInt = value ? 2 : 0; }
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
		set { this.m_OtherEntity = value; }
	}
	public Transform otherEntityTransform {
		get { 
			if (this.m_OtherEntity == null)
				return null;
			return this.m_OtherEntity.transform; 
		}
	}

	#endregion

	#region Implementation Entity

	protected override void Awake ()
	{
		base.Awake ();
		this.m_AnimatorComponent = this.GetGameComponent<CAnimatorComponent> ();
		this.m_DataComponent = this.GetGameComponent<CDataComponent> ();
		this.m_InventoryComponent = this.GetGameComponent<CInventoryComponent> ();
	}

	protected override void Start ()
	{
		base.Start ();
		this.m_Data = this.m_DataComponent.Get<CCharacterData>();
		this.m_InventoryComponent.items = this.m_Data.items;
	}

	protected override void LateUpdate ()
	{
		base.LateUpdate ();
		// ANIMATION
		this.m_AnimatorComponent.ApplyAnimation (
			"AnimParam", 
			this.m_AnimationInt
		);
	}

	#endregion

	#region Other Entity

	public virtual void InvokeOtherEntityEnergy() {
		if (this.m_OtherEntity != null) {
			var dataComponent = this.m_OtherEntity.GetGameComponent<CDataComponent> ();
			if (dataComponent != null) {
				dataComponent.UpdateDataPerInvoke ("AddPower");
			}
		}
	}

	public virtual void SetOtherEntityFollowMe(bool value) {
		if (this.m_OtherEntity != null) {
			var followerComponent = this.m_OtherEntity.GetGameComponent<CFollowObjectComponent> ();
			if (followerComponent != null) {
				followerComponent.target = value ? this.transform : null;
			}
		}
	}

	public virtual string[] GetOtherEntityJobs {
		get {
			if (this.m_OtherEntity != null) {
				var machineEntity = this.m_OtherEntity as CMachineEntity;
				if (machineEntity != null) {
					return machineEntity.GetJobs ();
				}
			}
			return new string[0];
		}
	}

	#endregion

	#region Getter && Setter

	public override void SetAnimation(int value) {
		base.SetAnimation (value);
	}

	#endregion

}
