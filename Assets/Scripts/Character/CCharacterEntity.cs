using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UICustomize;
using FSM;

public partial class CCharacterEntity : CEntity, IContext {

	#region Fields

	protected CAnimatorComponent m_AnimatorComponent;
	protected CInventoryComponent m_InventoryComponent;
	protected CDataComponent m_DataComponent;
	protected CMoveComponent m_MoveComponent;
	protected CObjectPoolMemberComponent m_ObjectPoolMemberComponent;

	protected CCharacterData m_Data;

	#endregion

	#region Implementation Entity

	public virtual void Init() {
		this.m_Data = this.m_DataComponent.Get<CCharacterData>();
		this.m_InventoryComponent.items = this.m_Data.items;
//		this.m_MoveComponent.currentPosition = new Vector3 (-24f, 0f, 5f);
//		this.m_MoveComponent.currentRotation = this.m_Data.rotation.ToV3 ();
//		this.m_MoveComponent.targetPosition = this.m_MoveComponent.currentPosition;
		// INVOKE DATA
		this.m_DataComponent.AddListener ("foodPoint", this.WasEatFood);
	}

	protected override void Awake ()
	{
		base.Awake ();
		this.m_AnimatorComponent = this.GetGameComponent<CAnimatorComponent> ();
		this.m_InventoryComponent = this.GetGameComponent<CInventoryComponent> ();
		this.m_DataComponent = this.GetGameComponent<CDataComponent> ();
		this.m_MoveComponent = this.GetGameComponent<CMoveComponent> ();
		this.m_ObjectPoolMemberComponent = this.GetGameComponent<CObjectPoolMemberComponent> ();
	}

	protected override void Start ()
	{
		base.Start ();
		this.Init ();
	}

	protected override void LateUpdate ()
	{
		base.LateUpdate ();
		// ANIMATION
		this.m_AnimatorComponent.ApplyAnimation (
			"AnimParam", 
			this.m_AnimationInt
		);

//		this.m_Data.position = this.m_Transform.position.ToString();
//		this.m_Data.rotation = this.m_Transform.rotation.ToString();
	}

	#endregion

	#region Getter && Setter

	public override void SetAnimation(int value) {
		base.SetAnimation (value);
	}

	#endregion

}
