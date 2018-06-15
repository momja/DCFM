using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour {

	public float speed = 0.2f;

	void Awake() {
	}

	void FixedUpdate() {
		float moveVertical = Input.GetAxis ("Vertical");
		float moveHorizontal = Input.GetAxis ("Horizontal");

		Vector3 moveVec = new Vector2(moveHorizontal * speed, moveVertical * speed);

		transform.position = transform.position + moveVec;
	}
}
