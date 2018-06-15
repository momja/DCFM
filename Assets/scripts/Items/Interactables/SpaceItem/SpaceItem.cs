using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceItem : Interactable {
	public override void Interact() {
		Debug.Log("Interacting with Space Item");
	}

	public override string GetInteractionType() {
		return("Space Item");
	}
}
