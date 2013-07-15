using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SafezoneScript : MonoBehaviour {
	public GameObject player_;
	private bool isInTrigger_ = false;
	private bool doorsClosed_ = true;
	public GameObject DoorTrigger1;
	public GameObject DoorTrigger2;
	public GameObject DoorTrigger3;
	public bool isProtected_ = false;
	private List<DoorTriggerScript> ds_;

	// Use this for initialization
	void Start () {
		ds_ = new List<DoorTriggerScript>();
		if (DoorTrigger1!= null) ds_.Add(DoorTrigger1.GetComponent<DoorTriggerScript>());
		if (DoorTrigger2!= null) ds_.Add(DoorTrigger2.GetComponent<DoorTriggerScript>());
		if (DoorTrigger3!= null) ds_.Add(DoorTrigger3.GetComponent<DoorTriggerScript>());
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		bool closed = true;
		foreach (DoorTriggerScript dts in ds_) {
			if (!dts.isDoorClosed_) {
				closed = false;
				break;
			}
		}
		doorsClosed_ = closed;
		if (isInTrigger_ && doorsClosed_) isProtected_ = true;
		else isProtected_ = false;
	}
	
	void OnTriggerEnter(Collider c) {
		Debug.Log(c.gameObject.name);
		if (c.transform.root.tag == "Player") {
			isInTrigger_ = true;
		}
	}
	
	void OnTriggerExit(Collider c) {
		if (c.transform.root.tag == "Player") {
			isInTrigger_ = false;
		}
	}
}
