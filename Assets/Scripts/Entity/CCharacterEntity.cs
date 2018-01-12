using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCharacterEntity : CEntity {

	[SerializeField]	protected CMoveComponent m_MoveComponent;

	protected override void Start ()
	{
		base.Start ();
		this.m_MoveComponent.isActive = true;
	}

	protected override void Update ()
	{
		base.Update ();
		if (Input.GetMouseButtonUp (0)) {
			RaycastHit hitInfo;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hitInfo)) {
				var movePoint = hitInfo.point;
				movePoint.y = 0f;
				this.m_MoveComponent.targetPosition = movePoint;
			}
		}
	}

}
