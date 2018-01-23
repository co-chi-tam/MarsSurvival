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
		this.SetUpAdapter ();
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
	}

	protected virtual void SetUpAdapter() {
		// INSTANTIATE
		if (this.m_AdapterPrefab != null) {
			var adapterTypes = FindObjectsOfType<CAdapterComponent> ();
			for (int i = 0; i < adapterTypes.Length; i++) {
				if (this.m_AdapterPrefab.name == adapterTypes [i].name) {
					this.m_AdapterSample = adapterTypes [i];
				}
			}
			if (this.m_AdapterSample == null) {
				this.m_AdapterSample = Instantiate (this.m_AdapterPrefab);
				this.m_AdapterSample.transform.SetParent (CAdapterRoot.Instance.transform);
				var rectTransform = this.m_AdapterSample.transform as RectTransform;
				rectTransform.localPosition = Vector3.zero;
				rectTransform.sizeDelta = Vector2.zero;
			}
			this.m_AdapterSample.host = this;
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
//					Debug.Log (data.triggerName + " / " + trigger.sourceType + " / " + data.OnTriggerInvoke.sourceType);
					var value = trigger.Get ();
					// BUG
					if (value != null) {
						if (value.GetType ().Namespace.Contains ("UnityEngine")) {
							if (data.OnTriggerInvoke.isAssigned) {
								data.OnTriggerInvoke.Set (value);
							}
						} else {
							if (data.OnTriggerInvoke.isAssigned) {
								data.OnTriggerInvoke.InvokeOrSet (value);
							}
						}
					} else {
						if (data.OnTriggerInvoke.isAssigned) {
							data.OnTriggerInvoke.Set (value);
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
