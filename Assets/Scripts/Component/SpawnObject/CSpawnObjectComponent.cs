using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Events.Utils;
using Ludiq.Reflection;

public class CSpawnObjectComponent : CComponent {

	#region Internal class

	[System.Serializable]
	public class UnityEnvenObject : UnityEvent<GameObject> {}

	#endregion

	#region Fields

	[Header("Configs")]
	[SerializeField]	protected CSpawnObjectData[] m_SpawnObjects;
	public CSpawnObjectData[] spawnObjects {
		get { return this.m_SpawnObjects; }
		set { this.m_SpawnObjects = value; }
	}

	[Header("Events")]
	[Filter(Fields = true, Properties = true, Methods = true)]
	public UnityMember OnSetLoaded;
	public UnityEnvenObject OnSpawned;

	#endregion

	#region Implementation Component

	protected override void Awake ()
	{
		base.Awake ();
	}

	#endregion

	#region Main methods

	public virtual void SpawnGameObject(string name) {
		this.SpawnObject (name);
	}

	public virtual GameObject SpawnObject(string name) {
		if (this.m_IsActive == false)
			return null;
		for (int i = 0; i < this.m_SpawnObjects.Length; i++) {
			var objData = this.m_SpawnObjects [i];
			if (objData.objectName == name) {
				var objInstantiate = Instantiate (objData.objectPrefab);
				if (this.OnSetLoaded.isAssigned) {
					this.OnSetLoaded.InvokeOrSet (objInstantiate);
				}
				if (this.OnSpawned != null) {
					this.OnSpawned.Invoke (objInstantiate);
				}
				return objInstantiate;
			}
		}
		return null;
	}

	#endregion

}
