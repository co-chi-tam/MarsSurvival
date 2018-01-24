using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Ludiq.Reflection.Internal;

public class CUIToggleButton : Toggle {

	#region Fields

	[Header("Events")]
	public UnityEvent OnTrue;
	public UnityEvent OnFalse;

	public virtual bool IsOnValue {
		get { return this.isOn; }
		set { this.isOn = value; }
	}

	#endregion

	#region Implementation Toggle

	protected override void Awake ()
	{
		base.Awake ();
		this.onValueChanged.AddListener ((val) => {
			if (val) {
				if (this.OnTrue != null) {
					this.OnTrue.Invoke ();
				}
			} else {
				if (this.OnFalse != null) {
					this.OnFalse.Invoke ();
				}
			}
		});
	}

	#endregion

}
