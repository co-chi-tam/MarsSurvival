using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Events.Utils;
using Ludiq.Reflection;

public class CDataComponent : CComponent {

	#region Fields

	[Header("Configs")]
	[Range(1, 999)]
	[SerializeField]	protected int m_UpdateDeltaSpeed = 1;
	public int updateDeltaSpeed {
		get { return this.m_UpdateDeltaSpeed; }
		set { this.m_UpdateDeltaSpeed = value < 1 ? 1 : value > 999 ? 999 : value; }
	}
	[SerializeField]	protected ScriptableObject m_InstanceData;
	protected ScriptableObject m_CloneData;
	public ScriptableObject cloneData {
		get { return this.m_CloneData; }
		protected set { this.m_CloneData = value; }
	}

	protected Dictionary<string, Func<object, object, object>> m_UpdateMethods;

	protected float m_TimerPerSecond = 1f;
	protected float m_TimerPerInvoke = 1f;

	#endregion

	#region Implementation Component

	protected override void Awake () {
		base.Awake ();
		this.m_CloneData = ScriptableObject.Instantiate (this.m_InstanceData);

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
		if (this.m_IsActive) {
			this.UpdateDataPerSecond (Time.deltaTime);
		}
	}

	#endregion

	#region Main methods

	protected virtual object UpdateNothing(object value, object updateValue) {
		return value;
	}

	protected virtual object UpdateDecrease (object value, object updateValue) {
		if (updateValue == null)
			return 0;
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
		if (updateValue == null)
			return 0;
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

	public virtual void UpdateDataPerSecond(float dt) {
		if (this.m_InstanceData == null || this.m_CloneData == null)
			return;
		// UPDATE PER SECOND
		if (this.m_TimerPerSecond > 0) {
			this.m_TimerPerSecond -= dt * this.m_UpdateDeltaSpeed;
			return;
		} else {
			this.m_TimerPerSecond = 1f;
		}
		// GET PROPERTIES ATTRIBUTE
		var fields = this.m_CloneData.GetType ().GetProperties ();
		foreach (var fld in fields) {
			foreach (var attr in fld.GetCustomAttributes(typeof (UpdateValuePerSecondAttribute), false)) {
				var marker = attr as UpdateValuePerSecondAttribute;
				if (this.m_UpdateMethods.ContainsKey (marker.updateMethod)) {
					var value = this.m_UpdateMethods [marker.updateMethod] (fld.GetValue (this.m_CloneData, null), marker.updateValuePerSecond);
					fld.SetValue (this.m_CloneData, value, null);
				}
			}
		}
	}

	public virtual void UpdateDataPerInvoke(string name) {
		if (this.m_InstanceData == null || this.m_CloneData == null)
			return;
		// UPDATE PER SECOND
		if (this.m_TimerPerInvoke > 0) {
			this.m_TimerPerInvoke -= Time.deltaTime * this.m_UpdateDeltaSpeed;
			return;
		} else {
			this.m_TimerPerInvoke = 1f;
		}
		// GET PROPERTIES ATTRIBUTE
		var fields = this.m_CloneData.GetType ().GetProperties ();
		foreach (var fld in fields) {
			foreach (var attr in fld.GetCustomAttributes(typeof (UpdateValuePerInvokeAttribute), false)) {
				var marker = attr as UpdateValuePerInvokeAttribute;
				if (marker.updateName == name &&
					this.m_UpdateMethods.ContainsKey (marker.updateMethod)) {
					var value = this.m_UpdateMethods [marker.updateMethod] (
						fld.GetValue (this.m_CloneData, null), 
						marker.updateValuePerInvoke);
					fld.SetValue (this.m_CloneData, value, null);
				}
			}
		}
	}

	#endregion

	#region Getter & Setter

	public virtual T Get<T>() where T : ScriptableObject {
		return (T) Convert.ChangeType(this.m_CloneData, typeof (T));
	}

	public virtual void Set<T>(T value) where T : ScriptableObject {
		this.m_CloneData = value;
	}

	// GET PROPERTIY
	public virtual object GetProperty(string name) {
		var fields = this.m_CloneData.GetType ().GetProperty(name);
		if (fields != null) {
			return fields.GetValue (this.m_CloneData, null);
		}
		return null;
	}

	// SET PROPERTIY
	public virtual void SetProperty(string name, object value) {
		var fields = this.m_CloneData.GetType ().GetProperty(name);
		if (fields != null) {
			fields.SetValue (this.m_CloneData, value, null);
		}
	}

	#endregion

}
