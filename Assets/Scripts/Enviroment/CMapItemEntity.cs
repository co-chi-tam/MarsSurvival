using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMapItemEntity : CItemEntity {

	#region Fields

	[Header ("Configs")]
	[SerializeField]	protected float m_MapItemPercent = 100f;
	[SerializeField]	protected CRenderObjectComponent m_RenderObjectComponent;

	public bool IsActiveInMap {
		get { 
			if (this.m_RenderObjectComponent != null) {
				return Random.Range (0f, 100f) <= this.m_MapItemPercent 
					&& this.m_RenderObjectComponent.isInvisible;
			}
			return true;
		}
	}

	#endregion

	#region Main methods

	public override void ApplyDamage (float value) {
		base.ApplyDamage (value);
	}

	#endregion

}
