using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CUISolarDisplay : MonoBehaviour {

	[Header("Configs")]
	[SerializeField]	protected Image m_ValueImage;

	protected CGameDataManager m_GameDataManager;

	protected virtual void Start() {
		this.m_GameDataManager = CGameDataManager.GetInstance ();
	}

	protected virtual void LateUpdate() {
		this.m_ValueImage.fillAmount = (float) this.m_GameDataManager.OnSolarPoint.Get ();
	}

}
