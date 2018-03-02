using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using Ludiq.Reflection;
using SimpleSingleton;

public class CSceneManager : CMonoSingleton<CSceneManager> {

	#region Internal class

	[Serializable]
	public class UnityEventString : UnityEvent <string> {}

	#endregion

	#region Fields

	[Header("Events")]
	public UnityEventString OnLoadScene;
	public UnityEventString OnEndLoadScene;

	protected WaitForSeconds m_WaitShortTime = new WaitForSeconds (2f);

	public string sceneName {
		get { return SceneManager.GetActiveScene ().name; }
	}

	public Scene scene {
		get { return SceneManager.GetActiveScene (); }
	}

	#endregion

	#region Implementation Component

	protected override void Awake ()
	{
		base.Awake ();
	}

	public virtual void LoadScene(string name) {
		if (this.OnLoadScene != null) {
			this.OnLoadScene.Invoke (name);
		}
		SceneManager.LoadScene (name);
		if (this.OnEndLoadScene != null) {
			this.OnEndLoadScene.Invoke (name);
		}
	}

	#endregion

	#region Main methods

	public virtual void LoadSceneAsync(string name) {
		StartCoroutine (this.HandleLoadSceneAsyn (name));
	}

	protected virtual IEnumerator HandleLoadSceneAsyn(string name) {
		if (this.OnLoadScene != null) {
			this.OnLoadScene.Invoke (name);
		}
		yield return this.m_WaitShortTime;
		yield return SceneManager.LoadSceneAsync (name);
		if (this.OnEndLoadScene != null) {
			this.OnEndLoadScene.Invoke (name);
		}
	}

	#endregion

}
