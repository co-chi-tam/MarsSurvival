using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Ludiq.Reflection;

public class CTileMapObject : MonoBehaviour {

	[Header("Events")]
	public UnityEvent OnLoaded;
	public UnityEvent OnRemove;

	public virtual void OnLoadTile() {
		if (this.OnLoaded != null) {
			this.OnLoaded.Invoke ();
		}
	}

	public virtual void OnRemoveTile() {
		if (this.OnRemove != null) {
			this.OnRemove.Invoke ();
		}
	}

}
