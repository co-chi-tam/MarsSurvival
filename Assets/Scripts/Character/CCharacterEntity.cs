using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UICustomize;

public class CCharacterEntity : CEntity {

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

	protected CCharacterData m_Data;

	public override bool isActive {
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

	public virtual bool IsCharging {
		get {
			return CGameDataManager.Instance.IsCharging;
		}
	}

	#endregion

	#region Implementation Entity

	protected override void Awake ()
	{
		base.Awake ();
		this.m_AnimatorComponent = this.GetGameComponent<CAnimatorComponent> ();
		this.m_DataComponent = this.GetGameComponent<CDataComponent> ();
	}

	protected override void Start ()
	{
		base.Start ();
		this.m_Data = this.m_DataComponent.Get<CCharacterData>();
	}

	protected override void LateUpdate ()
	{
		base.LateUpdate ();
		// ANIMATION
		this.m_AnimatorComponent.ApplyAnimation (
			"AnimParam", 
			CGameDataManager.Instance.animationValue
		);
		// SOLAR
		CGameDataManager.Instance.UpdateSolarPoint (
			this.m_Data.solarPoint, 
			this.m_Data.maxSolarPoint
		);
	}

	#endregion

	#region Getter && Setter

	public virtual void SetAnimation(int value) {
		CGameDataManager.Instance.animationValue = value;
	}

	#endregion

}
