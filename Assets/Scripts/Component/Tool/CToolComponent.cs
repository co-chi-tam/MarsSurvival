using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Ludiq.Reflection;

public class CToolComponent : CComponent {

	#region Fields

	[Header("Configs")]
	[SerializeField]	protected string m_ToolActiveName = "Empty";
	public string toolActiveName {
		get { return this.m_ToolActiveName; }
		set { this.m_ToolActiveName = value; }
	}
	[SerializeField]	protected string m_ToolPositionName = "RightHand";
	public string toolPositionName {
		get { return this.m_ToolPositionName; }
		set { this.m_ToolPositionName = value; }
	}

	[Header("Events")]
	public UnityEvent OnUse;

	#endregion

	#region Main methods

	public virtual string UseTool () {
		if (this.OnUse != null) {
			this.OnUse.Invoke ();
		}
		return this.m_ToolActiveName;
	}

	#endregion

	#region Override

	public override int GetHashCode ()
	{
		return base.GetHashCode ();
	}

	public override bool Equals (object other)
	{
		if (other is CComponent) {
			return this.name == (other as CComponent).name;
		}
		return base.Equals (other);
	}

	#endregion

}
