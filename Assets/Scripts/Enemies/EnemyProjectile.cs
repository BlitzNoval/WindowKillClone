using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemyProjectile : MonoBehaviour
{
    public float speed = 10f;          // Speed of the projectile
    public int damage = 1;             // Damage dealt by the projectile
    public float despawnTime = 5f;     // Time in seconds before the projectile despawns

    private Vector3 direction;

    public void Initialize(Vector3 direction)
    {
        this.direction = direction.normalized;
        StartCoroutine(DespawnAfterTime(despawnTime)); // Start the despawn timer
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Deal damage to the player
            PlayerResources.Instance.DamagePlayer(damage);
            Debug.Log("Player hit by projectile.");

            // Check player health and destroy the player if health is below or equal to 0
            if (PlayerResources.Instance.health <= 0)
            {
                Debug.Log("Player has died.");
                Destroy(collision.gameObject);
            }

            // Destroy the projectile
            Destroy(gameObject);
        }
    }

    private IEnumerator DespawnAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
