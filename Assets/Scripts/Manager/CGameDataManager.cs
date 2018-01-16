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

	#endregion

	#region Implementation CMonoSingleton

	protected override void Awake () {
		base.Awake ();
	}

	#endregion

}
