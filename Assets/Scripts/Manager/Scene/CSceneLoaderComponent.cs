using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSceneLoaderComponent : CComponent {

	protected CSceneManager m_SceneManager;

	protected override void Start ()
	{
		base.Start ();
		this.m_SceneManager = CSceneManager.GetInstance ();
	}

	public virtual void LoadScene (string name) {
		this.m_SceneManager.LoadScene (name);
	}

	public virtual void LoadSceneAsync (string name) {
		this.m_SceneManager.LoadSceneAsync (name);
	}

}
