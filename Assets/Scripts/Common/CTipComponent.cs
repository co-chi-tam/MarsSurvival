using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CTipComponent : CComponent {

	[Header("Configs")]
	[SerializeField]	protected string[] m_Tips;
	[SerializeField]	protected Text m_TipText;

	public virtual void LoadRandomTip() {
		var index = Random.Range (0, this.m_Tips.Length);
		this.LoadTip (index);
	}

	public virtual void LoadTip(int index) {
		this.m_TipText.text = this.m_Tips[index];
	}

}
