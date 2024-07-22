using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
  public float maxHealth; 
  public float health;

  private void Start ()
  {
    health = maxHealth;
  }

  public void TakeDamage(float damageAmount)
  {
    health -= damageAmount;

    if (health <= 0)
    {
        Destroy (gameObject);
    }
  }
}
