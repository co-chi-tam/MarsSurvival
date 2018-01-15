using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleSingleton;

public class CGameSetting : CMonoSingleton<CGameSetting> {

	#region Implementation CMonoSingleton

	protected override void Awake () {
		base.Awake ();
	}

	#endregion

}
