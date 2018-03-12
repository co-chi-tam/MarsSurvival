using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGameSwitchScene : MonoBehaviour {

	protected CSceneManager m_SceneManager;
	protected string m_StartScene = "StartScene";
	protected string m_SpaceScene = "SpaceScene";
	protected string m_MarsScene = "MarsScene";

	protected virtual void Start() {
		this.m_SceneManager = CSceneManager.GetInstance ();
	}

	public void LoadScene() {
		var sceneStatus = PlayerPrefs.GetString ("SCENE_STATUS", this.m_StartScene);
		if (sceneStatus == this.m_StartScene) {
			PlayerPrefs.SetString ("SCENE_STATUS", this.m_SpaceScene);
			this.m_SceneManager.LoadSceneAsync (this.m_SpaceScene);
		} else {
			PlayerPrefs.SetString ("SCENE_STATUS", this.m_MarsScene);
			this.m_SceneManager.LoadSceneAsync (this.m_MarsScene);
		} 
		PlayerPrefs.Save ();
	}

}
