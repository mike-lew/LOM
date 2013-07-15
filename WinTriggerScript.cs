using UnityEngine;
using System.Collections;

public class WinTriggerScript : MonoBehaviour {
	public GameObject player_;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider c) {
		if (c.transform.root.tag == "Player") {
			player_.GetComponent<PlayerHealth>().win_ = true;
		}
	}
}
