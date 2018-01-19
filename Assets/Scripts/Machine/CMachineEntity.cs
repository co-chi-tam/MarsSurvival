using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMachineEntity : CEntity {

	#region Fields

	[Header("Info")]
	[SerializeField]	protected CAnimatorComponent m_AnimatorComponent;
	[SerializeField]	protected CDataComponent m_CharacterComponent;

	protected CMachineData m_Data;

	#endregion

	#region Implementation Entity

	protected override void Start ()
	{
		base.Start ();
		this.m_Data = this.m_CharacterComponent.Get<CMachineData>();
	}

	protected override void Update ()
	{
		base.Update ();
	}

	protected override void LateUpdate ()
	{
		base.LateUpdate ();
	}

	#endregion

	#region Getter && Setter

	#endregion

}
