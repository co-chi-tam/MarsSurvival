using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Ludiq.Reflection;

public class CHostAdapterComponent : CComponent {

	#region Fields

	[Header("Adapters")]

	[SerializeField]	CInOutTriggerData[] m_DataResponses;
	public CInOutTriggerData[] dataResponses {
		get { return this.m_DataResponses; }
		set { this.m_DataResponses = value; }
	}
	protected Dictionary<string, CInOutTriggerData> m_DataSamples;
	public Dictionary<string, CInOutTriggerData> dataSamples {
		get { return this.m_DataSamples; }
	}

	protected List<CAdapterComponent> m_Adapters;
	public List<CAdapterComponent> adapters {
		get { return this.m_Adapters; }
		set { this.m_Adapters = value; }
	}

	#endregion

	#region Implementation Component

	protected override void Awake ()
	{
		base.Awake ();
		// INIT
		this.InitData ();
	}

	#endregion

	#region Main methods

	protected virtual void InitData() {
		this.m_Adapters = new List<CAdapterComponent> ();
		this.m_DataSamples = new Dictionary<string, CInOutTriggerData> ();
		for (int i = 0; i < this.m_DataResponses.Length; i++) {
			var data = this.m_DataResponses[i];
			if (this.m_DataSamples.ContainsKey (data.triggerName) == false) {
				this.m_DataSamples.Add (data.triggerName, data);
			}
		}
	}

	public virtual void Invoke (CAdapterComponent adapter, CInOutTriggerData data) {
		if (this.m_IsActive == false)
			return;
		if (this.m_Adapters.Contains (adapter) == false) {
			this.m_Adapters.Add (adapter);
		}
		if (this.m_DataSamples.ContainsKey (data.triggerName)) {
			var trigger = this.m_DataSamples [data.triggerName].OnTriggerInvoke;
			if (trigger.isAssigned) {
				if (data.isInvoke ()) {
					// INVOKE
					trigger.Invoke ();
				} else if (data.isGet ()) {
					// GET
					var value = trigger.Get ();
					data.OnTriggerInvoke.Invoke (value);
				} else if (data.isSet ()) {
					// SET
					var value = data.OnTriggerInvoke.Get ();
					trigger.InvokeOrSet (value);
				} 
			} else {
				Debug.LogError (string.Format ("[{0}] DID NOT ASSIGN METHODS", data.triggerName));
			}
		}
	}

	#endregion

}
