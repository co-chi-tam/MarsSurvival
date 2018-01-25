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
	protected CSpawnObjectComponent m_SpawnObjectComponent;

	protected CCharacterData m_Data;

	#endregion

	#region Implementation Entity

	protected override void Awake ()
	{
		base.Awake ();
		this.m_AnimatorComponent = this.GetGameComponent<CAnimatorComponent> ();
		this.m_InventoryComponent = this.GetGameComponent<CInventoryComponent> ();
		this.m_DataComponent = this.GetGameComponent<CDataComponent> ();
		this.m_SpawnObjectComponent = this.GetGameComponent<CSpawnObjectComponent> ();
	}

	protected override void Start ()
	{
		base.Start ();
		this.m_Data = this.m_DataComponent.Get<CCharacterData>();
		this.m_InventoryComponent.items = this.m_Data.items;
		// INVOKE DATA
		this.m_DataComponent.AddListener ("foodPoint", this.WasEatFood);
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

	#region Getter && Setter

	public override void SetAnimation(int value) {
		base.SetAnimation (value);
	}

	#endregion

}
