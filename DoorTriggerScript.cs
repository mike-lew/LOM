using UnityEngine;
using System.Collections;

public class DoorTriggerScript : MonoBehaviour {
	private float maxAngle_ = 90;
	private bool isInTrigger_ = false;
	private bool isHolding_ = false;
	private float lastPlayerAngle_ = 0;
	private float startPlayerAngle_ = 0;
	private float startAngleBetween_ = 0;
	private float startHingeAngle_ = 0;
	
	public Transform player_;
	public Transform hinge_;
	
	public bool isDoorClosed_;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (isHolding_) {
			if (!isInTrigger_ || ! Input.GetKey(KeyCode.E)) isHolding_ = false;
		}
		
		if (isInTrigger_ && Input.GetKeyDown(KeyCode.E)) {
			isHolding_ = true;
			float angleBetween = 0;
			Vector3 p_ray = (player_.position - hinge_.position).normalized;
			angleBetween = Mathf.Atan2(hinge_.forward.z - p_ray.z, hinge_.forward.x - p_ray.x);
			angleBetween = angleBetween * 360 / Mathf.PI;
			startAngleBetween_ = Vector3.Angle(hinge_.forward, player_.forward);
			startPlayerAngle_ = player_.localEulerAngles.y;
			startHingeAngle_ = hinge_.localEulerAngles.y;
		}
		
		if (isInTrigger_ && isHolding_) {
			float playerAngleDiff = player_.localEulerAngles.y - lastPlayerAngle_;
			if (startAngleBetween_ <= 90) hinge_.Rotate(0, playerAngleDiff * 1, 0);
			else hinge_.Rotate(0, playerAngleDiff * -1, 0);
			if (hinge_.localEulerAngles.y > 270) {
				hinge_.Rotate(0, 360 - hinge_.localEulerAngles.y, 0);
			}
			else if (hinge_.localEulerAngles.y > 180) hinge_.Rotate(0, 180 - hinge_.localEulerAngles.y, 0);
		}
		if (isInTrigger_) lastPlayerAngle_ = player_.localEulerAngles.y;
		if (hinge_.localEulerAngles.y < 100 && hinge_.localEulerAngles.y > 80) isDoorClosed_ = true;
		else isDoorClosed_ = false;
	}
	
	void OnTriggerEnter(Collider c) {
		if (c.transform.root.tag == "Player") isInTrigger_ = true;
	}
	
	void OnTriggerExit(Collider c) {
		if (c.transform.root.tag == "Player") isInTrigger_ = false;
	}
	
	void OnTriggerStay(Collider c) {
		if (c.transform.root.tag == "ghost") {
			if (hinge_.localEulerAngles.y > 100) hinge_.Rotate(0, -30 * Time.fixedDeltaTime, 0);
			else if (hinge_.localEulerAngles.y < 80) hinge_.Rotate(0, 30 * Time.fixedDeltaTime, 0);
		}
	}
	
	void OnGUI() {
		if (isInTrigger_) {
			GUI.TextField(new Rect(Screen.width/2 - 150, Screen.height/2, 300, 40), 
				"Hold 'e' and move mouse left or right to control door");
		}
	}
}
