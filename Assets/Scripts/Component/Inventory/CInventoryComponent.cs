using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Events.Utils;

[RequireComponent(typeof(CPhysicDetectComponent))]
public class CInventoryComponent : CComponent {

	#region Fields

	[Header("Inventory")]
	[SerializeField]	protected LayerMask m_ItemLayerMask = -1;
	[SerializeField]	protected List<CItemData> m_Items;
	public List<CItemData> items {
		get { 
			if (this.m_Items == null) {
				this.m_Items = CGameDataManager.Instance.items;
			}
			return this.m_Items;
		}
		set { 
			CGameDataManager.Instance.items = new List<CItemData> (value);
			this.m_Items = CGameDataManager.Instance.items; 
		}
	}

	[Header("Events")]
	public UnityEvent OnNothingCanPick;
	public UnityEvent OnPickItem;

	protected CPhysicDetectComponent m_PhysicDetect;

	#endregion

	#region Implementation Component

	protected override void Awake ()
	{
		base.Awake ();
		this.m_PhysicDetect = this.GetComponent<CPhysicDetectComponent> ();
	}

	protected override void Start ()
	{
		base.Start ();
		this.m_Items = CGameDataManager.Instance.items; 
		CGameDataManager.Instance.RegisterCallback ("PickItem", this.PickItem);
	}

	#endregion

	#region Main methods

	public virtual void PickItem() {
		if (this.m_PhysicDetect.colliderCount == 0) {
			if (this.OnNothingCanPick != null) {
				this.OnNothingCanPick.Invoke ();
			}
			return;
		} 
		// FIND FIRST ITEM
		var sampleColliders = this.m_PhysicDetect.sampleColliders;
		var coll = sampleColliders.FirstOrDefault ((x) => {
			return x != null 
				&& x.GetComponent <CItemComponent> () != null;
		});
		// ITEM IS AVAILABLE 
		if (coll != null) {
			var item = coll.GetComponent <CItemComponent> ();
			if (item != null) {
				this.PickItem (item);
			} else {
				if (this.OnNothingCanPick != null) {
					this.OnNothingCanPick.Invoke ();
				}
			}
		} else {
			if (this.OnNothingCanPick != null) {
				this.OnNothingCanPick.Invoke ();
			}
		} 
	}

	public virtual void PickItem(CItemComponent item) {
		var storedItem = this.items.Find ((x) => {
			return x.itemName == item.name;
		});
		// CHECK IF ITEM EXIST 
		if (storedItem == null) {
			storedItem = ScriptableObject.CreateInstance<CItemData> ();
			storedItem.itemName = item.itemData.itemName;
			storedItem.avatar = item.itemData.avatar;
			storedItem.model = item.itemData.model;
			storedItem.amount = 1;
			this.m_Items.Add (storedItem);
		} else {
			storedItem.amount += item.itemData.amount;
		}
		// ITEM PICKED
		item.Picked ();
		if (this.OnPickItem != null) {
			this.OnPickItem.Invoke ();
		}
		CGameDataManager.Instance.InvokeCallback ("UpdateInventory");
	}

	public virtual void RemoveDuplicateItem() {
		this.m_Items = this.m_Items 
			.GroupBy ((x) => new { 
				name = x.itemName,
				avatar = x.avatar,
				amount = items.Where((c) => c.itemName == x.itemName).Sum((s) => s.amount) 
			})
			.Select ((z) => new CItemData() {
				itemName = z.Key.name,
				avatar = z.Key.avatar,
				amount = z.Key.amount
			})
			.ToList ();
	}

	public virtual void SortItem() {
		this.m_Items = this.m_Items
			.OrderBy ((x) => x.itemName)
			.OrderBy ((y) => y.amount)
			.ToList();
	}

	#endregion

}

