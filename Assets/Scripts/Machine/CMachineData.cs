using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class CMachineData : ScriptableObject {

	#region Fields

	[Header("Fields")]
	protected string m_MachineName;
	public virtual string machineName {
		get { return this.m_MachineName; }
		set { this.m_MachineName = value; }
	}

	#endregion

	#region Constructor

	public CMachineData (): base() {
		this.m_MachineName	= "Empty name";
	}

	#endregion

	#region Override

	public override string ToString ()
	{
		return string.Format ("[CMachineData] machineName: {0}", this.m_MachineName);
	}

	#endregion

}
