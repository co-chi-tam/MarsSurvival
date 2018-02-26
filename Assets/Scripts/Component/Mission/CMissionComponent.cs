using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CMissionComponent : CComponent {

	[Header ("Configs")]
	[SerializeField]	protected GameObject m_Context;
	[SerializeField]	protected CMissionData m_Data;

	[Header ("Data")]
	[SerializeField]	protected CMissionInputData[] m_InputDatas;
	public CMissionInputData[] inputDatas {
		get { return this.m_InputDatas; }
		set { this.m_InputDatas = value; }
	}
	[Header ("Event")]
	public UnityEvent OnCompleteMission;

	protected Dictionary<string, CMissionInputData> m_SampleInputs;

	protected override void Awake ()
	{
		base.Awake ();
		this.m_SampleInputs = new Dictionary<string, CMissionInputData> ();
		this.InitMission ();
	}

	protected override void Start ()
	{
		base.Start ();
	}

	protected override void LateUpdate ()
	{
		base.LateUpdate ();
		if (Input.GetKeyDown (KeyCode.M)) {
			this.UpdateMission ();
		}
	}

	public virtual void InitMission() {
		for (int i = 0; i < this.m_InputDatas.Length; i++) {
			var inputData = this.m_InputDatas [i];
			if (this.m_SampleInputs.ContainsKey (inputData.inputName) == false) {
				this.m_SampleInputs.Add (inputData.inputName, inputData);
			}
		}
	}

	public virtual void UpdateMission() {
		if (this.IsConditionCorrect ()) {
			if (this.OnCompleteMission != null) {
				this.OnCompleteMission.Invoke ();
			}
		}
	}
		
	public virtual bool IsConditionCorrect() {
		if (this.m_Data == null)
			return false;
		var mission = this.m_Data.mission;
		var conditions = mission.missionConditions;
		var result = true;
		for (int i = 0; i < conditions.Length; i++) {
			var cond = conditions [i];
			var condMethod = cond.conditionMethod;
			if (this.m_SampleInputs.ContainsKey (condMethod) == false)
				return false;
			var inputMethod = this.m_SampleInputs [condMethod].inputMethod;
			if (inputMethod.isAssigned == false)
				return false;
			if (cond.conditionValue is CVector3Condition) {
				var conditionValue = cond.conditionValue as CVector3Condition;
				var condTargetMethod = inputMethod.Get<Vector3> ();
				result &= condTargetMethod == conditionValue.conditionV3;
			} else if (cond.conditionValue is CObtainItemCondition) {
				var conditionValue = cond.conditionValue as CObtainItemCondition;
				var condTargetMethod = inputMethod.GetOrInvoke<string> (conditionValue.conditionStr);
				result &= condTargetMethod == conditionValue.conditionStr;
			} else {
				result = false;
			}
		}
		return result;
	}

}
