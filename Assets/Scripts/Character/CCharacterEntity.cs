﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UICustomize;
using FSM;

public partial class CCharacterEntity : CGameEntity, IContext {

	#region Fields

	protected CAnimatorComponent m_AnimatorComponent;
	protected CInventoryComponent m_InventoryComponent;
	protected CDataComponent m_DataComponent;
	protected CMoveComponent m_MoveComponent;
	protected CMapMemberComponent m_MapMemberComponent;
	protected CObjectPoolMemberComponent m_ObjectPoolMemberComponent;
	protected CStoreToolComponent m_StoreToolComponent;

	protected CCharacterData m_Data;

	[SerializeField]	protected bool m_IsAttack = false;
	public bool IsAttack {
		get { return this.m_IsAttack; }
		set { this.m_IsAttack = value; }
	}

	#endregion

	#region Implementation Entity

	public virtual void Init() {
		this.m_Data = this.m_DataComponent.Get<CCharacterData>();
		this.m_MoveComponent.moveSpeed = this.m_Data.moveSpeed;
		this.m_InventoryComponent.items = this.m_Data.items;
		this.m_DataComponent.AddListener ("foodPoint", this.WasEatFood);
		this.m_StoreToolComponent.LoadTool (this.m_Data.currentTool);
	}

	protected override void Awake ()
	{
		base.Awake ();
		this.m_AnimatorComponent = this.GetGameComponent<CAnimatorComponent> ();
		this.m_InventoryComponent = this.GetGameComponent<CInventoryComponent> ();
		this.m_DataComponent = this.GetGameComponent<CDataComponent> ();
		this.m_MoveComponent = this.GetGameComponent<CMoveComponent> ();
		this.m_ObjectPoolMemberComponent = this.GetGameComponent<CObjectPoolMemberComponent> ();
		this.m_MapMemberComponent = this.GetGameComponent<CMapMemberComponent> ();
		this.m_StoreToolComponent = this.GetGameComponent<CStoreToolComponent> ();
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
	}

	protected override void OnApplicationQuit ()
	{
		base.OnApplicationQuit ();
		// DATA
		this.m_Data.saveData.position = this.m_Transform.position.ToString();
		this.m_Data.saveData.rotation = this.m_Transform.rotation.ToString();
		this.m_Data.saveData.saveTool = this.m_Data.currentTool;
	}

	#endregion

	#region Main methods

	public virtual void ApplyMovePosition(float dt) {
		var movePoint = this.m_Transform.position + this.m_DeltaMovePoint;
		this.m_MoveComponent.targetPosition = movePoint;
		this.m_MoveComponent.SetupMove (dt);
	}

	#endregion

	#region Getter && Setter

	public override void SetAnimation(int value) {
		base.SetAnimation (value);
	}

	#endregion

}
