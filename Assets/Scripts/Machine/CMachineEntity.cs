using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMachineEntity : CEntity {

	#region Fields

	[SerializeField]	protected bool m_HaveEnergy;
	public virtual bool HaveEnergy {
		get { return this.m_HaveEnergy; }
		set { this.m_HaveEnergy = value; }
	}

	protected CDataComponent m_DataComponent;
	protected CMachineData m_Data;

	#endregion

	#region Implementation Entity

	protected override void Start ()
	{
		base.Start ();
		this.m_DataComponent = this.GetGameComponent<CDataComponent> ();
		this.m_Data = this.m_DataComponent.Get<CMachineData>();
	}

	protected override void Update ()
	{
		base.Update ();
	}

	protected override void LateUpdate ()
	{
		base.LateUpdate ();
	}

	#endregion

	#region Getter && Setter

	#endregion

}
