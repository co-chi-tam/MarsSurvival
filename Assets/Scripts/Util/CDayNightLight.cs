using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDayNightLight: MonoBehaviour {

	[Header("Config")]
	[SerializeField]	protected bool m_IsActive;
	public bool isActive {
		get { return this.m_IsActive; }
		set { this.m_IsActive = value; }
	}

	public Gradient nightDayColor;

	public float maxIntensity = 1f;
	public float minIntensity = 0f;
	public float minPoint = -0.2f;

	public float maxAmbient = 1f;
	public float minAmbient = 0f;
	public float minAmbientPoint = -0.2f;

	public Vector3 dayRotateSpeed = Vector3.one;
	public Vector3 nightRotateSpeed = Vector3.one;

	public float skySpeed = 1;

	Light mainLight;

	void Start () 
	{
		mainLight = GetComponent<Light>();
		skySpeed = 1f / skySpeed;
	}

	void Update () 
	{
		if (this.m_IsActive == false)
			return;
		float tRange = 1 - minPoint;
		float dot = Mathf.Clamp01 ((Vector3.Dot (mainLight.transform.forward, Vector3.down) - minPoint) / tRange);
		float i = ((maxIntensity - minIntensity) * dot) + minIntensity;

		mainLight.intensity = i;

		tRange = 1 - minAmbientPoint;
		dot = Mathf.Clamp01 ((Vector3.Dot (mainLight.transform.forward, Vector3.down) - minAmbientPoint) / tRange);
		i = ((maxAmbient - minAmbient) * dot) + minAmbient;
		RenderSettings.ambientIntensity = i;

		mainLight.color = nightDayColor.Evaluate(dot);
		RenderSettings.ambientLight = mainLight.color;

		if (dot > 0) 
			transform.Rotate (dayRotateSpeed * Time.deltaTime * skySpeed);
		else
			transform.Rotate (nightRotateSpeed * Time.deltaTime * skySpeed);

//		if (Input.GetKeyDown (KeyCode.Q)) skySpeed *= 0.5f;
//		if (Input.GetKeyDown (KeyCode.E)) skySpeed *= 2f;
	}

	public virtual void SetUpdate(int hour) {
	
	}

}
