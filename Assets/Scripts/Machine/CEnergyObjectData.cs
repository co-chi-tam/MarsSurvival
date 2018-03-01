using System;
using System.Runtime.Serialization;
using System.Reflection;
using UnityEngine;

[Serializable]
public class CEnergyObjectData: ISerializable {

	#region Fields

	[Header("Energy Fields")]
	[SerializeField]	protected float m_EnergyPoint = 100f;
	[Info(valueName = "Energy point", valueMin = 0f, valueMax = 999f)]
	[UpdateValuePerInvoke(updateName = "AddEnergy", updateMethod = "Increase", updateValuePerInvoke = 20f)]
	[UpdateValuePerInvoke(updateName = "UseEnergy", updateMethod = "Decrease", updateValuePerInvoke = 0.5f)]
	public virtual float energyPoint {
		get { return this.m_EnergyPoint; }
		set { this.m_EnergyPoint = value < 0f ? 0f : value > 999f ? 999f : value; }
	}

	[SerializeField]	protected float m_MaxEnergyPoint = 100f;
	[Info(valueName = "Max energy point", valueMin = 0f, valueMax = 999f)]
	public virtual float maxEnergyPoint {
		get { return this.m_MaxEnergyPoint; }
		set { this.m_MaxEnergyPoint = value; }
	}

	[SerializeField]	protected CAmountItem[] m_ItemsPerCharge;
	public virtual CAmountItem[] itemsPerCharge {
		get { return this.m_ItemsPerCharge; }
		set { this.m_ItemsPerCharge = value; }
	}

	#endregion

	#region Constructor

	public CEnergyObjectData () {
		this.m_EnergyPoint = 100f;
		this.m_MaxEnergyPoint = 100f;
	}

	public CEnergyObjectData (SerializationInfo info, StreamingContext context)
	{
		foreach (FieldInfo field in this.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.Instance))
		{
			field.SetValue(this, info.GetValue(field.Name, field.FieldType));
		}
	}

	#endregion

	#region Getter && Setter

	public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		foreach(FieldInfo field in this.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.Instance))
		{
			info.AddValue(field.Name, field.GetValue(this), field.FieldType);
		}
	}

	#endregion

}
