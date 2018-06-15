using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Item {
  public float maxHealth;
  public float damage;
  public Dictionary<string, float> weakness;

  private Image healthbar;
  private float currentHealth;

  void Awake() {
    currentHealth = maxHealth;
    healthbar = transform.Find("Canvas/HealthbarBG/Healthbar").GetComponent<Image>();
    Debug.Log(healthbar);
  }
  // execute spawning animation, initiate character
  public virtual void Spawn() {

  }

  // destroy character, and execute dying animation
  public virtual void Die() {
    //run death animation
    //drop any loot
    //destroy the game GameObject
    Destroy(gameObject);
  }

  // decrease health variable, and check if it goes below 0
  public void TakeDamage(float damage) {
    currentHealth -= damage;
    healthbar.fillAmount = currentHealth/maxHealth;
    Debug.Log(name + " hit: " + damage + " damage.");
    Debug.Log(currentHealth/maxHealth);
    if (currentHealth <= 0) {
      Die();
    }
  }

  // execute attack animation and deal damage if it collides with the player
  public virtual void Attack() {

  }

  public virtual void Movement() {
    
  }

  // if the colider collides with a trigger
  void OnTriggerEnter2D(Collider2D other) {
    // only take damage if it is an attack
    if(other.tag == "PlayerAttack") {
      damage = other.GetComponent<Attack>().damage;
      Debug.Log("Attack hit enemy: dealing " + damage + " damage.");
      TakeDamage(damage);
    }
    else if(other.tag == "Player") {
      player.GetComponent<Health>().TakeDamage(damage);
    }
  }

}
