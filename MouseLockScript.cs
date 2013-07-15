using UnityEngine;
using System.Collections;

public class MouseLockScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	void OnMouseDown() {
		Debug.Log("mouse lock");
        Screen.lockCursor = true;
    }
}
