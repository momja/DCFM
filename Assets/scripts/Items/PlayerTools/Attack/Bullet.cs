using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet: Attack {
  // if the bullet hits a collider
  void OnTriggerEnter2D(Collider2D other) {
    if(other.tag == "Enemy") {
      //deal damage here
      Destroy(gameObject);
    }
  }
}
