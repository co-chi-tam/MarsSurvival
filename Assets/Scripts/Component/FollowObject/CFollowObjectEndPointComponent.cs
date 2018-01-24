using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Ludiq.Reflection;

[DisallowMultipleComponent]
public class CFollowObjectEndPointComponent : CComponent {

	#region Internal class

	[System.Serializable]
	public class UnityEventFollowObject : UnityEvent<CFollowObjectComponent> {}

	#endregion

	#region Fields

	[Header("Events")]
	public UnityEventFollowObject OnActive;
	public UnityEvent OnFree;

	#endregion

	#region Main methods

	public virtual void OnActivePoint(CFollowObjectComponent value) {
		if (this.OnActive != null && value != null) {
			this.OnActive.Invoke (value);
		}
	}

	public virtual void OnFreePoint() {
		if (this.OnFree != null) {
			this.OnFree.Invoke ();
		}
	}

	#endregion

}
