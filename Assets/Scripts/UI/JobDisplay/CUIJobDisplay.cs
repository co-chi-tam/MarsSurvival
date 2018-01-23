using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CUIJobDisplay : MonoBehaviour {

	[Header("Configs")]
	[SerializeField]	protected CUIJobDisplayItem[] m_JobItems;

	public virtual void SetUpJobDisplay(string[] jobItems) {
		for (int x = 0; x < this.m_JobItems.Length; x++) {
			var jobItem = this.m_JobItems [x];
			jobItem.jobGameObject.SetActive (false);
		}
		for (int i = 0; i < jobItems.Length; i++) {
			var jobName = jobItems [i];
			for (int x = 0; x < this.m_JobItems.Length; x++) {
				var jobItem = this.m_JobItems [x];
				if (jobName == jobItem.jobName) {
					jobItem.jobGameObject.SetActive (true);
					break;
				} 
			}
		}
	}

}
