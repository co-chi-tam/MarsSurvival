using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMachineEntity : CEntity {

	#region Fields

	protected CDataComponent m_DataComponent;
	protected CMachineData m_Data;

	public virtual bool HaveEnergy {
		get { 
			if (this.m_Data == null)
				return true; 
			return this.m_Data.powerPoint > 0;
		}
	}

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
