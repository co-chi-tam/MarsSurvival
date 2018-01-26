using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Ludiq.Reflection;

public class CTileMapMemberComponent : MonoBehaviour {

	[Header("Configs")]
	[SerializeField]	protected CTileMapObject m_TileMapObject;
	public CTileMapObject tileMapObject {
		get { return this.m_TileMapObject; }
		set { this.m_TileMapObject = value; }
	}
	[SerializeField]	protected float m_Radius = 20f;
	public float radius {
		get { return this.m_Radius; }
		set { this.m_Radius = value; }
	}

	[Header("Events")]
	public UnityEvent OnReload;

	public virtual void LoadTileMap(CTileMapObject value) {
		this.m_TileMapObject = value;
		if (this.OnReload != null) {
			this.OnReload.Invoke ();
		}
	}

	public virtual void ApplyRandomPosition() {
		this.transform.position = this.GetRandomPosition (this.m_Radius);
	}

	public virtual Vector3 GetRandomPosition (float radius) {
		if (this.m_TileMapObject == null)
			return this.transform.position;
		return this.m_TileMapObject.GetRandomPosition (radius);
	}

}
