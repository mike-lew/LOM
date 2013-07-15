using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class WindowScriptVars : MonoBehaviour {
	private bool isWindowOpen_ = true;
	private bool isInWindowTrigger_ = false;
	private bool isInWindTrigger_ = false;
	public float windDamage_ = 10;
	public Transform window_;
	public GameObject windObj_;
	private AudioSource windSound_;
	
	public void SetWindowTrigger(bool trigger) { isInWindowTrigger_ = trigger; }
	public void SetWindTrigger(bool trigger) { isInWindTrigger_ = trigger; }

	// Use this for initialization
	void Start () {
		windSound_ = window_.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (isWindowOpen_ && isInWindowTrigger_ && Input.GetKeyDown(KeyCode.E)) {
			HOTween.To(window_, 1, new TweenParms().Prop("localPosition", new Vector3(0, 1, -1)));
			isWindowOpen_ = false;
			TriggerWind(false);
			Destroy(GetComponentInChildren<DamageTriggerScript>());
		}
		if (isWindowOpen_ && this.isInWindTrigger_) { //wind damaging
			float adjustedDamage = windDamage_ * Time.fixedDeltaTime;
		}
		if (!isWindowOpen_ && windSound_.isPlaying) {
			windSound_.volume -= Time.fixedDeltaTime;
			if (windSound_.volume <= 0) windSound_.Stop();
		}
	}
	
	void OnGUI() {
		if (isWindowOpen_ && isInWindowTrigger_) {
			GUI.TextField(new Rect(Screen.width/2 - 100, Screen.height/2 - 100, 200, 40), 
				"Press 'e' to shut window");
		}
	}
	
	public void TriggerWind(bool on) {
		if (on && isWindowOpen_) windObj_.SetActive(true);
		else if (!on) windObj_.SetActive(false);
	}
}
