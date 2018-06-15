using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : Interactable {

	public override void Interact() {
		Debug.Log("Interacting with Spaceship");
		player.SetActive(false);

		// activate rocket
		GetComponent<RocketMovement>().enabled = true;
		// Make the player a child of the rocket
		player.transform.parent = transform;
		player.transform.position = transform.position;
	}

	public override string GetInteractionType() {
		return("Spaceship");
	}
}
