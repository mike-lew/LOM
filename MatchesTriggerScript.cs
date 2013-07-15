using UnityEngine;
using System.Collections;

public class MatchesTriggerScript : MonoBehaviour {
	private bool isInTrigger_;
	public GameObject player_;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(isInTrigger_ && Input.GetKeyDown(KeyCode.E)) {
			player_.GetComponent<PlayerHealth>().PickupMatches();
			DestroyImmediate(transform.root.gameObject);
		}
	}
	
	void OnGUI() {
		if (isInTrigger_) {
			GUI.TextField(new Rect(Screen.width/2 - 100, Screen.height/2 - 50, 200, 40), 
				"Press 'e' to pickup matches");
		}
	}
	
	void OnTriggerEnter(Collider c) {
		if (c.transform.root.tag == "Player") isInTrigger_ = true;
	}
	
	void OnTriggerExit(Collider c) {
		if (c.transform.root.tag == "Player") isInTrigger_ = false;
	}
}
