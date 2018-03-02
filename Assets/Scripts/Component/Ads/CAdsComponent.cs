using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Advertisements;

public class CAdsComponent : CComponent {

	#region Internal Class

	[Serializable]
	public class UnityEventFloat: UnityEvent<float> {}

	#endregion

	#region Fields

	public enum EPlacementID : byte {
		video 			= 0,
		rewardedVideo 	= 1
	}

	[Header("Configs")]
	[SerializeField]	protected string m_GameId = "xxxxxxx";
	public string gameId {
		get { return this.m_GameId; }
		set { this.m_GameId = value; }
	}
	[SerializeField]	protected EPlacementID m_PlacementId = EPlacementID.rewardedVideo;
	public EPlacementID placementId {
		get { return this.m_PlacementId; }
		set { this.m_PlacementId = value; }
	}
	[SerializeField]	protected bool m_AutoInit = true;
	public bool autoInit {
		get { return this.m_AutoInit; }
		set { this.m_AutoInit = value; }
	}
	[SerializeField]	protected bool m_AutoShow = false;
	public bool autoShow {
		get { return this.m_AutoShow; }
		set { this.m_AutoShow = value; }
	}
	[SerializeField]	protected float m_Delay = 60f;
	public float delay {
		get { return this.m_Delay; }
		set { this.m_Delay = value; }
	}
	protected float m_DelayInterval = 0f;

	[Header("Events")]
	public UnityEventFloat OnDelay;
	public UnityEvent OnShowAds;
	public UnityEvent OnFinishAds;
	public UnityEvent OnSkipAds;
	public UnityEvent OnFailAds;

	#endregion

	#region Implementation Component

	protected override void Start ()
	{
		base.Start ();
		if (this.m_AutoInit) {
			this.InitAds ();
		}
		if (this.m_AutoShow) {
			this.ShowAds ();
		}
	}

	protected override void LateUpdate ()
	{
		base.LateUpdate ();
		if (this.m_DelayInterval >= 0f) {
			if (this.OnDelay != null) {
				this.OnDelay.Invoke (1f - (this.m_DelayInterval / this.m_Delay));
			}
			this.m_DelayInterval -= Time.deltaTime;
		}
	}

	#endregion

	#region Main methods

	public virtual void InitAds() {
		if (Advertisement.isInitialized == false) {
			if (Advertisement.isSupported) {
				Advertisement.Initialize (this.m_GameId, true);
			}
		}
	}

	public virtual void ShowAds ()
	{
		this.InitAds ();
		if (this.CanShowAd () == false) {
			if (this.OnFailAds != null) {
				this.OnFailAds.Invoke ();
			}
			return;
		}
		ShowOptions options = new ShowOptions();
		options.resultCallback = HandleShowResult;
		Advertisement.Show(placementId.ToString(), options);
		if (this.OnShowAds != null) {
			this.OnShowAds.Invoke ();
		}
		this.m_DelayInterval = this.m_Delay;
	}

	protected virtual void HandleShowResult (ShowResult result)
	{
		if (result == ShowResult.Finished) {
			if (this.OnFinishAds != null) {
				this.OnFinishAds.Invoke ();
			}
		} else if(result == ShowResult.Skipped) {
			if (this.OnSkipAds != null) {
				this.OnSkipAds.Invoke ();
			}
		} else if(result == ShowResult.Failed) {
			if (this.OnFailAds != null) {
				this.OnFailAds.Invoke ();
			}
		}
	}

	public virtual bool CanShowAd() {
		return Advertisement.IsReady () && this.m_DelayInterval <= 0f;
	}

	#endregion

}
