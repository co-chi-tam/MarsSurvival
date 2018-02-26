using System;
using UnityEngine;

[Serializable]
public class CMissionItemData {

	[SerializeField]	protected string m_MissionName;
	public string missionName {
		get { return this.m_MissionName; }
		set { this.m_MissionName = value; }
	}
	[SerializeField]	protected string m_MissionDetail;
	public string missionDetail {
		get { return this.m_MissionDetail; }
		set { this.m_MissionDetail = value; }
	}
	[SerializeField]	protected CMissionConditionData[] m_MissionConditions;
	public CMissionConditionData[] missionConditions {
		get { return this.m_MissionConditions; }
		set { this.m_MissionConditions = value; }
	}

}
