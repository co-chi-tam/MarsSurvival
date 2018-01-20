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

	[SerializeField]	protected bool m_IsStarted = false;
	public virtual bool IsStarted {
		get { return this.m_IsStarted; }
		set { this.m_IsStarted = value; }
	}

	public override bool IsActive {
		get { return base.IsActive; }
		set { base.IsActive = value; }
	}

	#endregion

	#region Implementation Entity

	protected override void Start ()
	{
		base.Start ();
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

	public override void SetAnimation(int value) {
		base.SetAnimation (value);
	}

	#endregion

}
