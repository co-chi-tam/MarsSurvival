using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CDayNightComponent : CComponent {

	#region Internal class

	[System.Serializable]
	public class UnityEventInt: UnityEvent<int> {}
	[System.Serializable]
	public class UnityEventString: UnityEvent<string> {}

	#endregion

	#region Fields

	[Header("Events")]
	public UnityEventInt 	OnEnterDay;
	public UnityEventInt 	OnEnterHour;
	public UnityEventInt	OnUpdateHour;
	public UnityEventString OnEnterDate;
	public UnityEvent OnDay;
	public UnityEvent OnNight;

	protected CDayNightManager m_DayNightManager;
	protected int m_CurrentDay;
	protected int m_CurrentHour;
	protected int m_UpdateHour;
	protected string m_CurrentDate;

	public bool IsLight {
		get { return this.m_CurrentHour > 4 && this.m_CurrentHour < 18; }
	}

	public bool IsDark {
		get { return this.IsLight; }
	}

	#endregion

	#region Implementation Component

	protected override void Start ()
	{
		base.Start ();
		this.m_DayNightManager = CDayNightManager.GetInstance ();
		this.m_CurrentDay = this.m_DayNightManager.day;
		this.m_CurrentHour = this.m_DayNightManager.hour24;
		this.m_CurrentDate = this.m_DayNightManager.date;
	}

	protected override void LateUpdate ()
	{
		base.Update ();
		// DAY
		if (this.m_CurrentDay != this.m_DayNightManager.day) {
			if (this.OnEnterDay != null) {
				this.OnEnterDay.Invoke (this.m_DayNightManager.day);
			}
			this.m_CurrentDay = this.m_DayNightManager.day;
		}
		// HOUR
		if (this.m_CurrentHour != this.m_DayNightManager.hour24) {
			if (this.OnEnterHour != null) {
				this.OnEnterHour.Invoke (this.m_DayNightManager.hour24);
			}
			this.m_CurrentHour = this.m_DayNightManager.hour24;
		}
		if (this.m_UpdateHour != this.m_DayNightManager.hour24) {
			if (this.OnUpdateHour != null) {
				this.OnUpdateHour.Invoke (this.m_DayNightManager.hour24);
			}
			this.m_UpdateHour = this.m_DayNightManager.hour24;
		}
		// DATE
		if (this.m_CurrentDate != this.m_DayNightManager.date) {
			if (this.OnEnterDate != null) {
				this.OnEnterDate.Invoke (this.m_DayNightManager.date);
			}
			this.m_CurrentDate = this.m_DayNightManager.date;
		}
		// DAY NIGHT
		if (this.IsLight) {
			if (this.OnDay != null) {
				this.OnDay.Invoke ();
			}
		} else {
			if (this.OnNight != null) {
				this.OnNight.Invoke ();
			}
		}
	}

	#endregion

}
