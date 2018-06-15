using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firing : MonoBehaviour {

  public PlayerTool attachedTool;

  private float nextTimeToFire = 0f;

  void Update() {
    // check for any keys
    float fireHorizontal = Input.GetAxis("FireHorizontal");
    float fireVertical = Input.GetAxis("FireVertical");

    Vector2 firingDirection = new Vector2(fireHorizontal, fireVertical);

    // make sure the character is firing
    if (firingDirection != Vector2.zero && Time.time >= nextTimeToFire) {
      //face direction of fire
      var angle = Mathf.Atan2(firingDirection.y, firingDirection.x) * Mathf.Rad2Deg;
      transform.rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward);
      //fire in this direction, if enough time has passed
      nextTimeToFire = Time.time + 1f / attachedTool.fireRate;

      // fire here
      if(attachedTool != null) {
        attachedTool.UseInDirection(firingDirection);
      }
    }
  }
}
