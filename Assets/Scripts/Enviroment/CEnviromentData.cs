using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnviromentData : CGameEntityData {

	[Header("Enviroment Fields")]
	public virtual float currentDamage {
		get { return 0f; }
		set {  }
	}
	public virtual float maximumDamage {
		get { return 0f; }
		set {  }
	}
	public virtual CAmountItem[] resourceContains {
		get { return null; }
		set {  }
	}

}
