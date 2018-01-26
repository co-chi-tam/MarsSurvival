using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Ludiq.Reflection;

public class CTileMapObject : MonoBehaviour {

	[Header("Events")]
	public UnityEvent OnLoaded;
	public UnityEvent OnRemove;

	public virtual Vector3 GetRandomPosition(float radius) {
		var randomVector = Random.insideUnitCircle;
		var randomPosition = new Vector3 (
			this.transform.position.x + randomVector.x * radius,
			this.transform.position.y,
			this.transform.position.z + randomVector.y * radius
		);
		return randomPosition;
	}

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
