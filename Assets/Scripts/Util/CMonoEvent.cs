using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent()]
public class CMonoEvent : MonoBehaviour {

	#region Fields

	[Header("Events")]
	public UnityEvent OnAwake;
	public UnityEvent OnStart;
	public UnityEvent OnFixedUpdate;
	public UnityEvent OnUpdate;
	public UnityEvent OnLateUpdate;

	#endregion

	#region Implementation MonoBehaviour

	protected virtual void Awake() {
		if (this.OnAwake != null) {
			this.OnAwake.Invoke ();
		}
	}

	protected virtual void Start() {
		if (this.OnStart != null) {
			this.OnStart.Invoke ();
		}
	}

	protected virtual void FixedUpdate() {
		if (this.OnFixedUpdate != null) {
			this.OnFixedUpdate.Invoke ();
		}
	}

	protected virtual void Update() {
		if (this.OnUpdate != null) {
			this.OnUpdate.Invoke ();
		}
	}

	protected virtual void LateUpdate() {
		if (this.OnLateUpdate != null) {
			this.OnLateUpdate.Invoke ();
		}
	}

	#endregion

}
