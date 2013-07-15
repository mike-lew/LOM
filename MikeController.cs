using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController))]
public class MikeController : MonoBehaviour {
	public float antiBumpFactor = .75f;
	
	private CharacterController controller;
	private Transform myTransform;
	private RaycastHit hit;
	private float speed = 1.0f;
	private float slideSpeed = 2.0f;
	private float jumpSpeed = 0.0F;
    private float gravity = 20.0F;
	private float slideLimit;
	private float rayDistance;
	private Vector3 moveDirection = Vector3.zero;
	
	public GameObject ghost_;

	// Use this for initialization
	void Start () {
		controller = gameObject.GetComponent<CharacterController>();
		myTransform = transform;
		rayDistance = controller.height * .5f + controller.radius;
		slideLimit = controller.slopeLimit - .1f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (ghost_.GetComponent<GhostScript>().isChasing_) speed = 2;
		else speed = 1;
		
		float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
		float inputModifyFactor = (inputX != 0.0f && inputY != 0.0f)? .7071f : 1.0f;
		if (controller.isGrounded) {
			bool sliding = false;
            if (Physics.Raycast(myTransform.position, -Vector3.up, out hit, rayDistance)) {
                if (Vector3.Angle(hit.normal, Vector3.up) > slideLimit)
                    sliding = true;
            }
			if (sliding) {
                Vector3 hitNormal = hit.normal;
                moveDirection = new Vector3(hitNormal.x, -hitNormal.y, hitNormal.z);
                Vector3.OrthoNormalize (ref hitNormal, ref moveDirection);
                moveDirection *= slideSpeed;
//                controller.playerControl = false;
            }
			else {
                moveDirection = new Vector3(inputX * inputModifyFactor, 0, inputY * inputModifyFactor);
                moveDirection = myTransform.TransformDirection(moveDirection) * speed;
//                playerControl = true;
            }
			if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;
		}
		else {
			moveDirection.y -= gravity * Time.deltaTime;
		}
		controller.Move(moveDirection * Time.deltaTime);
		
		
//		if (controller.isGrounded) {
//            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
//            moveDirection = transform.TransformDirection(moveDirection);
//            moveDirection *= speed;
//            if (Input.GetButton("Jump"))
//                moveDirection.y = jumpSpeed;
//            
//        }
//        
	}
}
