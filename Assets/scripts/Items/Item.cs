using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
  protected GameObject player;

  void Start() {
    player = GameObject.Find("Player");
  }
}
