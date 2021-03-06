﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Events.Utils;
using Ludiq.Reflection;

public class CDataComponent : CSaveLoadDataComponent {

	#region Fields

	[Header("Configs")]
	[Range(1, 10)]
	[SerializeField]	protected int m_UpdateDeltaSpeed = 1;
	public int updateDeltaSpeed {
		get { return this.m_UpdateDeltaSpeed; }
		set { this.m_UpdateDeltaSpeed = value < 1 ? 1 : value > 999 ? 999 : value; }
	}

	protected Dictionary<string, Func<string, object, object, object>> m_UpdateMethods;
	protected Dictionary<string, Action<object>> m_ValueChanged;
	protected Queue<string> m_DelayMethods;

	protected float m_TimerPerSecond = 1f;
	protected float m_TimerPerDelay = 1f;

	#endregion

	#region Implementation Component

	protected override void Awake () {
		base.Awake ();
		this.m_UpdateMethods = new Dictionary<string, Func<string, object, object, object>> ();
		this.m_UpdateMethods.Add ("None", this.UpdateNothing);
		this.m_UpdateMethods.Add ("SetValue", this.SetValue);
		this.m_UpdateMethods.Add ("Decrease", this.UpdateDecrease);
		this.m_UpdateMethods.Add ("Increase", this.UpdateIncrease);

		this.m_ValueChanged = new Dictionary<string, Action<object>> ();

		this.m_DelayMethods = new Queue<string> ();
	}

	protected override void Start ()
	{
		base.Start ();
	}

	protected override void Update ()
	{
		base.Update ();
		if (this.m_IsActive) {
			// UPDATE PER SECOND
			this.UpdateDataPerSecond (Time.deltaTime);
		}

		if (this.m_DelayMethods.Count () > 0) {
			// DELAY
			if (this.m_TimerPerDelay > 0f) {
				this.m_TimerPerDelay -= Time.deltaTime * this.m_UpdateDeltaSpeed;
			} else {
				this.UpdateDelay ();
				this.m_TimerPerDelay = 1f;
			}
		}
	}

	#endregion

	#region Main methods

	protected virtual object UpdateNothing(string name, object value, object updateValue) {
		return value;
	}

	protected virtual object SetValue(string name, object value, object updateValue) {
		// UPDATE CALLBACK
		if (this.m_ValueChanged.ContainsKey (name)) {
			this.m_ValueChanged [name] (updateValue);		
		}
		return updateValue;
	}

	protected virtual object UpdateDecrease (string name, object value, object updateValue) {
		// UPDATE VALUE
		var resultvalue = value;
		if (updateValue == null)
			return resultvalue;
		var changeValue = updateValue;
		if (value is int) {
			resultvalue = (int)value - (int)updateValue;
			changeValue = -(int)updateValue;
		} else if (value is float) {
			resultvalue = (float)value - (float)updateValue;
			changeValue = -(float)updateValue;
		} else if (value is string) {
			resultvalue = value.ToString ().Replace (updateValue.ToString (), "");
			changeValue = updateValue.ToString ();
		} 
		// UPDATE CALLBACK
		if (this.m_ValueChanged.ContainsKey (name)) {
			this.m_ValueChanged [name] (changeValue);		
		}
		return resultvalue;
	}

	protected virtual object UpdateIncrease (string name, object value, object updateValue) {
		// UPDATE VALUE
		if (updateValue == null)
			return value;
		// UPDATE CALLBACK
		if (this.m_ValueChanged.ContainsKey (name)) {
			this.m_ValueChanged [name] (updateValue);		
		}
		if (value is int) {
			var resultValue = (int)value + (int)updateValue;
			return resultValue;
		} else if (value is float) {
			var resultValue = (float)value + (float)updateValue;
			return resultValue;
		} else if (value is string) {
			var resultValue = string.Format ("{0} {1}", value, updateValue);
			return resultValue;
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
		this.UpdateDataPerSecond (this.m_CloneData);
	}

	public virtual void UpdateDataPerSecond(object valueObj) { 
		// GET PROPERTIES ATTRIBUTE
		var fields = valueObj.GetType ().GetProperties ();
		foreach (var fld in fields) {
			var sampleValue = fld.GetValue (valueObj, null);
			if (sampleValue is int
			    || sampleValue is float
			    || sampleValue is string) {
				this.ReflectValuePerSecond (fld, valueObj, sampleValue);
			} else {
				var fieldConstinues = fld.GetCustomAttributes (typeof(UpdateContinueAttribute), false);
				if (fieldConstinues.Length > 0) {
					this.UpdateDataPerSecond (fld.GetValue (valueObj, null));
				}
			}
		}
	}

	protected virtual void ReflectValuePerSecond(PropertyInfo fld, object valueObj, object sampleValue) {
		foreach (var attr in fld.GetCustomAttributes(typeof (UpdateValuePerSecondAttribute), false)) {
			var valuePerSecond = attr as UpdateValuePerSecondAttribute;
			if (this.m_UpdateMethods.ContainsKey (valuePerSecond.updateMethod)) {
				var value = this.m_UpdateMethods [valuePerSecond.updateMethod] (
					fld.Name,
					sampleValue, 
					valuePerSecond.updateValuePerSecond);
				fld.SetValue (valueObj, value, null);
			}
		}
	}

	public virtual void UpdateDataPerInvoke(string name) {
		if (this.m_InstanceData == null || this.m_CloneData == null)
			return;
		this.UpdateDataPerInvoke (name, this.m_CloneData);
	}

	public virtual void UpdateDataPerInvoke(string name, object valueObj) {
		// GET PROPERTIES ATTRIBUTE
		var fields = valueObj.GetType ().GetProperties ();
		foreach (var fld in fields) {
			var sampleValue = fld.GetValue (valueObj, null);
			if (sampleValue is int
			    || sampleValue is float
			    || sampleValue is string) {
				this.ReflectValuePerInvoke (name, fld, valueObj, sampleValue);
			} else {
				var fieldConstinues = fld.GetCustomAttributes (typeof(UpdateContinueAttribute), false);
				if (fieldConstinues.Length > 0) {
					this.UpdateDataPerInvoke (name, fld.GetValue (valueObj, null));
				}
			}
		}
	}

	protected virtual void ReflectValuePerInvoke(string name, PropertyInfo fld, object valueObj, object sampleValue) {
		foreach (var attr in fld.GetCustomAttributes(typeof (UpdateValuePerInvokeAttribute), false)) {
			var valuePerInvoke = attr as UpdateValuePerInvokeAttribute;
			if (valuePerInvoke.updateName == name &&
				this.m_UpdateMethods.ContainsKey (valuePerInvoke.updateMethod)) {
				var value = this.m_UpdateMethods [valuePerInvoke.updateMethod] (
					fld.Name,
					sampleValue, 
					valuePerInvoke.updateValuePerInvoke);
				fld.SetValue (valueObj, value, null);
			}
		}
	}

	public virtual void UpdateDataPerInvokeWithDelay(string name) {
		// ADD DELAY
		if (this.m_DelayMethods.Contains (name) == false) {
			this.m_DelayMethods.Enqueue (name);
		}
	}

	protected virtual void UpdateDelay() {
		while (this.m_DelayMethods.Count != 0) {
			var delayName = this.m_DelayMethods.Dequeue ();
			this.UpdateDataPerInvoke (delayName);
		}
	}

	public virtual void AddListener(string name, Action<object> callback) {
		if (this.m_ValueChanged.ContainsKey (name) == false) {
			this.m_ValueChanged.Add (name, callback);
		}
	}

	public virtual void RemoveListener(string name) {
		if (this.m_ValueChanged.ContainsKey (name)) {
			this.m_ValueChanged.Remove (name);
		}
	}

	public virtual void RemoveAllListener() {
		this.m_ValueChanged.Clear ();
	}

	#endregion

	#region Getter & Setter

	public virtual T Get<T>() where T : ScriptableObject {
		return this.m_CloneData as T;
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

	public byte[] ToByteArray<T>(T obj)
	{
		if(obj == null)
			return null;
		BinaryFormatter bf = new BinaryFormatter();
		using(MemoryStream ms = new MemoryStream())
		{
			bf.Serialize(ms, obj);
			return ms.ToArray();
		}
	}

	public T FromByteArray<T>(byte[] data)
	{
		if(data == null)
			return default(T);
		BinaryFormatter bf = new BinaryFormatter();
		using(MemoryStream ms = new MemoryStream(data))
		{
			object obj = bf.Deserialize(ms);
			return (T)obj;
		}
	}

	#endregion

}
