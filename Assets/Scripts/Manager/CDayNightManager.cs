using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleSingleton;

public class CDayNightManager : CMonoSingleton<CDayNightManager> {

	#region Fields

	[Header("Config")]
	[SerializeField]	protected bool m_IsActive = true;
	public bool isActive {
		get { return this.m_IsActive; }
		set { this.m_IsActive = value; }
	}
	[SerializeField]	protected float m_MinusPerDay = 20;
	public float minusPerDay {
		get { return this.m_MinusPerDay; }
		set { this.m_MinusPerDay = value; }
	}
	[SerializeField]	protected Light m_Sun;
	[SerializeField]	protected float m_MinIntensity = 0f;
	[SerializeField]	protected float m_MaxIntensity = 1f;
	[SerializeField]	protected AnimationCurve m_LightCurve;
	[SerializeField]	protected Gradient m_DayNightLightColor;
	[Header("Data")]
	[SerializeField]	protected int m_Day = 0;
	public int day {
		get { return this.m_Day; }
		set { this.m_Day = value; }
	}
	protected int m_DaySaved = 0;
	[SerializeField]	protected int m_Hour24 = 0;
	public int hour24 {
		get { return this.m_Hour24; }
		set { this.m_Hour24 = value; }
	}
	[SerializeField]	protected string m_Date = "AM";
	public string date {
		get { return this.m_Date; }
		set { this.m_Date = value; }
	}
	protected float m_TimerDayInterval = 0f;
	protected float m_ADay = 0f;
	protected float m_AHour24 = 0f;
	protected float m_DeltaHour = 0f;

	protected string m_WORLD_TIME = "M01_WORLD_TIME";
	protected string m_WORLD_DAY = "M01_WORLD_DAY";

	#endregion

	#region Implementation Component

	protected override void Awake ()
	{
		base.Awake ();
		this.Setup ();
		this.Load ();
	}

	protected virtual void Update() {
		if (this.m_IsActive == false)
			return;
		this.m_TimerDayInterval += Time.deltaTime;
		this.m_Day = this.m_DaySaved + (int)(this.m_TimerDayInterval / this.m_ADay);
		this.m_Hour24 = (int)(this.m_TimerDayInterval / this.m_AHour24) % 24;
		this.m_Date = this.m_Hour24 < 12f ? "AM" : "PM";
	}

	protected virtual void LateUpdate() {
		this.m_DeltaHour = this.m_Hour24 / 24f;
		var deltaIntensity = this.m_LightCurve.Evaluate (this.m_DeltaHour);
		var lightIntensity = (this.m_MaxIntensity - this.m_MinIntensity) * deltaIntensity + this.m_MinIntensity;

		var sunIntensity = Mathf.Lerp (this.m_Sun.intensity, lightIntensity, Time.deltaTime);
		this.m_Sun.intensity = sunIntensity;
		RenderSettings.ambientIntensity = sunIntensity;

		var sunColor = Color.Lerp (this.m_Sun.color, this.m_DayNightLightColor.Evaluate(this.m_DeltaHour), Time.deltaTime);
		this.m_Sun.color = RenderSettings.ambientLight = sunColor;
	}

	protected virtual void OnApplication() {
		this.Save ();
	}

	protected virtual void OnDestroy() {
		this.Save ();
	}

	#endregion

	#region Main methods

	public virtual void Setup() {
		this.m_ADay = this.m_MinusPerDay * 60f;
		this.m_AHour24 = this.m_ADay / 24f;
	}

	public virtual void Load() {
		this.m_TimerDayInterval = PlayerPrefs.GetFloat (this.m_WORLD_TIME, this.m_MinusPerDay * 10f);
		this.m_DaySaved = PlayerPrefs.GetInt (this.m_WORLD_DAY, this.m_DaySaved);
		this.m_Hour24 = (int)(this.m_TimerDayInterval / this.m_AHour24) % 24;
		this.m_Date = this.m_Hour24 < 12f ? "AM" : "PM";
	}

	public virtual void Save() {
		this.m_TimerDayInterval = this.m_TimerDayInterval % this.m_ADay;
		PlayerPrefs.SetFloat (this.m_WORLD_TIME, this.m_TimerDayInterval);
		PlayerPrefs.SetInt (this.m_WORLD_DAY, this.m_Day);
		PlayerPrefs.Save ();
	}

	public virtual void Reset() {
		PlayerPrefs.DeleteKey (this.m_WORLD_TIME);
		PlayerPrefs.DeleteKey (this.m_WORLD_DAY);
		PlayerPrefs.Save ();
	}

	#endregion

}
