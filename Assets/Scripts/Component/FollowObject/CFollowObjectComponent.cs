using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CMoveComponent))]
public class CFollowObjectComponent : CComponent {

	#region Fields

	[SerializeField]	protected CFollowObjectEndPointComponent m_EndPointComponent;
	public Transform target {
		get { 
			if (this.m_EndPointComponent == null)
				return null;
			return this.m_EndPointComponent.transform; 
		}
		set { 
			if (value != null) {
				this.m_EndPointComponent = value.GetComponent<CFollowObjectEndPointComponent> ();
				if (this.m_EndPointComponent != null) {
					this.m_EndPointComponent.AddFollower (this);
				}
			} else {
				this.m_EndPointComponent = null;
			}
		}
	}
	[SerializeField]	protected float m_MinDistance = 1f;
	public float minDistance {
		get { return this.m_MinDistance; }
		set { this.m_MinDistance = value; }
	}

	protected CMoveComponent m_MoveComponent;

	#endregion

	#region Implementation Component 

	protected override void Awake ()
	{
		base.Awake ();
		this.m_MoveComponent = this.GetComponent<CMoveComponent> ();
		this.m_MoveComponent.minDistance = this.m_MinDistance;
	}

	protected override void Update ()
	{
		base.Update ();
		if (this.m_IsActive
			&& this.m_EndPointComponent != null) {
			this.m_MoveComponent.targetPosition = this.m_EndPointComponent.transform.position;
			if (this.m_EndPointComponent != null) {
				this.m_EndPointComponent.OnActivePoint (this);
			}
		}
	}

	#endregion

}
