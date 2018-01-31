using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Ludiq.Reflection;

public class CTileMapMemberComponent : CComponent {

	#region Fields

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
	[Filter(Fields = true, Properties = true, Methods = true)]
	public UnityMember OnReloadCondition;
	public UnityEvent OnReload;

	#endregion

	#region Main methods

	public virtual void LoadTileMap(CTileMapObject value) {
		this.m_TileMapObject = value;
		if (this.OnReload != null) {
			this.OnReload.Invoke ();
		}
	}

	public virtual void ApplyRandomPosition() {
		if (this.OnReloadCondition.isAssigned) {
			var boolValue = this.OnReloadCondition.Get<bool> ();
			if (boolValue) {
				this.transform.position = this.GetRandomPosition (this.m_Radius);
			}
		} else {
			this.transform.position = this.GetRandomPosition (this.m_Radius);
		}
	}

	public virtual Vector3 GetRandomPosition (float radius) {
		if (this.m_TileMapObject == null) {
			var randomVector = Random.insideUnitCircle;
			var randomPosition = new Vector3 (
				this.transform.position.x + randomVector.x * radius,
				this.transform.position.y,
				this.transform.position.z + randomVector.y * radius
			);
			return randomPosition;
		}
		return this.m_TileMapObject.GetRandomPosition (radius);
	}

	#endregion

}
