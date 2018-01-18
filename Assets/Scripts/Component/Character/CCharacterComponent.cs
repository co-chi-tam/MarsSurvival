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
	[SerializeField]	protected CCharacterData m_CurrentCharacterData;
	protected CCharacterData m_CloneData;
	public CCharacterData characterData {
		get { return this.m_CloneData; }
		set { this.m_CloneData = value; }
	}

	protected Dictionary<string, Func<object, object, object>> m_UpdateMethods;

	protected float m_TimerPerSecond = 1f;

	#endregion

	#region Implementation Component

	protected override void Awake () {
		base.Awake ();
		this.m_CloneData = ScriptableObject.Instantiate (this.m_CurrentCharacterData) as CCharacterData;

		this.m_UpdateMethods = new Dictionary<string, Func<object, object, object>> ();
		this.m_UpdateMethods.Add ("None", this.UpdateNothing);
		this.m_UpdateMethods.Add ("Decrease", this.UpdateDecrease);
		this.m_UpdateMethods.Add ("Increase", this.UpdateIncrease);
	}

	protected override void Start ()
	{
		base.Start ();
	}

	protected override void Update ()
	{
		base.Update ();
		if (this.m_TimerPerSecond > 0) {
			this.m_TimerPerSecond -= Time.deltaTime;
		} else {
			this.UpdateByAttribute ();
			this.m_TimerPerSecond = 1f;
		}
	}

	#endregion

	#region Main methods

	protected virtual object UpdateNothing(object value, object updateValue) {
		return value;
	}

	protected virtual object UpdateDecrease (object value, object updateValue) {
		if (value is int) {
			var intValue = (int)value - (int)updateValue;
			return intValue;
		} else if (value is float) {
			var floatValue = (float)value - (float)updateValue;
			return floatValue;
		} else if (value is string) {
			var stringValue = value.ToString ().Replace (updateValue.ToString (), "");
			return stringValue;
		} 
		return value;
	}

	protected virtual object UpdateIncrease (object value, object updateValue) {
		if (value is int) {
			var intValue = (int)value + (int)updateValue;
			return intValue;
		} else if (value is float) {
			var floatValue = (float)value + (float)updateValue;
			return floatValue;
		} else if (value is string) {
			var stringValue = string.Format ("{0} {1}", value, updateValue);
			return stringValue;
		} 
		return value;
	}

	protected virtual void UpdateByAttribute() {
		if (this.m_CurrentCharacterData == null || this.m_CloneData == null)
			return;
		// GET PROPERTIES ATTRIBUTE
		var fields = this.m_CloneData.GetType ().GetProperties ();
		foreach (var fld in fields) {
			foreach (var attr in fld.GetCustomAttributes(
				typeof (MarkerValueAttribute), false)) {
				var marker = attr as MarkerValueAttribute;
				var value = this.m_UpdateMethods [marker.updateMethod] (fld.GetValue(this.m_CloneData, null), marker.updateValuePerSecond);
				fld.SetValue (this.m_CloneData, value, null);
			}
		}
		Debug.Log (this.m_CloneData.ToString ());
	}


	#endregion

}
