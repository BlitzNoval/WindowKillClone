using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour
{
    [System.Serializable]
    public class EnemyData
    {
        public EnemyClass size;
        public float maxHealth;
        public float speed;
        public float damage;
        public EnemyBehavior behavior;
    }

    public EnemyData enemyData;
    private float health;
    private Transform player;
    private bool isCharging;
    private float chargeDelay = 2f; // Seconds to wait before charging
    private float chargeRange = 5f; // Range at which to start charging

    private void Start()
    {
        if (enemyData == null)
        {
            Debug.LogError("EnemyData not assigned!");
            return;
        }

        health = enemyData.maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        isCharging = false;
    }

    private void Update()
    {
        if (player == null) return;

        switch (enemyData.behavior)
        {
            case EnemyBehavior.Normal:
                MoveTowardsPlayer();
                break;

            case EnemyBehavior.Charge:
                ChargeBehavior();
                break;

            case EnemyBehavior.NormalAndCharge:
                NormalAndChargeBehavior();
                break;
        }
    }

    private void MoveTowardsPlayer()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * enemyData.speed * Time.deltaTime;
        }
    }

    private void ChargeBehavior()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= chargeRange && !isCharging)
        {
            StartCoroutine(ChargeRoutine());
        }
        else
        {
            MoveTowardsPlayer();
        }
    }

    private void NormalAndChargeBehavior()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= chargeRange && !isCharging)
        {
            StartCoroutine(ChargeRoutine());
        }
        else
        {
            MoveTowardsPlayer();
        }
    }

    private IEnumerator ChargeRoutine()
    {
        isCharging = true;
        MoveTowardsPlayer();
        yield return new WaitForSeconds(chargeDelay);
        Vector3 direction = (player.position - transform.position).normalized;
        float chargeSpeed = enemyData.speed * 2; // Faster speed during charge
        float chargeDuration = 1f; // Duration of the charge
        float elapsed = 0f;

        while (elapsed < chargeDuration)
        {
            transform.position += direction * chargeSpeed * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }

        isCharging = false;
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
