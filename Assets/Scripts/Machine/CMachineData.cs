using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class CMachineData : ScriptableObject {

	#region Fields

	[Header("Fields")]
	public string machineName;

	#endregion

	#region Constructor

	public CMachineData (): base() {

	}

	#endregion

	#region Override

	public override string ToString ()
	{
		return string.Format ("[CMachineData] machineName: {0}", this.machineName);
	}

	#endregion

}
