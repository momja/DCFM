using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveBoundary : MonoBehaviour {

	public string interactsWith = "PlayerInteractions";

	private Transform interactionIcon;
	private bool inRange = false;

	void Awake () {
		interactionIcon = transform.Find("InteractionCanvas/InteractionIcon");
		interactionIcon.localScale = new Vector3(0,0);
	}

	void Update () {
		if (inRange && Input.GetKeyUp(KeyCode.Q)) {
			// open interaction
			GetComponent<Interactable>().Interact();
		}
	}

	void OnTriggerEnter2D(Collider2D other) {

		if(other.tag == interactsWith) {
			inRange = true;
			interactionIcon.localScale = new Vector3(1,1);
		}
		if(other.tag == interactsWith) {
			inRange = true;
			interactionIcon.localScale = new Vector3(1,1);
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if(other.tag == "Player") {
			inRange = false;
			interactionIcon.localScale = new Vector3(0,0);
		}
	}
}
