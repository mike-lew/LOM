using UnityEngine;
using System.Collections;

public class DamageTriggerScript : MonoBehaviour {
	public float damage_;
	public GameObject player_;
	private bool isInTrigger_ = false;

	// Use this for initialization
	void Start () {
	
	}
	
	void FixedUpdate() {
		if (isInTrigger_) {
			player_.GetComponent<PlayerHealth>().TakeDamage(damage_ * Time.fixedDeltaTime);
		}
	}
	
	// Update is called once per frame
	void OnTriggerEnter(Collider c) {
		if (c.transform.root.tag == "Player") isInTrigger_ = true;
	}
	
	void OnTriggerExit(Collider c) {
		if (c.transform.root.tag == "Player") isInTrigger_ = false;
	}
}
