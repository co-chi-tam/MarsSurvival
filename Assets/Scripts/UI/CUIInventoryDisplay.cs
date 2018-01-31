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

	#endregion

	#region Main methods

	public virtual void UpdateInventory(List<CItemData> items) {
		for (int i = 0; i < this.m_DisplayItems.Count; i++) {
			var itemDisplay = this.m_DisplayItems [i];
			if (i < items.Count) {
				var itemChild = items [i];
				if (itemChild == null) {
					itemDisplay.gameObject.SetActive (false);
					continue;
				}
				itemDisplay.itemImage = Resources.Load<Sprite> (itemChild.avatarPath);
				itemDisplay.itemName = string.Format ("{0} x{1}", itemChild.itemDisplayName, itemChild.amount);
				itemDisplay.gameObject.SetActive (itemChild.amount > 0);
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
