using UnityEngine;

public abstract class Enemy : MonoBehaviour
{

    public float lastHealth;
    public float health;
    public float speed;
    public float damage;
    public GameObject dropObject;
    [Range(0, 100)] public float dropRate = 100f;

    protected Transform player;
    protected PlayerResources playerResources;
    protected SpriteRenderer spriteRenderer;

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerResources = PlayerResources.Instance;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        /*if (health <= 0)
        {
            Die();
        }*/
    }

    public void TakeDamage(float amount) //MAKE A VIRTUAL CLASS IF IT NEEDS TO BE OVERRIDDEN
    {
        GameObject uiManager = GameObject.FindWithTag("LubaUI");
        uiManager.GetComponent<LubaUI>().displayEnemyDamage(this.gameObject, amount);
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    public void ApplyStatMultiplier(float multiplier)
    {
        health *= multiplier;
        speed *= multiplier;
        damage *= multiplier;
    }

    protected virtual void Die()
    {
        if (dropObject != null && Random.value <= dropRate / 100f)
        {
            Instantiate(dropObject, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    protected void FlipSprite(Vector3 direction)
    {
        if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    protected void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        FlipSprite(direction);
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            int playerHealthBefore = playerResources.health;

            playerResources.DamagePlayer(Mathf.RoundToInt(damage)); // Convert float damage to int
            
            Debug.Log($"Player hit by {gameObject.name}. Player health before: {playerHealthBefore}, after: {playerResources.health}");

            /*if (playerResources.health <= 0)
            {
                Debug.Log("Player has died.");
                Destroy(collision.gameObject);
            }*/
        }
    }
}