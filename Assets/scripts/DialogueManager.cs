// DCFM Team 2018

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Handles all dialogue events. Create a new DialogueManager
// for each dialogue event
public class DialogueManager : MonoBehaviour {

	private Queue<string> sentences;

	public Text nameText; // edit in component
	public Text dialogueText; // edit in component

	public Animator animator;

	private GameObject player;

	void Awake () {
		player = GameObject.Find("Player");
	}

	void Start () {
		// Initialize sentences
		sentences = new Queue<string>();
	}

	// Run when dialogue is initally triggered
	public void StartDialogue (Dialogue dialogue) {

		// Pause the character's movement until the dialogue is finished
		player.GetComponent<Walk>().enabled = false;

		animator.SetBool("IsBoxOpen", true);

		nameText.text = dialogue.name;

		// Make sure there aren't old sentences in the queue
		sentences.Clear();

		// Populate sentences Queue
		foreach (string sentence in dialogue.sentences) {
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence() {
		if (sentences.Count == 0) {
			EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();
		dialogueText.text = sentence;
	}

	void EndDialogue() {

		// Unfreeze the character after the dialogue has ended
		player.GetComponent<Walk>().enabled = true;

		animator.SetBool("IsBoxOpen", false);
	}


}
