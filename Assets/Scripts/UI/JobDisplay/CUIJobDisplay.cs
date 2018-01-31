using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CUIJobDisplay : MonoBehaviour {

	#region Fields

	[Header("Configs")]
	[SerializeField]	protected float m_DisplayRadius = 20f;
	[SerializeField]	protected CUIJobDisplayItem[] m_JobItems;

	#endregion

	#region Main methods

	public virtual void SetUpJobDisplay(string[] jobItems) {
		if (jobItems == null)
			return;
		for (int x = 0; x < this.m_JobItems.Length; x++) {
			var jobItem = this.m_JobItems [x];
			jobItem.jobGameObject.SetActive (false);
		}
		var segment = (Mathf.PI * 2f) / jobItems.Length;
		var theta = 0f;
		for (int i = 0; i < jobItems.Length; i++) {
			var jobName = jobItems [i];
			var x = Mathf.Sin (theta) * this.m_DisplayRadius;
			var y = Mathf.Cos (theta) * this.m_DisplayRadius;
			for (int a = 0; a < this.m_JobItems.Length; a++) {
				var jobItem = this.m_JobItems [a];
				if (jobName == jobItem.jobName) {
					jobItem.jobGameObject.SetActive (true);
					jobItem.SetUp (new Vector2 (x, y));
					break;
				} 
			}
			theta += segment;
		}
	}

	#endregion

}
