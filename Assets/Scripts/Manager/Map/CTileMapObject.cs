using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Ludiq.Reflection;

public class CTileMapObject : MonoBehaviour {

	#region Internal class

	[System.Serializable]
	public class UnityEventTileMap : UnityEvent<CTileMapObject> {}

	#endregion

	#region Fields

	[Header("Events")]
	public UnityEventTileMap OnLoaded;
	public UnityEventTileMap OnRemove;

	#endregion

	#region Main methods

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
			this.OnLoaded.Invoke (this);
		}
	}

	public virtual void OnRemoveTile() {
		if (this.OnRemove != null) {
			this.OnRemove.Invoke (this);
		}
	}

	#endregion

}
