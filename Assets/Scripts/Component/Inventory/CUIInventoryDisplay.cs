using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Events.Utils;

public class CUIInventoryDisplay : MonoBehaviour {

	#region Fields

	[Header("Configs")]
	[SerializeField]	protected CUIItemDisplay m_ItemPrefabs;
	[SerializeField]	protected GameObject m_ItemRoot;
	[SerializeField]	protected List<CUIItemDisplay> m_DisplayItems;

	[Header("Events")]
	public UnityEvent OnUpdateInventory;

	#endregion

	#region Implementation MonoBehaviour

	protected virtual void Awake() {
		
	}

	protected virtual void Start() {
		
	}

	#endregion

	#region Main methods

	public virtual void UpdateInventory() {
		var items = CGameDataManager.Instance.items;
		for (int i = 0; i < this.m_DisplayItems.Count; i++) {
			var itemDisplay = this.m_DisplayItems [i];
			if (i < items.Count) {
				var itemChild = items [i];
				itemDisplay.itemImage = itemChild.avatar;
				itemDisplay.itemName = string.Format ("{0} x{1}", itemChild.itemName, itemChild.amount);
				itemDisplay.gameObject.SetActive (true);
			} else {
				itemDisplay.gameObject.SetActive (false);
			}
		}
		if (this.OnUpdateInventory != null) {
			this.OnUpdateInventory.Invoke ();
		}
	}

	#endregion

}
