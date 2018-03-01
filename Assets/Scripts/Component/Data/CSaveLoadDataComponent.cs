using System;
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

public class CSaveLoadDataComponent : CComponent {

	#region Fields

	[Header("Configs")]
	[SerializeField]	protected ScriptableObject m_InstanceData;
	protected ScriptableObject m_CloneData;
	public ScriptableObject cloneData {
		get { return this.m_CloneData; }
		protected set { this.m_CloneData = value; }
	}

	[Header("Save")]
	[SerializeField]	protected bool m_AutoSaveLoad = false;
	[SerializeField]	protected string m_SaveFile = Guid.NewGuid().ToString();
	public UnityEvent OnLoad;
	public UnityEvent OnSave;

	#endregion

	#region Implementation Component

	protected override void Awake () {
		base.Awake ();
		this.m_CloneData = ScriptableObject.Instantiate (this.m_InstanceData);
	}

	protected override void Start ()
	{
		base.Start ();
		if (this.m_AutoSaveLoad) {
//			Debug.Log (this.GetFullSavePath());
			if (this.Load ()) {
				if (this.OnLoad != null) {
					this.OnLoad.Invoke ();
				}
			}
		}
	}

	protected override void OnApplicationPause (bool value)
	{
		base.OnApplicationPause (value);
		if (this.m_AutoSaveLoad && value) {
			if (this.Save ()) {
				if (this.OnSave != null) {
					this.OnSave.Invoke ();
				}
			}
		}
	}

	protected override void OnApplicationFocus (bool value)
	{
		base.OnApplicationFocus (value);
		if (this.m_AutoSaveLoad && value == false) {
			if (this.Save ()) {
				if (this.OnSave != null) {
					this.OnSave.Invoke ();
				}
			}
		}
	}

	protected override void OnApplicationQuit ()
	{
		base.OnDisable ();
		if (this.m_AutoSaveLoad) {
			if (this.Save ()) {
				if (this.OnSave != null) {
					this.OnSave.Invoke ();
				}
			}
		}
	}

	#endregion

	#region Main methods

	public virtual bool Load() {
		if (File.Exists (this.GetFullSavePath ())) {
			var fileStream = File.Open (this.GetFullSavePath (), FileMode.OpenOrCreate);
			var binaryFormt = new BinaryFormatter ();
			var data = (ScriptableObject) binaryFormt.Deserialize (fileStream);
			this.m_CloneData = ScriptableObject.Instantiate (data);
			fileStream.Close ();
			return true;
		}
		return false;
	}

	public virtual bool Save() {
		if (this.m_CloneData == null)
			return false;
		if (Directory.Exists (this.GetSavePath ()) == false) 
			Directory.CreateDirectory (this.GetSavePath ());
		var fileStream = File.Open (this.GetFullSavePath (), FileMode.OpenOrCreate);
		var binaryFormt = new BinaryFormatter ();
		binaryFormt.Serialize (fileStream, this.m_CloneData);
		fileStream.Close ();
		return true;
	}

	#endregion

	#region Getter & Setter

	// SET PROPERTIY
	public virtual void SetProperty(string name, object value) {
		var fields = this.m_CloneData.GetType ().GetProperty(name);
		if (fields != null) {
			fields.SetValue (this.m_CloneData, value, null);
		}
	}

	public virtual string GetSavePath() {
		return string.Format ("{0}/Save", Application.persistentDataPath);
	}

	public virtual string GetFullSavePath() {
		return string.Format ("{0}/{1}.dat", this.GetSavePath(), this.m_SaveFile);
	}

	#endregion

}
