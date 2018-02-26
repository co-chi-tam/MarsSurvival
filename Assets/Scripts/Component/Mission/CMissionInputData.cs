using System;
using UnityEngine;
using UnityEngine.Events;
using Ludiq.Reflection;

[Serializable]
public class CMissionInputData {

	[SerializeField]	protected string m_InputName;
	public string inputName {
		get { return this.m_InputName; }
		set { this.m_InputName = value; }
	}

	[Filter(Fields = true, Properties = true, Methods = true)]
	public UnityMember inputMethod;

}
