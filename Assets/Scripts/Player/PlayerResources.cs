using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerResources : MonoBehaviour
{
    public static PlayerResources Instance { get; private set; }

    public LubaUI lubaUI;

    public int health;
    public int maxHealth;

    public int level;               //players current level
    public int levelUp;             //how many times the player leveled up in a wave

    public int experience;          //current experience
    public int experienceRequired;  //experience required to level up to next level

    public int materials;           //amount of materials that the player has
    public int baggedMaterials;     //amount of materials that get doubled on next pickup

    public int lootBoxes;           //lootboxes that give you a random item

    [SerializeField] private bool canLifeSteal = true;

    #region Unity Events
    public UnityEvent dodgeEvent = new UnityEvent();    //if an item has an effect based off dodge subscribe here
    #endregion
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        CalcEXPRequired();
        PlayerBase.Instance.CalculateStat(Stats.HPRegen);
        StartCoroutine(HealthRegen());
    }


    #region Health

    public void DamagePlayer(float damage)
    {
        float randNum = Random.Range(0, 1f);

        //dodge chance
        if (randNum <= PlayerBase.Instance.calcPrimaryStats.dodge)
        {
            //add items effects that affect dodge
            dodgeEvent.Invoke();
            return;
        }
        else
        {
            damage = Mathf.Round(damage);
            float damageAfterArmor = 1 / (1 + PlayerBase.Instance.primaryStats.armor / 15); //maths and armor
            int damageTaken = Mathf.RoundToInt(damage * damageAfterArmor);
            GameObject uiManager = GameObject.FindWithTag("LubaUI");
            uiManager.GetComponent<LubaUI>().displayEnemyDamage(this.gameObject, damageTaken);
            health -= damageTaken;

            if (health <= 0)
            {
                Time.timeScale = 0;
                Debug.Log("Player has died.");
                Destroy(gameObject);
            }
        }
    }

    public void HealPlayer(float heal)
    {
        health += Mathf.RoundToInt(heal);
        health = Mathf.Clamp(health, -1, maxHealth);
    }

    public IEnumerator HealthRegen()
    {
        while (true)
        {
            yield return new WaitForSeconds(PlayerBase.Instance.calcPrimaryStats.HPRegen);
            if (PlayerBase.Instance.calcPrimaryStats.HPRegen > 0)
            {
                HealPlayer(1);
            }
        }
    }
    #endregion

    #region Damage

    /// <summary>
    /// Call this function when an enemy takes damage, Life Steal and Crit will be handled here.
    /// </summary>
    /// <param name="damage">amount of damage the enemy takes</param>
    /// <returns>float of the calculated damage</returns>
    public float DealDamage(float damage)
    {
        float randCrit = Random.Range(0, 1f);
        float randLifeSteal = Random.Range(0, 1f);

        if (randLifeSteal <= PlayerBase.Instance.calcPrimaryStats.lifeSteal &&
            canLifeSteal)
        {
            HealPlayer(1);
            StartCoroutine(LifeStealImmunity());
        }

        if (randCrit <= PlayerBase.Instance.calcPrimaryStats.critChance)
        {
            return damage *= 2;
        }
        else
        {
            return damage;
        }
    }

    public IEnumerator LifeStealImmunity()
    {
        canLifeSteal = false;
        yield return new WaitForSeconds(0.1f);
        canLifeSteal = true;
    }

    #endregion

    #region Level Up

    public void GainExperience(int exp)
    {
        experience += exp;
        materials += exp;

        if (experience >= experienceRequired)
        {
            //level up
            int remainingExperience = experience - experienceRequired;
            experience = remainingExperience;


            LevelUp();

        }
    }

    private void CalcEXPRequired()
    {
        experienceRequired = (level + 3) * (level + 3);
    }

    private void LevelUp()
    {
        levelUp++;
        level++;
        PlayerBase.Instance.UpdateStat(Stats.MaxHP, 1);
        lubaUI.ActivateLevelUp();
        CalcEXPRequired();
    }
    #endregion
}
