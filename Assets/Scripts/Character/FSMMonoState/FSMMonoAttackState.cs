﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

public class FSMMonoAttackState : FSMMonoBaseState {

	#region Fields

	public override bool IsAfterShortTime {
		get {
			return base.IsAfterShortTime;
		}
	}

	#endregion

	#region Implementation IState

	public override void StartState ()
	{
		base.StartState ();
	}

	#endregion

	#region Main methods

	#endregion

}
