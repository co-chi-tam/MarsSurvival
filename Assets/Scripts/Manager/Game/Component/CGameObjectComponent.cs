using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGameObjectComponent : CComponent {

	[SerializeField]	protected CCharacterEntity m_MainCharacter;

	protected CGameManager m_GameManager;

	protected override void Start ()
	{
		base.Start ();
		this.m_GameManager = CGameManager.GetInstance ();
	}

	protected override void LateUpdate ()
	{
		base.LateUpdate (); 
		if (this.m_MainCharacter == null)
			return;
		this.m_GameManager.isCharacterDeath = this.m_MainCharacter.IsDeath;
	}

}
