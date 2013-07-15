using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SafezoneChecker : MonoBehaviour {
	public bool isSafe_ = false;
	public GameObject ghost_;
	private List<SafezoneScript> zones_;

	// Use this for initialization
	void Start () {
		zones_ = new List<SafezoneScript>();
		foreach(GameObject go in GameObject.FindGameObjectsWithTag("safezone")) {
			zones_.Add(go.GetComponent<SafezoneScript>());
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		bool newSafe_ = false;
		foreach(SafezoneScript sz in zones_) {
			if (sz.isProtected_) {
				newSafe_ = true;
				break;
			}
		}
		if (newSafe_ != isSafe_) {
			isSafe_ = newSafe_;
			ToggleSafetyMeasures();
		}
	}
	
	private void ToggleSafetyMeasures() {
		if (isSafe_) ghost_.GetComponent<AudioSource>().volume = .1f;
		else ghost_.GetComponent<AudioSource>().volume = .5f;
	}
}
