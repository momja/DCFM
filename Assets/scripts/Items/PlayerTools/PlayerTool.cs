using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTool : Item {
	public int energyCost;
	public int fireRate;
	public float fireSpeed;
	public float travelTime = 2f;
	public GameObject bullet;
	public Transform spawnpoint;
	public float bulletCorrection;

	// use this item in the given direction. If necessary, call GetTileInDirection()

	public virtual void UseInDirection(Vector2 direction) {
		Debug.Log("fired tool in direction: " + direction);
		//animate the trigger action here
		GameObject firedBullet = BulletSpawn();
		Rigidbody2D rb = firedBullet.GetComponent<Rigidbody2D>();
		Vector2 playerMovement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		if (playerMovement != Vector2.zero) {
			Debug.Log("Correcting for player movement");
			rb.velocity += (Vector2) Vector3.Project(playerMovement * bulletCorrection, (new Vector2(-direction.y, direction.x)));
		}
		rb.velocity += direction*fireSpeed;
	}

	// load item into view, execute a spawn animation, set player tool to current, then subtract the consumed energy
	public virtual void Spawn() {

	}

	// animate item out of view, set player tool to null
	public virtual void Unspawn() {

	}

	// get the nearest tile in the given direction
	public Sprite GetTileInDirection(Vector2 direction) {
		return null;
	}

	// consume however much energy it takes to either create the weapon or use the weapon
	public void ConsumeEnergy(int energy) {

	}

	// spawn a new bullet at the spawnpoint
	private GameObject BulletSpawn() {
		GameObject firedBullet;
		if (spawnpoint != null) {
			// spawn at spawnpoint
			firedBullet = Instantiate(bullet, spawnpoint.position, spawnpoint.rotation);
		}
		else {
			// default to center of tool
			firedBullet = Instantiate(bullet, Vector2.zero, Quaternion.identity);
		}
		Destroy(firedBullet, travelTime);
		return firedBullet;
	}
}
