using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionItem : Interactable {
	public override void Interact() {
		Debug.Log("Interacting with Action Item");
	}

	public override string GetInteractionType() {
		return("Action Item");
	}
}
