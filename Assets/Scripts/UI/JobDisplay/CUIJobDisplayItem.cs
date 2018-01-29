using System;
using UnityEngine;

[Serializable]
public class CUIJobDisplayItem {

	#region Main methods

	[Header("Configs")]
	public string jobName;
	public GameObject jobGameObject;

	#endregion

	#region Main methods

	public virtual void SetUp(Vector2 pos) {
		var rectTransform = this.jobGameObject.transform as RectTransform;
		rectTransform.anchoredPosition = pos;
	}

	#endregion

}
