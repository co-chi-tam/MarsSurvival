﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Ludiq.Reflection;

public class CStoreToolComponent : CComponent {

	#region Internal class

	[Serializable]	
	public class CToolPositions
	{
		public string toolPositionName;
		public GameObject toolPosition;
	}

	#endregion

	#region Fields

	[Header("Configs")]
	[SerializeField]	protected CToolPositions[] m_ToolPositions;
	public CToolPositions[] toolPositions {
		get { return this.m_ToolPositions; }
		set { this.m_ToolPositions = value; }
	}
	[SerializeField]	protected CToolData m_CurrentData;
	public CToolData currentToolData {
		get { return this.m_CurrentData; }
	}
	[SerializeField]	protected CToolComponent m_CurrentTool;
	public CToolComponent currentTool {
		get { return this.m_CurrentTool; }
		set { this.m_CurrentTool = value; }
	}
	public bool haveTool {
		get { return this.m_CurrentTool != null; }
	}
	[SerializeField]	protected CToolInstanceItem[] m_InstanceTools;
	[SerializeField]	protected List<CToolComponent> m_ReloadTool;
	[SerializeField]	protected CToolMethodItem[] m_ToolMethods;
	public CToolMethodItem[] toolMethods {
		get { return this.m_ToolMethods; }
		set { this.m_ToolMethods = value; }
	}

	[Header("Events")]
	public UnityEvent OnLoaded;
	public UnityEvent OnDrop;

	#endregion

	#region Implementation Component

	

	#endregion

	#region Main methods

	public virtual void UseTool() {
		if (this.m_CurrentTool == null)
			return;
		var toolMethod = this.m_CurrentTool.UseTool ();
		for (int i = 0; i < this.m_ToolMethods.Length; i++) {
			var method = this.m_ToolMethods [i];
			if (toolMethod == method.methodName) {
				if (method.method.isAssigned) {
					method.method.InvokeOrSet (this.m_CurrentData);
				}
				break;
			}
		}
	}

	public virtual void DropTool () {
		if (this.m_CurrentTool == null)
			return;
		if (this.OnDrop != null) {
			this.OnDrop.Invoke ();
		}
		this.m_CurrentTool.gameObject.SetActive (false);
		this.m_CurrentTool = null;
	}

	public virtual void LoadTool (CToolData value) {
		if (value == null)
			return;
		this.LoadTool (value.entityName);
	}

	public virtual void LoadTool (string name) {
		if (string.IsNullOrEmpty (name))
			return;
		for (int i = 0; i < this.m_ReloadTool.Count; i++) {
			var tool = this.m_ReloadTool [i];
			tool.gameObject.SetActive (false);
		}
		if (this.m_CurrentTool != null) {
			this.m_CurrentTool.gameObject.SetActive (false);
		}
		this.m_CurrentTool = null;
		this.m_CurrentData = null;
		// LOAD TOOL
		for (int i = 0; i < this.m_InstanceTools.Length; i++) {
			var data = this.m_InstanceTools [i];
			if (data.toolName == name) {
				this.m_CurrentData = data.toolData;
				this.LoadToolComponent (data.toolPrefab);
				break;
			}
		}
	}

	protected virtual void LoadToolComponent (CToolComponent value) {
		// REUSE TOOL
		if (this.m_ReloadTool.Contains (value)) {
			for (int i = 0; i < this.m_ReloadTool.Count; i++) {
				var tool = this.m_ReloadTool [i];
				if (tool.Equals (value)) {
					this.m_CurrentTool = tool;
					this.m_CurrentTool.gameObject.SetActive (true);
					// EVENTS
					if (this.OnLoaded != null) {
						this.OnLoaded.Invoke ();
					}
					return;
				}
			}
		}
		// INSTANTIATE
		var toolInstance = Instantiate (value);
		var holdToolPosition = this.GetToolPosition (value.toolPositionName);
		this.m_CurrentTool = toolInstance;
		toolInstance.transform.SetParent (holdToolPosition);
		toolInstance.transform.localPosition = Vector3.zero;
		toolInstance.transform.localRotation = Quaternion.Euler (Vector3.zero);
		toolInstance.transform.localScale = Vector3.one;
		toolInstance.gameObject.SetActive (true);
		toolInstance.gameObject.name = value.gameObject.name;
		this.m_ReloadTool.Add (toolInstance);
		// EVENTS
		if (this.OnLoaded != null) {
			this.OnLoaded.Invoke ();
		}
	}

	public virtual Transform GetToolPosition(string name) {
		for (int i = 0; i < this.m_ToolPositions.Length; i++) {
			if (this.m_ToolPositions [i].toolPositionName == name)
				return this.m_ToolPositions [i].toolPosition.transform;
		}
		return this.m_Transform;
	}

	#endregion

}
