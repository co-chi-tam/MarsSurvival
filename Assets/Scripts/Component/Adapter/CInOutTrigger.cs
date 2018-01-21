using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Ludiq.Reflection;

public class CInOutTrigger : MonoBehaviour {

	#region Internal class

	[System.Serializable]
	public class UnityEventInOut: UnityEvent<CInOutTriggerData> {}

	#endregion

	#region Fields

	[Header("Events")]
	[SerializeField]	protected CInOutTriggerData m_TriggerData;
	public CInOutTriggerData triggerData {
		get { return this.m_TriggerData; }
		set { this.m_TriggerData = value; }
	}
	public UnityEventInOut OnTriggered;

	#endregion

	#region Main methods

	public virtual void Trigger() {
		if (this.OnTriggered != null) {
			this.OnTriggered.Invoke (this.m_TriggerData);
		}
	}

	#endregion

}
