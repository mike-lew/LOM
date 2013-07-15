using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (Rigidbody))]
public class GhostScript : MonoBehaviour {
	public GameObject player_;
	
	public bool isChasing_ = false;
	private List<Vector3> chasePoints_;
	private List<int> patrolPoints_;
	private int patrolCounter_ = 0;
	
	private float maxSeeDistance_ = 10;

	// Use this for initialization
	void Start () {
		patrolPoints_ = new List<int>();
		patrolPoints_.Add(5);
		patrolPoints_.Add(8);
		chasePoints_ = new List<Vector3>();
		chasePoints_.Add(new Vector3(-6, 8, -2));
		chasePoints_.Add(new Vector3(-6, 8, 0));
		chasePoints_.Add(new Vector3(-6, 8, 2));
		chasePoints_.Add(new Vector3(-6.5f, 8, 3));
		chasePoints_.Add(new Vector3(-5, 8, 4.5f));
		chasePoints_.Add(new Vector3(-4, 8, 4));
		chasePoints_.Add(new Vector3(-2, 8, 4));
		chasePoints_.Add(new Vector3(0, 8, 4));
		chasePoints_.Add(new Vector3(1, 8, 4));
		chasePoints_.Add(new Vector3(2, 8, 4));
		chasePoints_.Add(new Vector3(3, 8, 4.5f));
		chasePoints_.Add(new Vector3(4.5f, 8, 3));
		chasePoints_.Add(new Vector3(4, 8, 2));
		chasePoints_.Add(new Vector3(4, 8, 0));
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 toPlayer = player_.transform.position - transform.position;
		RaycastHit[] hits = Physics.RaycastAll(transform.position, toPlayer.normalized, maxSeeDistance_);
		bool directChase = false;
		bool inSightAngle = false;
		if (Vector3.Angle(transform.forward, toPlayer) < 100) {
			inSightAngle = true;
		}
		if (inSightAngle && !player_.GetComponent<PlayerHealth>().safe_) {
			foreach (RaycastHit hit in hits) {
				if (hit.transform.root.tag == "ghost" || hit.collider.isTrigger) continue;
				if (hit.transform.root.tag != "Player") {
					Debug.DrawRay(transform.position, toPlayer);
					break;
				}
				directChase = true;
				isChasing_ = true;
			}
		}
		if (player_.GetComponent<PlayerHealth>().safe_) {
			isChasing_ = false;
		}
		if (isChasing_) {
			Vector3 target;
			if (!directChase) {
				int pCPt = 0;
				int gCPt = 0;
				float pdist = 9999;
				float gdist = 9999;
				for (int i = 0; i < chasePoints_.Count; i++) {
					float dist = Vector3.Distance(player_.transform.position, chasePoints_[i]);
					if (dist < pdist) {
						pdist = dist;
						pCPt = i;
					}
				}
				for (int i = 0; i < chasePoints_.Count; i++) {
					float dist = Vector3.Distance(transform.position, chasePoints_[i]);
					if (dist < gdist) {
						gdist = dist;
						gCPt = i;
					}
				}
				if (pCPt > gCPt) target = chasePoints_[gCPt + 1];
				else if (pCPt < gCPt) target = chasePoints_[gCPt - 1];
				else target = chasePoints_[gCPt];
			}
			else {
				target = player_.transform.position;
			}
			transform.LookAt(target);
			Vector3 tempVelTar = transform.forward;
			tempVelTar.y = 8;
			rigidbody.velocity = tempVelTar;
		}
		else {
			Vector3 toPatrolPt = chasePoints_[patrolPoints_[patrolCounter_]] - transform.position;
			RaycastHit[] bhits_L = Physics.RaycastAll(transform.position + transform.right * -.3f, 
				toPatrolPt.normalized, toPatrolPt.magnitude);
			RaycastHit[] bhits_R = Physics.RaycastAll(transform.position + transform.right * .3f, 
				toPatrolPt.normalized, toPatrolPt.magnitude);
			bool blocking = false;
			foreach (RaycastHit hit in bhits_L) {
				if (hit.transform.root.tag == "ghost" || hit.collider.isTrigger) continue;
				Debug.Log("blocking " + hit.transform.root.name);
				Debug.DrawRay(transform.position, toPatrolPt);
				blocking = true;
				break;
			}
			if (!blocking) {
				foreach (RaycastHit hit in bhits_R) {
					if (hit.transform.root.tag == "ghost" || hit.collider.isTrigger) continue;
					Debug.Log("blocking " + hit.transform.root.name);
					Debug.DrawRay(transform.position, toPatrolPt);
					blocking = true;
					break;
				}
			}
			if (blocking) {
				int gCPt = 0;
				float gdist = 9999;
				for (int i = 0; i < chasePoints_.Count; i++) {
					float dist = Vector3.Distance(transform.position, chasePoints_[i]);
					if (dist < gdist) {
						gdist = dist;
						gCPt = i;
					}
				}
				if (patrolPoints_[patrolCounter_] > gCPt)
					transform.LookAt(chasePoints_[gCPt + 1]);
				else transform.LookAt(chasePoints_[gCPt - 1]);
				rigidbody.velocity = transform.forward;
			}
			else {
				//check if near next patrol point
				if ((transform.position - chasePoints_[patrolPoints_[patrolCounter_]]).magnitude < .25) {
					//inc patrol point
					patrolCounter_++;
					if (patrolCounter_ >= patrolPoints_.Count) patrolCounter_ = 0;
				}
				transform.LookAt(chasePoints_[patrolPoints_[patrolCounter_]]);
				rigidbody.velocity = transform.forward;
			}
		}
	}
	
	
	
}
