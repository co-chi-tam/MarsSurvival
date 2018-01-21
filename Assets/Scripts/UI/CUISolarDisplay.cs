using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Ludiq.Reflection;

public class CUISolarDisplay : MonoBehaviour {

	#region Fields

	[Header("Configs")]
	[SerializeField]	protected Image m_ValueImage;
	public UnityEvent OnUpdateDisplay;

	#endregion

	#region Implementation MonoBehaviour

	protected virtual void Start() {
		
	}

	protected virtual void LateUpdate() {
		// EVENTS
		if (this.OnUpdateDisplay != null) {
			this.OnUpdateDisplay.Invoke ();
		}
	}

	#endregion

	#region Getter && Setter

	public virtual void SetDisplay(float value) {
		this.m_ValueImage.fillAmount = value;
	}

	#endregion

}
