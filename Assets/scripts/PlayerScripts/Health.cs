using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {
  public float maxHealth;
  public Image healthbar;

  private float currentHealth;

  void Awake() {
    currentHealth = maxHealth;
  }

  public void TakeDamage(float damage) {
    currentHealth -= damage;
    healthbar.fillAmount = currentHealth/maxHealth;
    Debug.Log("Player hit: " + damage + " damage.");

    if(currentHealth <= 0) {
      // die
    }
  }
}
