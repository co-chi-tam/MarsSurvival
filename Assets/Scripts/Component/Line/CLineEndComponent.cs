using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Events.Utils;
using Ludiq.Reflection;

public class CLineEndComponent : CComponent {

	#region Internal class

	[System.Serializable]
	public class UnityEventConnectRoot: UnityEvent<CLineTerminalComponent> {}

	#endregion

	#region Fields

	[SerializeField]	protected string m_GroupName = "Empty group";
	public string groupName {
		get { return this.m_GroupName; }
		set { this.m_GroupName = value; }
	}
	[SerializeField]	protected CLineTerminalComponent m_RootLine = null;

	[Header("Events")]
	[Filter(Fields = true, Properties = true, Methods = true)]
	public UnityMember OnBoolCondition;
	public UnityEventConnectRoot OnConnected;
	public UnityEvent OnFree;

	#endregion

	#region Implementation CComponent

	protected override void LateUpdate ()
	{
		base.LateUpdate ();
		if (this.m_IsActive == false)
			return;
		if (this.m_RootLine != null) {
			if (this.OnConnected != null) {
				this.OnConnected.Invoke (this.m_RootLine);
			}
			this.m_RootLine = null;
		} else {
			if (this.OnFree != null) {
				this.OnFree.Invoke ();
			}
		}
	}

	#endregion

	#region Main methods

	public virtual bool ConnectedLine(CLineTerminalComponent value) {
		if (this.m_GroupName == value.groupName) {
			this.m_RootLine = value;
			if (this.OnBoolCondition.isAssigned) {
				return this.OnBoolCondition.Get<bool> ();
			}
			return true;
		}
		return false;
	}

	#endregion

}
