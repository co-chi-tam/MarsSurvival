using System;
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
	protected CMissionComponent m_MissionComponent;
	protected CFollowObjectEndPointComponent m_FollowEndPointComponent;
	protected CMissionListComponent m_MissionListComponent;

	protected CCharacterData m_Data;

	[SerializeField]	protected bool m_IsAttack = false;
	public bool IsAttack {
		get { return this.m_IsAttack; }
		set { this.m_IsAttack = value; }
	}

	#endregion

	#region Implementation Entity

	public override void Init() {
		base.Init ();
		this.m_Data = this.m_DataComponent.Get<CCharacterData>();
		this.m_MoveComponent.moveSpeed = this.m_Data.moveSpeed;
		this.m_MoveComponent.SetupPosition (this.m_Data.position.ToV3 (), Quaternion.identity);
		this.m_MoveComponent.currentRotationAngle = this.m_Data.rotation;

		this.m_InventoryComponent.items = this.m_Data.items;
		this.m_DataComponent.AddListener ("foodPoint", this.WasEatFood);

		this.m_StoreToolComponent.LoadTool (this.m_Data.currentTool);
		if (this.m_Data.missionIndex < this.m_MissionListComponent.missionList.Length) {
			this.m_MissionComponent.data = this.m_MissionListComponent.missionList [this.m_Data.missionIndex];
		} else {
			this.m_MissionComponent.data = null;
		}
	}

	protected override void Awake ()
	{
		base.Awake ();
		this.m_AnimatorComponent 		= this.GetGameComponent<CAnimatorComponent> ();
		this.m_InventoryComponent 		= this.GetGameComponent<CInventoryComponent> ();
		this.m_DataComponent 			= this.GetGameComponent<CDataComponent> ();
		this.m_MoveComponent 			= this.GetGameComponent<CMoveComponent> ();
		this.m_ObjectPoolMemberComponent = this.GetGameComponent<CObjectPoolMemberComponent> ();
		this.m_MapMemberComponent 		= this.GetGameComponent<CMapMemberComponent> ();
		this.m_StoreToolComponent 		= this.GetGameComponent<CStoreToolComponent> ();
		this.m_MissionComponent 		= this.GetGameComponent <CMissionComponent> ();
		this.m_FollowEndPointComponent 	= this.GetGameComponent <CFollowObjectEndPointComponent> ();
		this.m_MissionListComponent 	= this.GetGameComponent <CMissionListComponent> ();
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
		// SAVE VALUE
		this.SaveEntity ();
	}

	#endregion

	#region Main methods

	public override void SaveEntity ()
	{
		base.SaveEntity ();
		// DATA
		this.m_Data.position = this.m_MoveComponent.currentPosition.ToString();
		this.m_Data.rotation = this.m_MoveComponent.currentRotationAngle;
		if (this.m_StoreToolComponent.currentToolData != null) {
			this.m_Data.currentTool = this.m_StoreToolComponent.currentToolData.entityName;
		} else {
			this.m_Data.currentTool = string.Empty;
		}
	}

	public virtual void ApplyMovePosition(float dt) {
		var movePoint = this.m_Transform.position + this.m_DeltaMovePoint;
		this.m_MoveComponent.targetPosition = movePoint;
		this.m_MoveComponent.SetupMove (dt);
	}

	public override void ApplyDamage (float value)
	{
		base.ApplyDamage (value);
		this.m_Data.healthPoint -= value;
	}

	#endregion

	#region Getter && Setter

	public override void SetAnimation(int value) {
		base.SetAnimation (value);
	}

	#endregion

}
