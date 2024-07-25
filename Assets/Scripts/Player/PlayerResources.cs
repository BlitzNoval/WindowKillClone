using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerResources : MonoBehaviour
{
    public static PlayerResources Instance { get; private set; }

    public int health;
    public int maxHealth;

    public int level;               //players current level
    public int levelUp;             //how many times the player leveled up in a wave

    public int experience;          //current experience
    public int experienceRequired;  //experience required to level up to next level

    public int materials;           //amount of materials that the player has
    public int baggedMaterials;     //amount of materials that get doubled on next pickup

    public int lootBoxes;           //lootboxes that give you a random item

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
            health -= damageTaken;

            if (health <= 0)
            {
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
        yield return new WaitForSeconds(PlayerBase.Instance.calcPrimaryStats.HPRegen);
        HealPlayer(1);
    }
    #endregion

    #region Damage

    #endregion

}
