using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionsHandler : MonoBehaviour {

	private string type;

	private string objectName;

	public GameObject player;

	private bool state;

	public void Awake() {
		state = false;
	}

	public void setType(string type) {
		this.type = type;
	}
	public string getType() {
		return type;
	}
	public void setObjectName(string objectName) {
		this.objectName = objectName;
	}
	public string getObjectName() {
		return objectName;
	}
	public bool getState() {
		return state;
	}
	public void setState(bool state) {
		this.state = state;
	}
}
