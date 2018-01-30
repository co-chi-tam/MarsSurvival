using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Ludiq.Reflection;

public class CDisplayMapPosition : MonoBehaviour {

	#region Fields

	[Header("Config")]
	[SerializeField]	protected Text m_MapPositionText;
	[SerializeField]	protected string m_DisplayPattern = "x{0} - y{1}";
	[Header("Events")]
	public UnityEvent OnUpdateDisplay;

	public Vector3 displayPosition {
		get { return this.transform.position; }
		set { this.UpdateDisplay (value); }
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

	#region Main methods

	public virtual void UpdateDisplay(Vector3 value) {
		this.m_MapPositionText.text = string.Format (this.m_DisplayPattern, value.x, value.z);
	}

	#endregion

}
