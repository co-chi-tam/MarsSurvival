using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UICustomize;

public class CCharacterEntity : CEntity {

	[SerializeField]	protected UIJoytick m_Joytick;
	[SerializeField]	protected CMoveComponent m_MoveComponent;
	[SerializeField]	protected CDataPointComponent m_SolarPoint;

	protected override void Start ()
	{
		base.Start ();
		this.m_MoveComponent.isActive = true;
	}

	protected override void Update ()
	{
		base.Update ();
		if (this.m_Joytick != null) {
			var movePoint = this.transform.position + this.m_Joytick.InputDirectionXZ;
			this.m_MoveComponent.targetPosition = movePoint;
		}
	}

}
