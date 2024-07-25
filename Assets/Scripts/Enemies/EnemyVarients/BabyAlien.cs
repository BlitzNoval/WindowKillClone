using UnityEngine;

public class BabyAlien : MonoBehaviour
{
    public float speed = 3.5f;       // Speed of the Baby Alien
    public int health = 1;           // Health of the Baby Alien
    public int damage = 1;           // Damage dealt to the player

    public GameObject dropObject;   // Object to be dropped on death
    [Range(0, 100)] public float dropRate = 100f; // Drop rate in percentage

    private Transform player;        // Reference to the player's transform
    private PlayerResources playerResources; // Reference to PlayerResources script
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component

    void Start()
    {
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

        // Check Baby Alien health
        if (health <= 0)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the Baby Alien collided with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Calculate damage to the player
            int playerHealthBefore = playerResources.health;
            playerResources.DamagePlayer(damage);

            // Log to the console
            Debug.Log($"Player hit by Baby Alien. Player health before: {playerHealthBefore}, after: {playerResources.health}");

            // Destroy the player if health is below or equal to 0
        }
    }

    public void TakeDamage(int amount)
    {
        // Placeholder for taking damage logic
    }

    private void Die()
    {
        // Check if the Baby Alien should drop an object
        if (dropObject != null && Random.value <= dropRate / 100f)
        {
            Instantiate(dropObject, transform.position, Quaternion.identity);
        }

        // Destroy the Baby Alien
        Destroy(gameObject);
    }
}
