using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CUIItemDisplay : MonoBehaviour {

	#region Fields

	[Header("Configs")]
	[SerializeField]	protected Image m_ItemImage;
	public Sprite itemImage {
		get { return this.m_ItemImage.sprite; }
		set { this.m_ItemImage.sprite = value; }
	}
	[SerializeField]	protected Text m_ItemName;
	public string itemName {
		get { return this.m_ItemName.text; }
		set { this.m_ItemName.text = value; }
	}
	[SerializeField]	protected Button m_ItemButton;
	public Button itemButton {
		get { return this.m_ItemButton; }
		protected set { this.m_ItemButton = value; }
	}

	#endregion

}
