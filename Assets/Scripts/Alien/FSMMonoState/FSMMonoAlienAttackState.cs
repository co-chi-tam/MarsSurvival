using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

public class FSMMonoAlienAttackState : FSMMonoBaseState {

	#region Fields

	public override bool IsAfterShortTime {
		get { return base.IsAfterShortTime; }
	}

	#endregion

	#region Implementation MonoState

	public override void StartState ()
	{
		base.StartState ();
	}

	#endregion

}
