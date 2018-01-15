using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;
using System.Collections;

namespace UnityEngine.UICustomize {
	public class UIJoytick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
	{
		#region Fields

		[SerializeField]	private Image m_BackgroundImage;
		[SerializeField]	private Image m_KnobImage;
		[SerializeField]	private bool m_AlwayShow;

		[Header("Events")]
		public UnityEventVector3 OnChange;

		protected Vector3 m_inputDirectionXZ = Vector3.zero;
		public Vector3 InputDirectionXZ { 
			get { return this.m_inputDirectionXZ; } 
			set { this.m_inputDirectionXZ = value; } 
		}

		private RectTransform m_RectTransform;
		private bool m_EnableJoytick;

		#endregion

		#region Internal class

		[Serializable]
		public class UnityEventVector3 : UnityEvent<Vector3> {}

		#endregion

		#region Implementation MonoBehaviour

		protected virtual void Awake() {
			this.m_RectTransform = this.transform as RectTransform;
			this.m_EnableJoytick = false;
		}

		protected virtual void Start() {
			this.InputDirectionXZ = Vector3.zero;
			this.SetEnableJoytick (this.m_AlwayShow);
		}

		#endregion

		#region Main methods

		public virtual void SetEnable(bool value) {
			this.gameObject.SetActive (value);
			this.SetEnableJoytick (value);
		}

		public virtual bool GetEnableJoytick() {
			return m_EnableJoytick;
		}

		public virtual void SetEnableJoytick(bool value) {
			this.m_BackgroundImage.gameObject.SetActive (value);
			this.m_KnobImage.gameObject.SetActive (value);
			this.m_EnableJoytick = !value;
		}

		protected virtual void Reset() {
			this.m_inputDirectionXZ = Vector3.zero;
			this.m_BackgroundImage.rectTransform.anchoredPosition = Vector2.zero;
			this.m_KnobImage.rectTransform.anchoredPosition = Vector2.zero;
			// Call event.
			if (this.OnChange != null) {
				this.OnChange.Invoke (m_inputDirectionXZ);
			}
		}

		#endregion

		#region Interface implementation

		public void OnBeginDrag (PointerEventData eventData)
		{
			
		}

		public void OnDrag (PointerEventData eventData)
		{
			var pos = Vector2.zero;
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle(m_BackgroundImage.rectTransform, 
				eventData.position, 
				eventData.pressEventCamera, 
				out pos)) 
			{
				pos.x = (pos.x / m_BackgroundImage.rectTransform.sizeDelta.x);	
				pos.y = (pos.y / m_BackgroundImage.rectTransform.sizeDelta.y);	

				m_inputDirectionXZ.x = pos.x * 2f; 
				m_inputDirectionXZ.y = 0f;
				m_inputDirectionXZ.z = pos.y * 2f;
				m_inputDirectionXZ = m_inputDirectionXZ.magnitude > 1f ? m_inputDirectionXZ.normalized : m_inputDirectionXZ;
				m_KnobImage.rectTransform.anchoredPosition = new Vector2 (
					m_inputDirectionXZ.x * (m_BackgroundImage.rectTransform.sizeDelta.x / 3f) , 
					m_inputDirectionXZ.z * (m_BackgroundImage.rectTransform.sizeDelta.y / 3f));

				// Call event.
				if (this.OnChange != null) {
					this.OnChange.Invoke (m_inputDirectionXZ);
				}
			}
		}

		public void OnEndDrag (PointerEventData eventData)
		{
			this.Reset ();
		}

		public void OnPointerDown (PointerEventData eventData)
		{
			if (this.m_AlwayShow == false) {
				this.SetEnableJoytick (true);
			}
			this.m_BackgroundImage.rectTransform.anchoredPosition = eventData.position - this.m_RectTransform.anchoredPosition;
			this.OnDrag (eventData);
		}

		public void OnPointerUp (PointerEventData eventData)
		{
			if (this.m_AlwayShow == false) {
				this.SetEnableJoytick (false);
			}
			this.Reset ();
		}

		#endregion

	}
}

