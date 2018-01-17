using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Events.Utils;
using Ludiq.Reflection;

public class CCharacterComponent : CComponent {

	#region Fields

	[Header("Configs")]
	[SerializeField]	protected CCharacterData m_CharacterData;
	public CCharacterData characterData {
		get { return this.m_CharacterData; }
		set { this.m_CharacterData = value; }
	}

	[Header("Events")]
	public UnityEvent OnStart;

	#endregion

	#region Implementation Component

	protected override void Awake () {
		base.Awake ();
	}

	protected override void Start ()
	{
		base.Start ();
		if (this.OnStart != null) {
			this.OnStart.Invoke ();
		}

		this.GetAttribute ();
	}

	#endregion

	#region Main methods

	protected virtual void GetAttribute() {
		if (this.m_CharacterData == null)
			return;
		// GET PROPERTIES ATTRIBUTE
		var fields = this.m_CharacterData.GetType ().GetFields ();
		foreach (var fld in fields) {
			foreach (var attr in fld.GetCustomAttributes(
				typeof (CCharacterData.MarkerValueAttribute), false)) {
				var marker = attr as CCharacterData.MarkerValueAttribute;
				Debug.Log (marker.ToString ());
			}
		}
	}


	#endregion

}
