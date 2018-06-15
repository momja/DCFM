using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handle when player presses a key
public class KeysManagerLand : MonoBehaviour {
	void Update () {
		// If space key is pressed...
		if (Input.GetKeyUp(KeyCode.Space)) {
			// open next dialogue
			DialogueManager dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager> ();
			dialogueManager.DisplayNextSentence();
		}
	}
}
