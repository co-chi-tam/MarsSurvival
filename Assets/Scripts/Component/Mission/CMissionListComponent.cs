using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMissionListComponent : CComponent {

	[Header("Configs")]
	[SerializeField]	protected CMissionData[] m_MissionList;
	public CMissionData[] missionList {
		get { return this.m_MissionList; }
		set { this.m_MissionList = value; }
	}

}
