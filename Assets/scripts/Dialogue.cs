// DCFM Team 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Dialogue is populated with the name of the NPC and what they say
// NOTE: we could modify this to play a role in the puzzle org chart (e.g. talk to NPC, unlock this)
[System.Serializable]
public class Dialogue {

	public string name;

	[TextArea(3, 10)] // defines box size in component editor
	public string[] sentences;
}
