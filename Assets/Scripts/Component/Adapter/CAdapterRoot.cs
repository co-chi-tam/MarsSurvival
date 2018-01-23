using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleSingleton;

public class CAdapterRoot : MonoBehaviour {

	#region Singleton

	protected static CAdapterRoot m_Instance;
	private static object m_SingletonObject = new object();
	public static CAdapterRoot Instance {
		get { 
			lock (m_SingletonObject) {
				if (m_Instance == null) {
					var	go = new GameObject ();
					m_Instance = go.AddComponent<CAdapterRoot> ();

					go.SetActive (true);
					go.name = "AdapterRoot";
				}
				return m_Instance;
			}
		}
	}

	public static CAdapterRoot GetInstance() {
		return Instance;
	}

	#endregion

	#region Fields

	protected Transform m_Transform;

	#endregion

	#region Implementation Monobehaviour

	protected virtual void Awake() {
		m_Instance = this;
		this.m_Transform = this.transform;
	} 

	#endregion

	#region Main methods



	#endregion

}
