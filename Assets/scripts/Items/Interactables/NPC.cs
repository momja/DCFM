using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable {
	public override void Interact() {
		Debug.Log("Interacting with NPC");
		DialogueTrigger trigger = GetComponent<DialogueTrigger>();
		trigger.TriggerDialogue();
	}
	public override string GetInteractionType() {
		return("NPC");
	}
}
