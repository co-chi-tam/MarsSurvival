﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMapEnviromentEntity : CEnviromentEntity {

	#region Fields

	[Header ("Configs")]
	[SerializeField]	protected CRenderObjectComponent m_RenderObjectComponent;

	public bool IsActiveInMap {
		get { 
			if (this.m_RenderObjectComponent != null) {
				return this.m_RenderObjectComponent.isInvisible;
			}
			return true;
		}
	}

	#endregion

	#region Implementation Entity

	#endregion

	#region Main methods

	#endregion

}
