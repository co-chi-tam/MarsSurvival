using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Events.Utils;

public class CLineEndComponent : CComponent {

	#region Fields

	[Header("Events")]
	public UnityEvent OnConnected;

	#endregion

	#region Main methods

	public virtual void ConnectedLine() {
		if (this.OnConnected != null) {
			this.OnConnected.Invoke ();
		}
	}

	#endregion

}
