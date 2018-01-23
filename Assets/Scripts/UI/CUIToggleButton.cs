using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Ludiq.Reflection.Internal;

public class CUIToggleButton : Toggle {

	[Header("Events")]
	public UnityEvent OnTrue;
	public UnityEvent OnFalse;

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

}
