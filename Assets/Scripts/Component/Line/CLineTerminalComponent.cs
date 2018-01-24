using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Events.Utils;
using Ludiq.Reflection;

[RequireComponent(typeof(CPhysicDetectComponent))]
public class CLineTerminalComponent : CComponent {

	#region Internal Class

	[Serializable]
	public class UnityEventInt: UnityEvent<int> {}

	#endregion

	#region Fields

	[SerializeField]	protected Transform m_Source;

	[Header("Line")]
	[SerializeField]	protected int m_MaximumLine = 1;
	[SerializeField]	protected LayerMask m_GroundLayerMask;
	[SerializeField]	protected float m_GroundRadius = 1f;
	[SerializeField]	protected float m_GroundHeight = 50f;
	[SerializeField]	protected LineRenderer m_LinePrefabs;
	[SerializeField]	protected LineRenderer[] m_LineRenderers;

	[Header("Events")]
	public UnityEventInt OnConnected;
	public UnityEvent OnFree;

	protected CPhysicDetectComponent m_PhysicDetectComponent;
	protected float m_SegmentOffset;
	protected RaycastHit[] m_HitInfoSamples;

	#endregion

	#region Implementation Component

	protected override void Awake ()
	{
		base.Awake ();
		this.m_PhysicDetectComponent = this.GetComponent<CPhysicDetectComponent> ();
		this.m_LineRenderers = new LineRenderer[this.m_MaximumLine];
		this.m_SegmentOffset = 1f / this.m_PhysicDetectComponent.detectRadius;
	}

	protected override void Start ()
	{
		base.Start ();
		// INIT LINE
		this.InitLine ();
	}

	protected override void LateUpdate ()
	{
		base.LateUpdate ();
		// AUTO DRAW LINE
		if (this.m_IsActive) {
			this.DrawLine ();
		}
	}

	#endregion

	#region Main methods

	protected virtual void InitLine() {
		var max = (int)this.m_PhysicDetectComponent.detectRadius;
		for (int i = 0; i < this.m_LineRenderers.Length; i++) {
			var lineGo = Instantiate (this.m_LinePrefabs);
			var lineRenderer = lineGo.GetComponent<LineRenderer> ();
			lineRenderer.positionCount = max;
			for (int x = 0; x < lineRenderer.positionCount; x++) {
				lineRenderer.SetPosition (x, this.m_Transform.position);
			}
			this.m_LineRenderers [i] = lineRenderer;
			lineGo.transform.SetParent (this.m_Transform);
			lineGo.transform.localPosition = Vector3.zero;
			lineGo.gameObject.SetActive (false);
		}
		this.m_LinePrefabs.gameObject.SetActive (false);
		this.m_HitInfoSamples = new RaycastHit[max];
	}

	public virtual void DrawLine() {
		// DETECT PHYSIC
		var detectCount = this.m_PhysicDetectComponent.colliderCount;
		var isFree = true;
		// FREE LINE
		this.EraseLine ();
		var colliderIndex = 0;
		for (int i = 0; i < detectCount; i++) {
			if (colliderIndex >= this.m_LineRenderers.Length)
				break;
			// LINE RENDERER
			var line = this.m_LineRenderers[colliderIndex];
			// DRAW LINE
			var targetObject = this.m_PhysicDetectComponent.sampleColliders [i];
			var lineEnd = targetObject.GetComponent<CLineEndComponent> ();
			// CONNECTED
			if (lineEnd != null 
				&& lineEnd.ConnectedLine (this)) {
				for (int x = 0; x < line.positionCount; x++) {
					var lerp = Vector3.Lerp (this.m_Source.position, lineEnd.transform.position, this.m_SegmentOffset * x);
					var groundHitCount = Physics.RaycastNonAlloc (lerp + (Vector3.up * this.m_GroundHeight), Vector3.down, this.m_HitInfoSamples, 100f, this.m_GroundLayerMask);
					if (groundHitCount > 0) {
						var hitInfo = this.m_HitInfoSamples [0];
						lerp.y = hitInfo.point.y + this.m_GroundRadius;
					}
					line.SetPosition (x, lerp);
				}
				line.gameObject.SetActive (true);
				isFree = false;
				colliderIndex++;
			}
		}
		// EVENTS
		if (isFree) {
			if (this.OnFree != null) {
				this.OnFree.Invoke ();
			}
		} else {
			if (this.OnConnected != null) {
				this.OnConnected.Invoke (colliderIndex);
			}
		}
	}

	public void EraseLine() {
		for (int i = 0; i < this.m_LineRenderers.Length; i++) {
			var line = this.m_LineRenderers [i];
			line.gameObject.SetActive (false);
		}
	}

	#endregion

}
