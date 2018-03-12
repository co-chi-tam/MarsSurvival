using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSpaceshipEntity : CEntity {

	#region Fields

	public virtual bool HaveEnergy {
		get { return this.m_Data.energyPoint > 0f; }
	}

	protected CDataComponent m_DataComponent;

	protected CSpaceshipData m_Data;

	#endregion

	#region Implmentation component

	protected override void Awake ()
	{
		base.Awake ();
		this.m_DataComponent = this.GetGameComponent <CDataComponent> ();
	}

	protected override void Start ()
	{
		base.Start ();
		this.m_Data = this.m_DataComponent.Get <CSpaceshipData> ();
	}

	#endregion

}
