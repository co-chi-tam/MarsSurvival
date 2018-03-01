using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Ludiq.Reflection;

public class CUIHeartBeat : MonoBehaviour {

	[Header ("Configs")]
	[SerializeField]	protected bool m_IsActive = true;
	public bool isActive {
		get { return this.m_IsActive; }
		set { 
			this.m_IsActive = value; 
			if (value == false) {
				this.m_HeartBeatImage.fillAmount = 0f;
			}
		}
	}
	[SerializeField]	protected float m_SpeedThresholdValue = 1f;
	public float speedThresholdValue {
		get { return this.m_SpeedThresholdValue; }
		set { this.m_SpeedThresholdValue = value; }
	}
	[SerializeField]	protected Image m_HeartBeatImage;
	[SerializeField]	protected AnimationCurve m_HeartBeatCurve;

	[Header("Events")]
	public UnityEvent OnBeat;

	protected float m_Time;

	protected virtual void Update () {
		if (this.m_IsActive) {
			this.UpdateHeartBeat (Time.deltaTime);
		}
	}

	public virtual void UpdateHeartBeat(float dt) {
		var delta = Mathf.Clamp (this.m_HeartBeatCurve.Evaluate (this.m_Time), 0.1f, 1f);
		this.m_Time = (this.m_Time + dt * delta * this.m_SpeedThresholdValue) % 1f;
		this.m_HeartBeatImage.fillAmount = this.m_Time;
		if (this.m_Time < dt) {
			if (this.OnBeat != null) {
				this.OnBeat.Invoke ();
			}
		}
	}

}
