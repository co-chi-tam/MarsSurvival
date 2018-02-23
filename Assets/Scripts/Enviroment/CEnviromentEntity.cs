using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnviromentEntity : CGameEntity {

	#region Fields

	protected CObjectPoolMemberComponent m_ObjectPoolMember;
	protected CDataComponent m_DataComponent;
	protected CEnviromentData m_Data;

	#endregion

	#region Implementation Entity

	protected override void Awake ()
	{
		base.Awake ();
		this.m_ObjectPoolMember = this.GetGameComponent <CObjectPoolMemberComponent> ();
		this.m_DataComponent = this.GetGameComponent<CDataComponent> ();
	}

	protected override void Start ()
	{
		base.Start ();
		this.m_Data = this.m_DataComponent.Get<CEnviromentData> ();
	}

	#endregion

	#region Main methods

	public override void ApplyDamage (float value) {
		if (this.m_Data == null) {
			base.ApplyDamage (value);
			return;
		}
		this.m_Data.currentDamage += value;
		if (this.m_Data.currentDamage >= this.m_Data.maximumDamage) {
			base.ApplyDamage (value);
		}
	}

	public override void ResetEntity() {
		if (this.m_Data == null) {
			base.ResetEntity ();
			return;
		}
		this.m_Data.currentDamage = 0f;
	}

	public virtual void SpawnResourceContains() {	
		if (this.m_Data == null)
			return;
		for (int i = 0; i < this.m_Data.resourceContains.Length; i++) {
			var resource = this.m_Data.resourceContains [i];
			var resourceName = resource.itemData.entityName;
			var obj = this.m_ObjectPoolMember.Get (resourceName);
			if (obj != null) {
				obj.transform.position = this.transform.position;
			}
		}
	}

	#endregion

}
