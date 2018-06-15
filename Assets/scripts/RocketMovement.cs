using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RocketMovement : MonoBehaviour {

	public float speed = 0.2f;
	public float maxVelocity = 5.0f;

	public GameObject rocketCam;

	private Rigidbody2D rb;

	void Awake() {
		rb = GetComponent<Rigidbody2D>();
	}

	void OnEnable() {
		rocketCam.GetComponent<CinemachineVirtualCamera>().Priority = 11;
	}

	void FixedUpdate() {
		float moveVertical = Input.GetAxis ("Vertical");
		float moveHorizontal = Input.GetAxis ("Horizontal");

		Vector3 moveVec = new Vector2(moveHorizontal * speed, moveVertical * speed);
		rb.AddForce(moveVec);

		if(rb.velocity.magnitude > maxVelocity) {
			rb.velocity = rb.velocity/(rb.velocity.magnitude) * maxVelocity;
		}
	}

	void OnDisable() {
		rocketCam.GetComponent<CinemachineVirtualCamera>().Priority = 9;
	}

}
