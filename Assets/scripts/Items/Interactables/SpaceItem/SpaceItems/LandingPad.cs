using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingPad : SpaceItem {
	public override void Interact() {
		Debug.Log("Interacting with Landing Pad");
		GameObject spaceship = GameObject.Find("RocketShip");
		spaceship.transform.position = transform.position;
		player.SetActive(true);
		player.transform.position = spaceship.transform.position;
		// deactivate spaceship
		spaceship.GetComponent<RocketMovement>().enabled = false;
		spaceship.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
	}

	public override string GetInteractionType() {
		return("Landing Pad");
	}
}
