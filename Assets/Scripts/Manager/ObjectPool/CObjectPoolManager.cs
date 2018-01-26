using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Events.Utils;
using Ludiq.Reflection;
using SimpleSingleton;
using ObjectPool;

public class CObjectPoolManager : CMonoSingleton<CObjectPoolManager> {

	#region Internal clas

	[System.Serializable]
	public class UnityEventObjectMember: UnityEvent<CObjectPoolMemberComponent> {}

	#endregion

	#region Fields

	[Header("Configs")]
	[SerializeField]	protected List<CObjectPoolItem> m_ObjectPoolInstances;
	public List<CObjectPoolItem> objectPoolInstances {
		get { return this.m_ObjectPoolInstances; }
		set { this.m_ObjectPoolInstances = value; }
	}

	[Header("Events")]
	public UnityEventObjectMember OnGet;
	public UnityEventObjectMember OnSet;

	protected Dictionary<string, ObjectPool<CObjectPoolMemberComponent>> m_ObjectPools;

	#endregion

	#region Implementation MonoBehaviour

	protected override void Awake ()
	{
		base.Awake ();
		this.m_ObjectPools = new Dictionary<string, ObjectPool<CObjectPoolMemberComponent>> ();
	}

	#endregion

	#region Main methods

	public virtual void InitObjectPool()  {
		for (int i = 0; i < this.m_ObjectPoolInstances.Count; i++) {
			var item = this.m_ObjectPoolInstances [i];
			for (int x = 0; x < item.itemMaximum; x++) {
				var itemPrefab = Instantiate (item.itemPrefab);
				this.Set (name, itemPrefab);
				itemPrefab.gameObject.SetActive (false);
			}
		}
	}

	public virtual void GetMember(string name) {
		this.Get (name);
	}

	public virtual CObjectPoolMemberComponent Get(string name) {
		CObjectPoolMemberComponent member = null;
		var maximumMember = 0;
		if (this.m_ObjectPools.ContainsKey (name)) {
			member = this.m_ObjectPools [name].Get ();
			maximumMember = this.m_ObjectPools [name].Count ();
		}
		if (member == null) {
			for (int i = 0; i < this.m_ObjectPoolInstances.Count; i++) {
				var item = this.m_ObjectPoolInstances [i];
				if (item.itemName == name
				    && maximumMember < item.itemMaximum) {
					var itemPrefab = Instantiate (item.itemPrefab);
					this.Set (item.itemName, itemPrefab);
					member = this.m_ObjectPools [name].Get ();
					member.StartMember ();
					break;
				}
			}
		} else {
			member.StartMember ();
		}
		if (this.OnGet != null && member != null) {
			this.OnGet.Invoke (member);
		}
		return member;
	}

	public virtual void Set(string name, CObjectPoolMemberComponent value) {
		if (this.m_ObjectPools.ContainsKey (name)) {
			// TODO
		} else {
			this.m_ObjectPools.Add (name, new ObjectPool<CObjectPoolMemberComponent>());
		} 
		if (this.OnSet != null) {
			this.OnSet.Invoke (value);
		}
		this.m_ObjectPools[name].Set (value);
	}

	#endregion

}
