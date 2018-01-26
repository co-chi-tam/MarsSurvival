using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Events.Utils;

public class CItemComponent : CComponent {

	#region Fields

	[Header("Configs")]
	[SerializeField]	protected CItemData m_ItemData;
	public CItemData itemData {
		get { return this.m_ItemData; }
		set { this.m_ItemData = value; }
	}

	[Header("Events")]
	public UnityEvent OnPicked;

	#endregion

	#region Implementation Component

	protected override void Awake () {
		base.Awake ();
	}

	#endregion

	#region Main methods

	public virtual void Picked() {
		if (this.m_IsActive == false)
			return;
		if (this.OnPicked != null) {
			this.OnPicked.Invoke ();
		}
	}

	#endregion

}
