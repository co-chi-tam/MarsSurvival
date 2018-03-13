using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CToolEntity : CGameEntity {

	#region Fields

	protected CDataComponent m_DataComponent;
	protected CToolComponent m_ToolComponent;
	protected CToolData m_Data;

	#endregion

	#region Implementation Entity

	protected override void Awake ()
	{
		base.Awake ();
		this.m_DataComponent = this.GetGameComponent<CDataComponent> ();
		this.m_ToolComponent = this.GetGameComponent<CToolComponent> ();
	}

	protected override void Start ()
	{
		base.Start ();
		this.m_Data = this.m_DataComponent.Get<CToolData> ();
		this.m_ToolComponent.toolActiveName = this.m_Data.toolMethod;
		this.m_ToolComponent.toolPositionName = this.m_Data.toolPosition;
	}

	#endregion

}
