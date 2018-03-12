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
	public UnityEventString OnStartScene;
	public UnityEventString OnLoadScene;
	public UnityEventString OnEndLoadScene;

	protected WaitForSeconds m_WaitShortTime = new WaitForSeconds (1f);

	public string sceneName {
		get { return SceneManager.GetActiveScene ().name; }
	}

	public Scene scene {
		get { return SceneManager.GetActiveScene (); }
	}

	protected bool m_IsSceneLoading = false;

	#endregion

	#region Implementation Component

	protected override void Awake ()
	{
		base.Awake ();
		if (this.OnStartScene != null) {
			this.OnStartScene.Invoke (name);
		}
	}

	#endregion

	#region Main methods

	public virtual void LoadScene(string name) {
		if (this.OnLoadScene != null) {
			this.OnLoadScene.Invoke (name);
		}
		SceneManager.LoadScene (name);
		if (this.OnEndLoadScene != null) {
			this.OnEndLoadScene.Invoke (name);
		}
	}

	public virtual void LoadSceneAsyncAfter(string name, float timer) {
		StartCoroutine (this.HandleLoadSceneAsyn (name, timer));
	}

	protected virtual IEnumerator HandleLoadSceneAsyn(string name, float timer) {
		yield return new WaitForSeconds (timer);
		yield return StartCoroutine (this.HandleLoadSceneAsyn (name));
	}

	public virtual void LoadSceneAsync(string name) {
		StartCoroutine (this.HandleLoadSceneAsyn (name));
	}

	protected virtual IEnumerator HandleLoadSceneAsyn(string name) {
		if (this.m_IsSceneLoading) {
			yield break;
		}
		this.m_IsSceneLoading = true;
		if (this.OnLoadScene != null) {
			this.OnLoadScene.Invoke (name);
		}
		yield return this.m_WaitShortTime;
		yield return SceneManager.LoadSceneAsync (name);
		if (this.OnEndLoadScene != null) {
			this.OnEndLoadScene.Invoke (name);
		}
		this.m_IsSceneLoading = false;
	}

	#endregion

}
