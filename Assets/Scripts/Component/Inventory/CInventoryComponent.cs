using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CPhysicDetectComponent))]
public class CInventoryComponent : CComponent {

	#region Fields

	[Header("Configs")]
	[SerializeField]	protected LayerMask m_ItemLayerMask = -1;
	[SerializeField]	protected List<CItemData> m_Items;
	public List<CItemData> items {
		get { 
			if (this.m_Items == null) {
				this.m_Items = new List<CItemData> ();
			}
			return this.m_Items;
		}
		set { this.m_Items = new List<CItemData> (value); }
	}

	protected CPhysicDetectComponent m_PhysicDetect;

	#endregion

	#region Implementation Component

	protected override void Awake ()
	{
		base.Awake ();
		this.m_PhysicDetect = this.GetComponent<CPhysicDetectComponent> ();
		this.m_Items = new List<CItemData> ();
	}

	#endregion

	#region Main methods

	public virtual void PickItem() {
		if (this.m_PhysicDetect.colliderCount == 0)
			return;
		var sampleColliders = this.m_PhysicDetect.sampleColliders;
		var coll = sampleColliders.FirstOrDefault ((x) => {
			return x != null 
				&& x.GetComponent <CItemEntity> () != null;
		});
		if (coll != null) {
			var item = coll.GetComponent <CItemEntity> ();
			if (item != null) {
				this.PickItem (item);
			}
		}
	}

	public virtual void PickItem(CItemEntity item) {
		var storedItem = this.m_Items.Find ((x) => {
			return x.itemName == item.name;
		});
		if (storedItem == null) {
			this.m_Items.Add (new CItemData() {
				itemName = item.itemData.itemName,
				itemAvatar = item.itemData.itemAvatar,
				itemAmount = 1
			});
		} else {
//			storedItem.itemName = item.itemData.itemName;
//			storedItem.itemAvatar = item.itemData.itemAvatar;
			storedItem.itemAmount += item.itemData.itemAmount;
		}

		Debug.Log (storedItem != null);
	}

	public virtual void RemoveDuplicateItem() {
		this.m_Items = items
			.GroupBy ((x) => new { 
				name = x.itemName,
				avatar = x.itemAvatar,
				amount = items.Where((c) => c.itemName == x.itemName).Sum((s) => s.itemAmount) 
			})
			.Select ((z) => new CItemData() {
				itemName = z.Key.name,
				itemAvatar = z.Key.avatar,
				itemAmount = z.Key.amount
			})
			.ToList ();
	}

	public virtual void SortItem() {
		this.m_Items = this.m_Items
			.OrderBy ((x) => x.itemName)
			.OrderBy ((y) => y.itemAmount)
			.ToList();
	}

	#endregion

}

