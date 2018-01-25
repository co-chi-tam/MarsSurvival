﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Ludiq.Reflection;

public class CUIInfoDisplay : MonoBehaviour {

	#region Fields

	[Header("Configs")]
	[SerializeField]	protected Image m_ValueImage;
	[Header("Events")]
	public UnityEvent OnUpdateDisplay;

	public float value {
		get { 
			if (this.m_ValueImage == null)
				return 0f;
			return this.m_ValueImage.fillAmount;
		}
		set { 
			if (this.m_ValueImage == null)
				return;
			this.m_ValueImage.fillAmount = value;
		}
	}

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
		var lerpValue = Mathf.Lerp (this.m_ValueImage.fillAmount, value, 0.5f);
		this.m_ValueImage.fillAmount = lerpValue;
	}

	#endregion

}