using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Ludiq.Reflection;

public class CHostAdapterComponent : CComponent {

	#region Fields

	[Header("Configs")]
	[SerializeField]	protected CAdapterComponent m_AdapterPrefab;
	[SerializeField]	CInOutTriggerData[] m_DataResponses;
	public CInOutTriggerData[] dataResponses {
		get { return this.m_DataResponses; }
		set { this.m_DataResponses = value; }
	}
	protected Dictionary<string, CInOutTriggerData> m_DataSamples;
	public Dictionary<string, CInOutTriggerData> dataSamples {
		get { return this.m_DataSamples; }
	}

	protected CAdapterComponent m_AdapterSample;

	#endregion

	#region Implementation Component

	protected override void Awake ()
	{
		base.Awake ();
		// INIT
		this.InitData ();
	}

	protected override void Start ()
	{
		base.Start ();
	}

	#endregion

	#region Main methods

	protected virtual void InitData() {
		this.m_DataSamples = new Dictionary<string, CInOutTriggerData> ();
		for (int i = 0; i < this.m_DataResponses.Length; i++) {
			var data = this.m_DataResponses[i];
			if (this.m_DataSamples.ContainsKey (data.triggerName) == false) {
				this.m_DataSamples.Add (data.triggerName, data);
			}
		}
		// INSTANTIATE
		if (this.m_AdapterPrefab != null) {
			this.m_AdapterSample = FindObjectOfType<CAdapterComponent> ();
			if (this.m_AdapterSample == null) {
				this.m_AdapterSample = Instantiate (this.m_AdapterPrefab);
				this.m_AdapterSample.transform.SetParent (CAdapterRoot.Instance.transform);
			}
			this.m_AdapterSample.host = this;
		}
	}

	public virtual void Set (CInOutTriggerData data) {
		if (this.m_IsActive == false)
			return;
		if (this.m_AdapterSample == null)
			return;
		var adapter = this.m_AdapterSample;
		for (int x = 0; x < adapter.instanceTriggers.Length; x++) {
			var trigger = adapter.instanceTriggers [x];
			var triggerData = trigger.triggerData;
			if (triggerData.triggerName == data.triggerName) {
				var value = data.OnTriggerInvoke.Get ();
				triggerData.OnTriggerInvoke.InvokeOrSet (value);
			}
		}
	}

	public virtual void Invoke (CAdapterComponent adapter, CInOutTriggerData data) {
		if (this.m_IsActive == false)
			return;
		this.m_AdapterSample = adapter;
		if (this.m_DataSamples.ContainsKey (data.triggerName)) {
			var trigger = this.m_DataSamples [data.triggerName].OnTriggerInvoke;
			if (trigger.isAssigned) {
				if (data.isInvoke ()) {
					// INVOKE
					trigger.Invoke ();
				} else if (data.isGet ()) {
					// GET
					var value = trigger.Get ();
					// BUG
					if (value.GetType().Namespace.Contains("UnityEngine")) {
						if (data.OnTriggerInvoke.isAssigned) {
							data.OnTriggerInvoke.Set (value);
						}
					} else {
						if (data.OnTriggerInvoke.isAssigned) {
							data.OnTriggerInvoke.InvokeOrSet (value);
						}
					}
				} else if (data.isSet ()) {
					// SET
					if (data.OnTriggerInvoke.isAssigned) {
						var value = data.OnTriggerInvoke.Get ();
						trigger.InvokeOrSet (value);
					}
				} 
			} else {
				Debug.LogError (string.Format ("[{0}] DID NOT ASSIGN METHODS", data.triggerName));
			}
		}
	}

	#endregion

}
