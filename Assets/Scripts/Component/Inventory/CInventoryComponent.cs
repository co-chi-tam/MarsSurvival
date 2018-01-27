﻿using System;
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
		get { return this.m_Items; }
		set { this.m_Items = value; }
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
	}

	#endregion

	#region Main methods

	public virtual void InstanceInventory(List<CItemData> items) {
		this.m_Items = items; 
	}

	public virtual bool CheckAmountItem(int amount, CItemData value) {
		for (int i = 0; i < this.m_Items.Count; i++) {
			var item = this.m_Items[i];
			if (item.amount >= amount && item.itemName == value.itemName) {
				return true;
			}
		}
		return false;
	}

	public virtual void UseItem(int amount, CItemData value) {
		for (int i = 0; i < this.m_Items.Count; i++) {
			var item = this.m_Items[i];
			if (item.itemName == value.itemName) {
				item.amount -= amount;
				break;
			}
		}
	}

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
			return x.itemName == item.itemData.itemName;
		});
		// CHECK IF ITEM EXIST 
		if (storedItem == null) {
			storedItem = ScriptableObject.CreateInstance<CItemData> ();
			storedItem.itemName 	= item.itemData.itemName;
			storedItem.itemDisplayName = item.itemData.itemDisplayName;
			storedItem.avatarPath 	= item.itemData.avatarPath;
			storedItem.modelPath 	= item.itemData.modelPath;
			storedItem.amount 		= 1;
			this.m_Items.Add (storedItem);
		} else {
			storedItem.amount += item.itemData.amount;
		}
		// ITEM PICKED
		item.Picked ();
		if (this.OnPickItem != null) {
			this.OnPickItem.Invoke ();
		}
	}

	public virtual void RemoveDuplicateItem() {
		this.m_Items = this.m_Items 
			.GroupBy ((x) => new { 
				name = x.itemName,
				disName = x.itemDisplayName,
				avatar = x.avatarPath,
				model = x.modelPath,
				amount = items.Where((c) => c.itemName == x.itemName).Sum((s) => s.amount) 
			})
			.Select ((z) => new CItemData() {
				itemName = z.Key.name,
				itemDisplayName = z.Key.disName,
				avatarPath = z.Key.avatar,
				modelPath = z.Key.model,
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

