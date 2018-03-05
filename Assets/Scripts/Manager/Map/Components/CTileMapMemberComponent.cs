using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Ludiq.Reflection;

public class CTileMapMemberComponent : CComponent {

	#region Internal class

	[Serializable]
	public class UnityEventVector3 : UnityEvent <Vector3> {}

	#endregion

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
	public UnityEventVector3 OnReloadPosition;
	public UnityEvent OnFree;

	#endregion

	#region Implementation Component

	protected override void LateUpdate ()
	{
		base.LateUpdate ();
		if (this.IsInTileMap ()) {
			if (this.OnFree != null) {
				this.OnFree.Invoke ();
			}
		}
	}

	#endregion

	#region Main methods

	public virtual bool IsInTileMap() {
		if (this.m_TileMapObject == null) 
			return false;
		var centerPosition = this.m_Transform.position;
		var position = this.m_TileMapObject.transform.position;
		return (centerPosition - position).sqrMagnitude < this.m_Radius * this.m_Radius;
	}

	public virtual void LoadTileMap(CTileMapObject value) {
		this.m_TileMapObject = value;
		if (this.OnReload != null) {
			this.OnReload.Invoke ();
		}
		if (this.OnReloadPosition != null) {
			this.OnReloadPosition.Invoke (value.transform.position);
		}
	}

	public virtual void ApplyPosition() {
		this.transform.position = this.m_TileMapObject == null 
			? Vector3.zero 
			: this.m_TileMapObject.transform.position;
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
			var randomVector = UnityEngine.Random.insideUnitCircle;
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
