using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMotherBoxEntity : CMachineEntity {

	#region Fields

	public override bool IsActive {
		get { return this.m_IsActive; }
		set { this.m_IsActive = value; }
	}

	public override bool IsStarted {
		get {
			return base.IsStarted; }
		set { base.IsStarted = value; }
	}

	public override bool HaveEnergy {
		get { return base.HaveEnergy; }
		set { base.HaveEnergy = value; }
	}

	#endregion

	#region Implementation Entity

	protected override void Awake ()
	{
		base.Awake ();
	}

	protected override void Start ()
	{
		base.Start ();
	}

	#endregion

	#region Main methods



	#endregion

}
