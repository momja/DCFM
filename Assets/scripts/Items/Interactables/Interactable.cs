using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : Item {
	public void approaching() {
		Debug.Log("Near interactable of type: ");
	}

	public virtual void Interact() {
		Debug.Log("Interacting with base interactable");
	}

	public virtual string GetInteractionType() {
		return("Interactable");
	}
}
