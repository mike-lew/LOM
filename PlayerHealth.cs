using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	public float hp_ = 100;
	public GameObject damageplane_;
	public int matchboxes_ = 0;
	public bool safe_ = false;
	private Light candleLight_;
	private ParticleSystem candleParticles_;
	private AudioSource gasp_;
	
	private float lightRange_ = 5;
	private float lightIntensity_ = .5f;
	private float particleEmissions_ = 200;
	
	private float timeSinceDamageLast_ = 1;
	private float timeToCalmDown_ = 1;
	
	private bool isDead_ = false;
	private float guiTimer_ = 0;
	
	
	public Texture matchesGUI_;
	public Texture deadScreen_;
	public Texture winScreen_;
	public bool win_ = false;
	// Use this for initialization
	void Start () {
		candleLight_ = GetComponentInChildren<Light>();
		candleParticles_ = GetComponentInChildren<ParticleSystem>();
		gasp_ = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (isDead_ || win_) {
			guiTimer_ += Time.fixedDeltaTime;
			if (guiTimer_ > 3) guiTimer_ = 3;
			return;
		}
		candleLight_.range = lightRange_ * hp_ / 200 + lightRange_ / 2;
		candleLight_.intensity = lightIntensity_ * hp_ / 200 + lightIntensity_ / 2;
		candleParticles_.emissionRate = particleEmissions_ * hp_ / 100;
		
		timeSinceDamageLast_ += Time.fixedDeltaTime;
		if (timeSinceDamageLast_ <= timeToCalmDown_) {
			damageplane_.renderer.material.color = new Color(1, 0, 0, (1 - timeSinceDamageLast_) / 3);
			gasp_.volume = (1 - timeSinceDamageLast_);
		}
		if (timeSinceDamageLast_ > timeToCalmDown_) {
			if (damageplane_.renderer.material.color.a != 0)
				damageplane_.renderer.material.color = new Color(1, 0, 0, 0);
			if (gasp_.isPlaying) gasp_.Stop();
		}
		
		if (Input.GetKeyDown(KeyCode.R) && matchboxes_ > 0 && hp_ < 80) {
			matchboxes_--;
			hp_ += 30;
			damageplane_.GetComponent<AudioSource>().Play();
		}
		
		safe_ = gameObject.GetComponent<SafezoneChecker>().isSafe_;
	}
	
	void OnGUI() {
		if (win_) {
			Color textureColor = GUI.color;
			textureColor.a = guiTimer_ / 3;
			GUI.color = textureColor;
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), winScreen_);
		}
		else if (!isDead_){
			GUI.DrawTexture(new Rect(20, Screen.height - 75, 70, 50), matchesGUI_);
			GUI.TextField(new Rect(100, Screen.height - 65, 30, 20), "" + matchboxes_);
		}
		else {
			Color textureColor = GUI.color;
			textureColor.a = guiTimer_ / 3;
			GUI.color = textureColor;
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), deadScreen_);
		}
	}
	
	public void TakeDamage(float damage) {
		if (safe_ || isDead_) return;
		hp_ -= damage;
		timeSinceDamageLast_ = 0;
		damageplane_.renderer.material.color = new Color(1, 0, 0, .333f);
		if (!gasp_.isPlaying) {
			gasp_.volume = 1;
			gasp_.Play();
		}
		if (hp_ <= 0) {
			isDead_ = true;
		}
	}
	
	public void PickupMatches() { 
		matchboxes_++; 
		transform.Find("Capsule").GetComponent<AudioSource>().Play();
	}
}
