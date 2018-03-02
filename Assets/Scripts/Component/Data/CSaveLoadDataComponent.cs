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
	public UnityEvent OnBeforeLoad;
	public UnityEvent OnLoad;
	public UnityEvent OnBeforeSave;
	public UnityEvent OnSave;
	public UnityEvent OnComplete;
	public UnityEvent OnFail;

	#endregion

	#region Implementation Component

	protected override void Awake () {
		base.Awake ();
		if (this.m_InstanceData) {
			this.m_CloneData = ScriptableObject.Instantiate (this.m_InstanceData);
		}
	}

	protected override void Start ()
	{
		base.Start ();
		if (this.m_AutoSaveLoad) {
			this.Load ();
		}
	}

	protected override void OnApplicationPause (bool value)
	{
		base.OnApplicationPause (value);
		if (this.m_AutoSaveLoad && value) {
			this.Save ();
		}
	}

	protected override void OnApplicationFocus (bool value)
	{
		base.OnApplicationFocus (value);
		if (this.m_AutoSaveLoad && value == false) {
			this.Save ();
		}
	}

	protected override void OnApplicationQuit ()
	{
		base.OnDisable ();
		if (this.m_AutoSaveLoad) {
			this.Save ();
		}
	}

	#endregion

	#region Main methods

	public virtual void DeleteSaveFile() {
		if (Directory.Exists (this.GetSavePath ())) {
			var files = Directory.GetFiles (this.GetSavePath ());
			for (int i = 0; i < files.Length; i++) {
				File.Delete (files [i]);
			}
			if (this.OnComplete != null) {
				this.OnComplete.Invoke ();
			}
		} else {
			if (this.OnFail != null) {
				this.OnFail.Invoke ();
			}
		}
	}

	public virtual bool Load() {
		if (File.Exists (this.GetFullSavePath ())) {
			if (this.OnBeforeLoad != null) {
				this.OnBeforeLoad.Invoke ();
			}
			var fileStream = File.Open (this.GetFullSavePath (), FileMode.OpenOrCreate);
			var binaryFormt = new BinaryFormatter ();
			var data = (ScriptableObject) binaryFormt.Deserialize (fileStream);
			this.m_CloneData = ScriptableObject.Instantiate (data);
			fileStream.Close ();
			if (this.OnLoad != null) {
				this.OnLoad.Invoke ();
			}
			if (this.OnComplete != null) {
				this.OnComplete.Invoke ();
			}
			return true;
		}
		if (this.OnFail != null) {
			this.OnFail.Invoke ();
		}
		return false;
	}

	public virtual bool Save() {
		if (this.m_CloneData == null)
			return false;
		if (Directory.Exists (this.GetSavePath ()) == false) 
			Directory.CreateDirectory (this.GetSavePath ());
		try {
			if (this.OnBeforeSave != null) {
				this.OnBeforeSave.Invoke ();
			}
			var fileStream = File.Open (this.GetFullSavePath (), FileMode.OpenOrCreate);
			var binaryFormt = new BinaryFormatter ();
			binaryFormt.Serialize (fileStream, this.m_CloneData);
			fileStream.Close ();
			if (this.OnSave != null) {
				this.OnSave.Invoke ();
			}
			if (this.OnComplete != null) {
				this.OnComplete.Invoke ();
			}
			return true;
		} catch {
			if (this.OnFail != null) {
				this.OnFail.Invoke ();
			}
			return false;
		}
	}

	#endregion

	#region Getter & Setter

	public virtual string GetSavePath() {
		return string.Format ("{0}/Save", Application.persistentDataPath);
	}

	public virtual string GetFullSavePath() {
		return string.Format ("{0}/{1}.dat", this.GetSavePath(), this.m_SaveFile);
	}

	#endregion

}
