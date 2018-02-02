using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Events.Utils;
using Ludiq.Reflection;

public class CUIEntityControl : MonoBehaviour {

	#region Fields

	[Header("Configs")]
	[SerializeField]	protected Image m_ToolImage;
	[SerializeField]	protected GameObject m_ToolControl;
	[SerializeField]	protected CToolData m_ToolData;
	protected CToolData m_PreviousToolData;
	public CToolData toolData {
		get { return this.m_ToolData; }
		set { this.m_ToolData = value; }
	}

	[Header("Events")]
	public UnityEvent OnConnect;
	public UnityEvent OnFree;

	#endregion

	#region Implementation MonoBehaviour

	protected virtual void LateUpdate() {
		// UPDATE
		if (this.m_ToolData != null) {
			if (this.m_ToolData != this.m_PreviousToolData) {
				this.ConnectUI ();
				this.m_PreviousToolData = this.m_ToolData;
			}
			if (this.OnConnect != null) {
				this.OnConnect.Invoke ();
			}
		} else {
			if (this.OnFree != null) {
				this.OnFree.Invoke ();
			}
		}
		this.m_ToolControl.SetActive (this.m_ToolData != null);
	}

	#endregion

	#region Main methods

	public virtual void ConnectUI () {
		var data = this.m_ToolData;
		var avatar = Resources.Load<Sprite> (data.avatarPath);
		this.m_ToolImage.sprite = avatar;
	}

	#endregion

}
