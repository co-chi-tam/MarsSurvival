using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CMoveComponent))]
public class CFollowObjectComponent : CComponent {

	#region Fields

	[Header("Configs")]
	[SerializeField]	protected bool m_AutoActive;
	public bool autoActive {
		get { return this.m_AutoActive; }
		set { this.m_AutoActive = value; }
	}
	[SerializeField]	protected Transform m_Target;
	public Transform target {
		get { return this.m_Target; }
		set { this.m_Target = value; }
	}

	protected CMoveComponent m_MoveComponent;

	#endregion

	#region Implementation Component 

	protected override void Awake ()
	{
		base.Awake ();
		this.m_MoveComponent = this.GetComponent<CMoveComponent> ();
	}

	protected override void Update ()
	{
		base.Update ();
		if (this.m_AutoActive
			&& this.m_Target != null) {
			this.m_MoveComponent.targetPosition = this.m_Target.position;
		}
	}

	#endregion

}
