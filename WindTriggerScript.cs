using UnityEngine;
using System.Collections;

public class WindTriggerScript : MonoBehaviour {
	public GameObject parent_;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider c) {
		if (c.transform.root.gameObject.tag == "Player") {
			parent_.GetComponent<WindowScriptVars>().SetWindTrigger(true);
			parent_.GetComponent<WindowScriptVars>().TriggerWind(true);
		}
	}
	
	void OnTriggerExit(Collider c) {
		if (c.transform.root.gameObject.tag == "Player") {
			parent_.GetComponent<WindowScriptVars>().SetWindTrigger(false);
			parent_.GetComponent<WindowScriptVars>().TriggerWind(false);
		}
	}
}
