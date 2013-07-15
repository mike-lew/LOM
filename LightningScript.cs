using UnityEngine;
using System.Collections;

public class LightningScript : MonoBehaviour {
	public float minTimeBetweenStrikes_ = .5f;
	public float timeBetweenStrikeRolls_ = 4;
	public int strikePercentChance_ = 30;
	private float intensity_ = 0;
	private float rerollTimer_ = 1;

	// Use this for initialization
	void Start () {
		foreach(Light light in gameObject.GetComponentsInChildren<Light>()){
			light.intensity = 0;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (intensity_ < .1) {
			rerollTimer_ -= Time.deltaTime;
		}
		else {
			intensity_ = Mathf.Max(intensity_ - Time.deltaTime * 40, 0);
			foreach(Light light in gameObject.GetComponentsInChildren<Light>()){
				light.intensity = intensity_;
			}
		}
		if (rerollTimer_ <= 0) {
			rerollTimer_ = timeBetweenStrikeRolls_;
			Roll();
		}
	}
	
	private void Roll() {
		int roll = Random.Range(0, 100);
		if (roll >= strikePercentChance_) return;
		intensity_ = 10;
		rerollTimer_ = minTimeBetweenStrikes_;
	}
}
