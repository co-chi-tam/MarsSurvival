using UnityEngine;
using System;
using System.Runtime.Serialization;
using System.Reflection;

[Serializable]
public class CMachineData : CGameEntityData {

	#region Fields

	[Header("Machine Fields")]

	[SerializeField]	protected bool m_IsActive;
	public bool isActive {
		get { return this.m_IsActive; }
		set { this.m_IsActive = value; }
	}

	[SerializeField]	protected bool m_IsStart;
	public bool isStart {
		get { return this.m_IsStart; }
		set { this.m_IsStart = value; }
	}

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

	#region Getter && Setter

	public override void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		base.GetObjectData (info, context);
	}

	#endregion

	#region Override

	public override string ToString ()
	{
		return string.Format ("[CMachineData] machineName: {0}", this.m_MachineName);
	}

	#endregion

}
