using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleSingleton;

public class CMapManager : CMonoSingleton<CMapManager> {

	#region Fields

	[Header("Places")]
	[SerializeField]	protected float m_PlaceDistance = 50f;
	[SerializeField]	protected string m_PlaceNamePattern = "x{0}+y0+z{1}";
	protected Vector2[] m_PlacePatterns = new Vector2[] {
		new Vector2 (-1f, 1f), new Vector2 (0f, 1f), new Vector2 (1f, 1f), 
		new Vector2 (-1f, 0f), new Vector2 (0f, 0f), new Vector2 (1f, 0f), 
		new Vector2 (-1f, -1f), new Vector2 (0f, -1f), new Vector2 (1f, -1f), 
	};
	[SerializeField]	protected List<Transform> m_UsedPlaces;
	[SerializeField]	protected List<Transform> m_ReusePlaces;
	protected Dictionary<string, CTileMap> m_MapInstance;

	[Header("Target")]
	[SerializeField]	protected Transform m_Target;
	[SerializeField]	protected Vector3 m_CurrentPosition;
	protected Vector3 m_PreviousPosition = new Vector3(-999f, 0f, -999f);
	protected bool m_NeedUpdate = false;

	#endregion

	#region Internal class

	public class CTileMap
	{
		public string tileName;
		public Transform tileTransform;
		public Vector3 tileRotation;
	}

	#endregion

	#region Implementation MonoBehaviour

	protected override void Awake() {
		base.Awake ();
		this.m_ReusePlaces = new List<Transform> ();
		this.m_MapInstance = new Dictionary<string, CTileMap> ();
	}

	protected void Start() {
		this.InitMap();
	}

	protected void Update() {
		this.CheckCurrentPosition ();
		this.CheckPlacePatterns ();
	}

	protected void LateUpdate() {
		if (this.m_NeedUpdate) {
			this.UpdateReusePlaces ();
			this.UpdatePlaces ();
		} 
	}

	#endregion

	#region Main methods

	protected void InitMap() {
		var childCount = this.transform.childCount;
		for (int i = 0; i < childCount; i++) {
			var child = this.transform.GetChild (i);
			var updatePos = this.m_PlacePatterns [i % 9];
			child.gameObject.SetActive (false);
			this.UpdatePlanetPosition (child, updatePos); 
			this.m_ReusePlaces.Add (child);
		}
		this.m_NeedUpdate = true;
	}

	protected void CheckCurrentPosition() {
		if (this.m_Target == null)
			return;
		var position = this.m_Target.position;
		this.m_CurrentPosition.x = Mathf.RoundToInt (position.x / this.m_PlaceDistance);
		this.m_CurrentPosition.y = 0f;
		this.m_CurrentPosition.z = Mathf.RoundToInt (position.z / this.m_PlaceDistance);
		if (this.m_CurrentPosition.x != this.m_PreviousPosition.x
		    || this.m_CurrentPosition.z != this.m_PreviousPosition.z) {
			this.m_NeedUpdate = true;
			this.m_PreviousPosition.x = this.m_CurrentPosition.x;
			this.m_PreviousPosition.y = 0f;
			this.m_PreviousPosition.z = this.m_CurrentPosition.z;
		}
	}

	protected void CheckPlacePatterns() {
		var currentPos 	= this.m_CurrentPosition;
		// topLeft
		this.m_PlacePatterns[0].x = currentPos.x - 1f;
		this.m_PlacePatterns[0].y = currentPos.z + 1f;
		// topUp
		this.m_PlacePatterns[1].x = currentPos.x;
		this.m_PlacePatterns[1].y = currentPos.z + 1f; 
		// topRight
		this.m_PlacePatterns[2].x = currentPos.x + 1f;
		this.m_PlacePatterns[2].y = currentPos.z + 1f;
		// left
		this.m_PlacePatterns[3].x = currentPos.x - 1f;
		this.m_PlacePatterns[3].y = currentPos.z;
		// center
		this.m_PlacePatterns[4].x = currentPos.x;
		this.m_PlacePatterns[4].y = currentPos.z; 
		// right
		this.m_PlacePatterns[5].x = currentPos.x + 1f; 
		this.m_PlacePatterns[5].y = currentPos.z;
		// bottomLeft
		this.m_PlacePatterns[6].x = currentPos.x - 1f;
		this.m_PlacePatterns[6].y = currentPos.z - 1f; 
		// bottomDown
		this.m_PlacePatterns[7].x = currentPos.x;
		this.m_PlacePatterns[7].y = currentPos.z - 1f; 
		// bottomRight
		this.m_PlacePatterns[8].x = currentPos.x + 1f;
		this.m_PlacePatterns[8].y = currentPos.z - 1f;
	}

	protected void UpdateReusePlaces() {
		for (int x = 0; x < this.m_UsedPlaces.Count; x++) {
			var checkName = this.m_UsedPlaces [x].name;
			var isGoodPlace = false;
			for (int i = 0; i < this.m_PlacePatterns.Length; i++) {
				var planetPos = this.m_PlacePatterns[i];
				var planetName = string.Format (this.m_PlaceNamePattern, planetPos.x, planetPos.y);
				if (planetName == checkName) {
					isGoodPlace = true;
					break;
				}
			}
			if (isGoodPlace == false) {
				var usedObject = this.m_UsedPlaces [x];
				if (this.m_ReusePlaces.Contains (usedObject) == false) {
					if (this.AddReuseObject (usedObject)) {
						x--;
					}
				}
			}
		}
	}

	protected void UpdatePlaces() {
		for (int i = 0; i < this.m_PlacePatterns.Length; i++) {
			if (this.m_ReusePlaces.Count == 0)
				return;
			var planetPos = this.m_PlacePatterns[i];
			var placeName = string.Format (this.m_PlaceNamePattern, planetPos.x, planetPos.y);
			var isGoodPlace = false;
			for (int x = 0; x < this.m_UsedPlaces.Count; x++) {
				var checkName = this.m_UsedPlaces [x].name;
				isGoodPlace |= placeName == checkName;
			}
			if (isGoodPlace == false) {
				var reuseObject = this.LoadTileMapInstance (placeName);
				if (this.AddUsedObject (reuseObject)) {
					this.UpdatePlanetPosition (reuseObject, planetPos);
				}
			}
		}
		this.m_NeedUpdate = this.m_UsedPlaces.Count != this.m_PlacePatterns.Length;
	}

	protected Transform LoadTileMapInstance(string name) {
		if (this.m_MapInstance.ContainsKey (name)) {
			var tile = this.m_MapInstance [name];
			if (this.m_ReusePlaces.Contains (tile.tileTransform) == false) {
				var reLoadTile = ReloadTile (tile.tileTransform);
				tile.tileTransform = reLoadTile;
			}
			return tile.tileTransform;
		} else {
			var randomIndex = Random.Range (0, this.m_ReusePlaces.Count);
			var selectedPlace = this.m_ReusePlaces [randomIndex];
			this.m_MapInstance.Add (name, new CTileMap() {
				tileName = name,
				tileTransform = selectedPlace,
				tileRotation = Vector3.up
			});
			return selectedPlace;
		} 
	}

	protected Transform ReloadTile(Transform origin) {
		var needRenew = true;
		var reLoadTile = origin;
		for (int i = 0; i < this.m_ReusePlaces.Count; i++) {
			if (this.m_ReusePlaces [i].tag == origin.tag) {
				reLoadTile = this.m_ReusePlaces [i];
				needRenew = false;
				break;
			}
		}
		if (needRenew) {
			reLoadTile = Instantiate (origin);
			reLoadTile.transform.SetParent (this.transform);
			this.m_ReusePlaces.Add (reLoadTile);
		}
		return reLoadTile;
	}

	protected bool AddReuseObject(Transform value) {
		value.gameObject.SetActive (false);
		if (this.m_ReusePlaces.Contains (value) == false
			&& this.m_UsedPlaces.Contains (value) == true) {
			this.m_ReusePlaces.Add (value);
			this.m_UsedPlaces.Remove (value);
			this.m_UsedPlaces.TrimExcess ();
			return true;
		}
		return false;
	}

	protected bool AddUsedObject(Transform value) {
		value.gameObject.SetActive (true);
		if (this.m_UsedPlaces.Contains (value) == false
			&& this.m_ReusePlaces.Contains (value) == true) {
			this.m_UsedPlaces.Add (value);
			this.m_ReusePlaces.Remove (value);
			this.m_ReusePlaces.TrimExcess ();
			return true;
		}
		return false;
	}

	protected void UpdatePlanetPosition (Transform planet, Vector2 pos) {
		planet.name = string.Format (this.m_PlaceNamePattern, pos.x, pos.y);
		planet.position = new Vector3 (pos.x * this.m_PlaceDistance, 0f, pos.y * this.m_PlaceDistance);
	}

	#endregion

}
