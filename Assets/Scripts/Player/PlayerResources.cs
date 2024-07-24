using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    #region Health

    public void DamagePlayer(int damage)
    {
        float randNum = Random.Range(0, 1f);

        //dodge chance

        health -= damage;
        //cool maths stuff with armor;

        if (health < 0)
        {
            //player dies
        }
    }


    #endregion

}
