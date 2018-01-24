using System;
using UnityEngine;
using UnityEngine.Events;
using Ludiq.Reflection;

[Serializable]
public class CInOutTriggerData {

	#region Fields

	[SerializeField]	protected string m_TriggerName;
	public string triggerName {
		get { return this.m_TriggerName; }
		set { this.m_TriggerName = value; }
	}
	[Filter(Fields = true, Properties = true, Methods = true)]
	[SerializeField]	public UnityMember OnTriggerInvoke;

	#endregion

	#region Constructor

	public CInOutTriggerData () {
		this.m_TriggerName = "Empty event";
	}

	#endregion

	#region Main methods

	public virtual bool isEmpty() {
		return string.IsNullOrEmpty (this.m_TriggerName);
	}

	public virtual bool isInvoke() {
		if (this.isEmpty()) {
			return false;
		}
		return this.m_TriggerName.StartsWith ("Invoke");
	}

	public virtual bool isGet() {
		if (this.isEmpty()) {
			return false;
		}
		return this.m_TriggerName.StartsWith ("Get");
	}

	public virtual bool isSet() {
		if (this.isEmpty()) {
			return false;
		}
		return this.m_TriggerName.StartsWith ("Set");
	}

	#endregion

}
