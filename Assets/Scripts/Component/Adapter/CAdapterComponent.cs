using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ludiq.Reflection;

public class CAdapterComponent : CComponent {

	#region Fields

	[Header("Configs")]
	[SerializeField]	protected CHostAdapterComponent m_Host;
	public CHostAdapterComponent host {
		get { return this.m_Host; }
		set { this.m_Host = value; }
	}
	[SerializeField]	protected List<CInOutTriggerData> m_ListDataResponses;
	public List<CInOutTriggerData> listDataResponse {
		get { return this.m_ListDataResponses; }
		set { this.m_ListDataResponses = new List<CInOutTriggerData> (value); }
	}
	protected Dictionary<string, CInOutTriggerData> m_DataSamples;
	public Dictionary<string, CInOutTriggerData> dataSamples {
		get { return this.m_DataSamples; }
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
		this.m_DataSamples = new Dictionary<string, CInOutTriggerData> ();
		for (int i = 0; i < this.m_ListDataResponses.Count; i++) {
			var data = this.m_ListDataResponses[i];
			if (this.m_DataSamples.ContainsKey (data.triggerName) == false) {
				this.m_DataSamples.Add (data.triggerName, data);
			}
		}
	}

	public virtual void Invoke(string name, CInOutTriggerData value) {
		if (this.m_DataSamples.ContainsKey (name) == false)
			return;
		this.m_DataSamples [name].OnTriggerInvoke.InvokeOrSet (value.OnTriggerInvoke.Get ());
	}

	public virtual void Invoke(CInOutTriggerData value) {
		if (this.m_Host == null) {
			Debug.LogError ("HOST IS EMPTY.");
		} else {
			this.m_Host.Invoke (this, value);
		}
	}

	#endregion

}
