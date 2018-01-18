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

	[Header("Info")]
	[SerializeField]	protected CAnimatorComponent m_AnimatorComponent;
	[SerializeField]	protected CMoveComponent m_MoveComponent;
	[SerializeField]	protected CDataComponent m_CharacterComponent;

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

	protected override void Start ()
	{
		base.Start ();
		this.m_Data = this.m_CharacterComponent.Get<CCharacterData>();
	}

	protected override void Update ()
	{
		base.Update ();
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
