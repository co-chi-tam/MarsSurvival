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
	[SerializeField]	protected CInOutTrigger[] m_InstanceTriggers;
	public CInOutTrigger[] instanceTriggers {
		get { return this.m_InstanceTriggers; }
		set { this.m_InstanceTriggers = value; }
	}

	#endregion

	#region Implementation Component

	protected override void Awake ()
	{
		base.Awake ();
	}

	#endregion

	#region Main methods

	public virtual void Invoke(CInOutTriggerData value) {
		if (this.m_Host == null) {
			Debug.LogError ("HOST IS EMPTY.");
		} else {
			this.m_Host.Invoke (this, value);
		}
	}



	#endregion

}
