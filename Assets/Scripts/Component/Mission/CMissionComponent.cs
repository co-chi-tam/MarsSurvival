using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CMissionComponent : CComponent {

	#region Internal Class

	[Serializable]
	public class UnityEventMission : UnityEvent <CMissionData> {}

	#endregion

	#region Fields

	[Header ("Configs")]
	[SerializeField]	protected bool m_DisplayResult = false;
	[SerializeField]	protected string m_DisplatResultPattern = "OK";
	[SerializeField]	protected CMissionData m_Data;
	public CMissionData data {
		get { return this.m_Data; }
		set { this.m_Data = value; }
	}
	public string conditionFullDetail {
		get { 
			if (this.m_Data == null)
				return string.Empty;
			var result = this.m_Data.mission.missionName + "\n";
			result += this.m_Data.mission.missionDetail + "\n";
			var conditions = this.m_Data.mission.missionConditions;
			for (int i = 0; i < conditions.Length; i++) {
				result += "  +" + conditions [i].conditionValue.conditionDetail
					+ (this.m_DisplayResult 
						? (this.IsConditionCorrect (i) ? this.m_DisplatResultPattern : string.Empty) + "\n" : "\n");
			}
			return result;
		}
	}

	[Header ("Data")]
	[SerializeField]	protected CMissionInputData[] m_InputDatas;
	public CMissionInputData[] inputDatas {
		get { return this.m_InputDatas; }
		set { this.m_InputDatas = value; }
	}
	[Header ("Events")]
	public UnityEventMission OnCompleteMission;
	public UnityEventMission OnHoldMission;

	protected Dictionary<string, CMissionInputData> m_SampleInputs;

	#endregion

	#region Implementation Component

	protected override void Awake ()
	{
		base.Awake ();
		this.m_SampleInputs = new Dictionary<string, CMissionInputData> ();
		this.InitMission ();
	}

	protected override void LateUpdate ()
	{
		base.LateUpdate ();
		if (this.m_IsActive) {
			this.UpdateMission ();
		}
	}

	#endregion

	#region Main methods

	public virtual void InitMission() {
		for (int i = 0; i < this.m_InputDatas.Length; i++) {
			var inputData = this.m_InputDatas [i];
			if (this.m_SampleInputs.ContainsKey (inputData.inputName) == false) {
				this.m_SampleInputs.Add (inputData.inputName, inputData);
			}
		}
	}

	public virtual void UpdateMission() {
		if (this.m_Data == null)
			return;
		if (this.IsConditionCorrect ()) {
			if (this.OnCompleteMission != null) {
				this.OnCompleteMission.Invoke (this.m_Data);
			}
		} else {
			if (this.OnHoldMission != null) {
				this.OnHoldMission.Invoke (this.m_Data);
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
			result &= this.IsConditionCorrect (i);
		}
		return result;
	}

	public virtual bool IsConditionCorrect(int index) {
		var mission = this.m_Data.mission;
		var conditions = mission.missionConditions;
		var cond = conditions [index];
		var condMethod = cond.conditionMethod;
		if (this.m_SampleInputs.ContainsKey (condMethod) == false)
			return false;
		var inputMethod = this.m_SampleInputs [condMethod].inputMethod;
		if (inputMethod.isAssigned == false)
			return false;
		if (cond.conditionValue is CVector3Condition) {
			var conditionValue = cond.conditionValue as CVector3Condition;
			var condTargetMethod = inputMethod.Get<Vector3> ();
			return condTargetMethod == conditionValue.conditionV3;
		} else if (cond.conditionValue is CObtainItemCondition) {
			var conditionValue = cond.conditionValue as CObtainItemCondition;
			var condTargetMethod = inputMethod.GetOrInvoke<string> (conditionValue.conditionStr);
			return condTargetMethod == conditionValue.conditionStr;
		} else if (cond.conditionValue is CInteractiveMachineCondition) {
			var conditionValue = cond.conditionValue as CInteractiveMachineCondition;
			var condTargetMethod = inputMethod.GetOrInvoke<string> (conditionValue.conditionName);
			return condTargetMethod == conditionValue.conditionName;
		} else {
			return false;
		}
	}

	public virtual void AbandonMission() {
		this.m_Data = null;
	}

	#endregion

}
