using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Events.Utils;
using Ludiq.Reflection;

public class CUIEntityInteractive : MonoBehaviour {

	#region Fields

	[Header("Configs")]
	[SerializeField]	protected CEntity m_Entity;
	protected CEntity m_PreviousEntity;
	public CEntity entity {
		get { return this.m_Entity; }
		set { this.m_Entity = value; }
	}
	public Transform entityEntityTransform {
		get {
			if (this.m_Entity == null)
				return null;
			return this.m_Entity.transform;
		}
	}
	[SerializeField]	protected CUIFollowObject m_UIFollowObject;
	[SerializeField]	protected CUIJobDisplay m_UIJobDisplay;
	[SerializeField]	protected CUIToggleButton m_UIFollowToggle;
	[SerializeField]	protected CUIToggleButton m_UIStartToggle;
	[SerializeField]	protected CUIInfoDisplay m_UIEnergyDisplay;
	[SerializeField]	protected CUIInfoDisplay m_UIDirtDisplay;
	[SerializeField]	protected CUIInfoDisplay m_UIGatherCarrotDisplay;
	[SerializeField]	protected CUIInfoDisplay m_UIAddCarrotDisplay;
	[SerializeField]	protected CUIInfoDisplay m_UIAddCactusDisplay;

	[Header("Events")]
	public UnityEvent OnInteractive;
	public UnityEvent OnFree;

	protected bool entityStarted {
		get {
			if (this.m_Entity == null)
				return false;
			if (this.m_Entity is CMachineEntity) {
				var machine = this.m_Entity as CMachineEntity;
				return machine.IsStarted;
			}
			return false;
		}
		set { 
			if (this.m_Entity == null)
				return;
			if (this.m_Entity is CMachineEntity) {
				var machine = this.m_Entity as CMachineEntity;
				machine.IsStarted = value;
			}
		}
	}

	protected string[] entityJobs {
		get {
			if (this.m_Entity == null)
				return new string[0];
			if (this.m_Entity is CMachineEntity) {
				var machine = this.m_Entity as CMachineEntity;
				return machine.GetJobs();
			}
			return new string[0];
		}
	}

	#endregion

	#region Implementation MonoBehaviour

	protected virtual void LateUpdate() {
		// UPDATE
		if (this.m_Entity != null) {
			if (this.OnInteractive != null) {
				this.OnInteractive.Invoke ();
			}
			this.UpdateUI ();
		} else {
			if (this.OnFree != null) {
				this.OnFree.Invoke ();
			}
		}
		// CONNECT
		if (this.m_PreviousEntity != this.m_Entity) {
			this.ConnectUI ();
			this.m_PreviousEntity = this.m_Entity;
		}
	}

	#endregion

	#region Main methods

	public virtual void ConnectUI () {
		if (this.m_Entity != null) {
			this.m_UIFollowObject.follow = this.m_Entity.transform;
			this.m_UIJobDisplay.SetUpJobDisplay (this.m_Entity.IsActive, this.entityJobs);
			if (this.m_Entity is CMachineEntity) {
				var machine = this.m_Entity as CMachineEntity;
				// FOLLOW
				this.m_UIFollowToggle.isOn = machine.isFollowing;
				if (this.m_UIFollowToggle.onValueChanged != null) {
					this.m_UIFollowToggle.onValueChanged.Invoke (machine.isFollowing);
				}
				// START
				this.m_UIStartToggle.isOn = machine.IsStarted;
				if (this.m_UIStartToggle.onValueChanged != null) {
					this.m_UIStartToggle.onValueChanged.Invoke (machine.IsStarted);
				}
			}
		} else {
			this.m_UIFollowObject.follow = null;
		}
	}

	public virtual void UpdateUI() {
		if (this.m_Entity is CMachineEntity) {
			var machine = this.m_Entity as CMachineEntity;
			if (this.m_UIEnergyDisplay.gameObject.activeInHierarchy) {
				this.m_UIEnergyDisplay.SetDisplay ("-1", machine.energyPercent);
			}
			if (this.m_UIDirtDisplay.gameObject.activeInHierarchy) {
				this.m_UIDirtDisplay.SetDisplay ("-1", machine.energyPercent);
			}
			if (this.m_UIGatherCarrotDisplay.gameObject.activeInHierarchy) {
				this.m_UIGatherCarrotDisplay.SetDisplay (
					"+" + Mathf.FloorToInt (machine.collectPercent), 
					machine.collectPercent
				);
			}
			if (this.m_UIAddCarrotDisplay.gameObject.activeInHierarchy) {
				this.m_UIAddCarrotDisplay.SetDisplay ("-1", machine.energyPercent);
			}
			if (this.m_UIAddCactusDisplay.gameObject.activeInHierarchy) {
				this.m_UIAddCactusDisplay.SetDisplay ("-1", machine.energyPercent);
			}
		}
	}

	#endregion

}
