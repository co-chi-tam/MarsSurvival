﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Reflection;

[Serializable]
public class CMachineData : CGameEntityData {

	#region Fields

	[Header("Machine Fields")]
	[SerializeField]	protected string m_MachineName;
	public string machineName {
		get { return this.m_MachineName; }
		set { this.m_MachineName = value; }
	}

	[SerializeField]	protected string[] m_MachineJobs;
	public string[] machineJobs {
		get { return this.m_MachineJobs; }
		set { 
			this.m_MachineJobs = new string[value.Length]; 
			value.CopyTo (this.m_MachineJobs, 0);
		}
	}

	[SerializeField]	protected CAmountItem[] m_ActiveWithItems;
	public CAmountItem[] activeWithItems {
		get { return this.m_ActiveWithItems; }
		set { this.m_ActiveWithItems = value; }
	}

	#endregion

	#region Constructor

	public CMachineData (): base() {
		this.m_MachineName	= "Empty name";
	}

	public CMachineData (SerializationInfo info, StreamingContext context) : base (info, context)
	{

	}

	#endregion

	#region Override

	public override string ToString ()
	{
		return string.Format ("[CMachineData] machineName: {0}", this.m_MachineName);
	}

	#endregion

}
