using UnityEngine;

public class Chaser : MonoBehaviour
{
    public float minSpeed = 3.5f;    // Minimum speed of the Chaser
    public float maxSpeed = 4.5f;    // Maximum speed of the Chaser
    private float speed;             // Actual speed of the Chaser
    public int health = 1;           // Health of the Chaser
    public int damage = 1;           // Damage dealt to the player

    public GameObject dropObject;   // Object to be dropped on death
    [Range(0, 100)] public float dropRate = 100f; // Drop rate in percentage

    private Transform player;        // Reference to the player's transform
    private PlayerResources playerResources; // Reference to PlayerResources script
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component

    void Start()
    {
        // Assign a random speed within the specified range
        speed = Random.Range(minSpeed, maxSpeed);

        // Find the player and get the PlayerResources component
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerResources = PlayerResources.Instance;

        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (player != null)
        {
            // Move towards the player
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            // Flip the sprite based on movement direction
            if (direction.x > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (direction.x < 0)
            {
                spriteRenderer.flipX = true;
            }
        }

        // Check Chaser health
        if (health <= 0)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the Chaser collided with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Calculate damage to the player
            int playerHealthBefore = playerResources.health;
            playerResources.DamagePlayer(damage);

            // Log to the console
            Debug.Log($"Player hit by Chaser. Player health before: {playerHealthBefore}, after: {playerResources.health}");

            // Destroy the player if health is below or equal to 0
            if (playerResources.health <= 0)
            {
                Debug.Log("Player has died.");
                Destroy(collision.gameObject);
            }
        }
    }

    public void TakeDamage(int amount)
    {
        // Placeholder for taking damage logic
    }

    private void Die()
    {
        // Check if the Chaser should drop an object
        if (dropObject != null && Random.value <= dropRate / 100f)
        {
            Instantiate(dropObject, transform.position, Quaternion.identity);
        }

        // Destroy the Chaser
        Destroy(gameObject);
    }
}
