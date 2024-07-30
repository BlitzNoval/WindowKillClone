using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class WeaponBehaviour : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private PlayerBase playerStats;
    [SerializeField] private Weapon weaponData;
    [SerializeField] private float debugRangeMultiplier;
    public static float weaponRangeMultiplier = 0.08f;
    
    [Header("Watchers")] 
    [SerializeField] private WeaponTier currentTier;

    [SerializeField] private bool canAttack;

    [Header("Targeting")] 
    [SerializeField] private float detectionRange;
    [SerializeField] private CircleCollider2D trackingArea;

    private List<Transform> enemiesInRange = new List<Transform>();

    public WeaponTier CurrentTier
    {
        get => currentTier;
        set => currentTier = value;
    }

    public delegate void SecondaryEffect();

    public delegate void ShootingEffect(Vector2 direction);

    [SerializeField] private ShootingEffect thisShootingEffect;
    [SerializeField] private SecondaryEffect thisSecondaryEffect;

    public ShootingEffect ThisShootingEffect
    {
        get => thisShootingEffect;
        set => thisShootingEffect = value;
    }
    
    public SecondaryEffect ThisSecondaryEffect
    {
        get => thisSecondaryEffect;
        set => thisSecondaryEffect = value;
    }

    public PlayerBase PlayerStats
    {
        get => playerStats;
        set => playerStats = value;
    }

    public Weapon WeaponData
    {
        get => weaponData;
        set => weaponData = value;
    }

    void Start()
    {
        trackingArea = GetComponent<CircleCollider2D>();
        playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerBase>();
        canAttack = true;
        UpdateRange();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            thisSecondaryEffect?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 shootDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            thisShootingEffect?.Invoke(shootDir);
        }
    }

    private void FixedUpdate()
    {
        //enemy tracking behaviour
        DoTrackingBehaviour();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemiesInRange.Add(other.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(other.transform);
        }
    }

    /// <summary>
    /// Tracking behaviour for the weapon to track the closest enemy in range
    /// </summary>
    private void DoTrackingBehaviour()
    {
        //There is an enemy in range
        if (enemiesInRange.Count > 0)
        {
            float closestDistance = int.MaxValue;
            Transform closestEnemy = transform;
            // Getting the closest enemy
            foreach (var enemyInstance in enemiesInRange)
            {
                float enemyDistance = Vector2.Distance(transform.position, enemyInstance.position);
                if (closestDistance > enemyDistance)
                {
                    closestDistance = enemyDistance;
                    closestEnemy = enemyInstance;
                }
            }

            if (canAttack)
            {
                canAttack = false;
                thisShootingEffect?.Invoke(closestEnemy.position - transform.position);
                StartCoroutine(DoWeaponCooldown());
            }
        }
    }

    /// <summary>
    /// Update the range at which the weapon will detect enemies, based on the calculated Range4
    /// MAKE SURE TO ADD THIS TO STAT CHANGES IN THE PLAYER
    /// </summary>
    public void UpdateRange()
    {
        //Halving the range to account for the radius rather than the diameter
        detectionRange = CalculateRange()/2;
        trackingArea.radius = detectionRange;
    }

    /// <summary>
    /// Method used to get the damage of a weapon as it attacks
    /// </summary>
    public float CalculateDamage()
    {
        float result = 0;
        //pulling damage percentile modifier from the player stats
        float damageStat = playerStats.calcPrimaryStats.damage;
        
        //pulling weapon damage from the attached scriptableObject
        float weaponDamage = weaponData.DamagePerTier[(int)currentTier];
        
        //scaling calculations
        float scaleValue = 0;
        
        //iterating through each scaling value for the current weapon tier
        foreach (var scaleInstance in weaponData.ScalingPerTier[(int)currentTier].Scalings)
        {
            switch (scaleInstance.ScalingType)
            {
                case Stats.MaxHP:
                    scaleValue += playerStats.calcPrimaryStats.maxHP * scaleInstance.ScalingAmount;
                    break;
                case Stats.HPRegen:
                    scaleValue += playerStats.calcPrimaryStats.HPRegen * scaleInstance.ScalingAmount;
                    break;
                case Stats.LifeSteal:
                    scaleValue += playerStats.calcPrimaryStats.lifeSteal * scaleInstance.ScalingAmount;
                    break;
                case Stats.Damage:
                    scaleValue += playerStats.calcPrimaryStats.damage * scaleInstance.ScalingAmount;
                    break;
                case Stats.MeleeDamage:
                    scaleValue += playerStats.calcPrimaryStats.meleeDamage * scaleInstance.ScalingAmount;
                    break;
                case Stats.RangedDamage:
                    scaleValue += playerStats.calcPrimaryStats.rangedDamage * scaleInstance.ScalingAmount;
                    break;
                case Stats.ElementalDamage:
                    scaleValue += playerStats.calcPrimaryStats.elementalDamage * scaleInstance.ScalingAmount;
                    break;
                case Stats.AttackSpeed:
                    scaleValue += playerStats.calcPrimaryStats.attackSpeed * scaleInstance.ScalingAmount;
                    break;
                case Stats.CritChance:
                    scaleValue += playerStats.calcPrimaryStats.critChance * scaleInstance.ScalingAmount;
                    break;
                case Stats.Engineering:
                    scaleValue += playerStats.calcPrimaryStats.engineering * scaleInstance.ScalingAmount;
                    break;
                case Stats.Range:
                    scaleValue += playerStats.calcPrimaryStats.range * scaleInstance.ScalingAmount;
                    break;
                case Stats.Armor:
                    scaleValue += playerStats.calcPrimaryStats.armor * scaleInstance.ScalingAmount;
                    break;
                case Stats.Dodge:
                    scaleValue += playerStats.calcPrimaryStats.dodge * scaleInstance.ScalingAmount;
                    break;
                case Stats.Speed:
                    scaleValue += playerStats.calcPrimaryStats.speed * scaleInstance.ScalingAmount;
                    break;
                case Stats.Luck:
                    scaleValue += playerStats.calcPrimaryStats.luck * scaleInstance.ScalingAmount;
                    break;
                case Stats.Harvesting:
                    scaleValue += playerStats.calcPrimaryStats.harvesting * scaleInstance.ScalingAmount;
                    break;
            }
        }

        weaponDamage += scaleValue;
        
        //multiplying weapon damage by the percentage of the damage stat
        result = weaponDamage * (1 + damageStat/100);
        return result;
    }
    
    /// <summary>
    /// Method used to get the range of a weapon as it attacks
    /// </summary>
    public float CalculateRange()
    {
        float result = 0;
        //pulling range percentile modifier from the player stats
        float rangeStat = playerStats.calcPrimaryStats.range;
        
        //pulling weapon damage from the attached scriptableObject
        float weaponDamage = weaponData.RangePerTier[(int)currentTier];
        
        //multiplying weapon range by the percentage of the range stat
        result = weaponDamage * (1 + rangeStat/100);

        if (UnityEngine.Debug.isDebugBuild)
        {
            result *= debugRangeMultiplier;
        }
        else
        {
            result *= weaponRangeMultiplier;
        }
        return result;
    }

    /// <summary>
    /// Method used to get the range of a weapon as it attacks
    /// </summary>
    public int CalculatePierce()
    {
        int pierceStat = (int) playerStats.secondaryStats.piercing;
        int weaponPierce = weaponData.PiercePerTier[(int)currentTier];
        return weaponPierce + pierceStat;
    }
    
    /// <summary>
    /// Cooldown timer method
    /// </summary>
    private IEnumerator DoWeaponCooldown()
    {
        float waitTime = 0;
        //This is a value in seconds
        float weaponSpeed = weaponData.AttackSpeedPerTier[(int)currentTier];
        //percentage modifier of base speed
        float attackSpeedStat = playerStats.calcPrimaryStats.attackSpeed;
        // There are some exceptions with changes to certain weapons etc, but because I couldn't find a sheet of which
        // weapons get what effects, they will be ignored for now
        
        //cooldown = baseTime/modifiedSpeed
        float modifiedSpeed = (100 + attackSpeedStat)/100;
        waitTime = weaponSpeed / modifiedSpeed;
        yield return new WaitForSeconds(waitTime);
        canAttack = true;
    }
    
    public void TriggerShootEffect(Vector2 dir)
    {
        thisShootingEffect?.Invoke(dir);
    }

    public void TriggerSecondaryEffect()
    {
        thisSecondaryEffect?.Invoke();
    }
}
