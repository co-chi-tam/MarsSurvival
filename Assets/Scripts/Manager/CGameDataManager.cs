using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleSingleton;

public class CGameDataManager : CMonoSingleton<CGameDataManager> {

	#region Fields

	[Header("Game configs")]
	[SerializeField]	protected Vector3 m_MovePoint;
	public Vector3 movePoint {
		get { return this.m_MovePoint; }
		set { this.m_MovePoint = value; }
	}
//	[SerializeField]	protected Vector3 m_PositionPoint;
//	public Vector3 positionPoint {
//		get { return this.m_PositionPoint; }
//		set { this.m_PositionPoint = value; }
//	}
//	[SerializeField]	protected Vector3 m_RotationPoint;
//	public Vector3 rotationPoint {
//		get { return this.m_RotationPoint; }
//		set { this.m_RotationPoint = value; }
//	}
	[SerializeField]	protected List<CItemData> m_Items;
	public List<CItemData> items {
		get { 
			if (this.m_Items == null) {
				this.m_Items = new List<CItemData> ();
			}
			return this.m_Items;
		}
		set { this.m_Items = new List<CItemData> (value); }
	}

	protected Dictionary<string, Action> m_AnimatorEvents;

	#endregion

	#region Implementation CMonoSingleton

	protected override void Awake () {
		base.Awake ();
		this.m_AnimatorEvents = new Dictionary<string, Action> ();
	}

	#endregion

	#region Events

	public virtual void RegisterCallback(string name, Action callback) {
		if (this.m_AnimatorEvents.ContainsKey (name) == false) {
			this.m_AnimatorEvents.Add (name, callback);
		} 
	} 

	public virtual void InvokeCallback(string name) {
		if (this.m_AnimatorEvents.ContainsKey (name)) {
			this.m_AnimatorEvents[name].Invoke();
		} 
	}

	#endregion

}
