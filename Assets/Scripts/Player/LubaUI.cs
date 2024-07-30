using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class LubaUI : MonoBehaviour
{
    private PlayerResources playerResources;
    private Enemy enemyS;

    public int health; // These are the PlayTest Stats however I have linked themto JayLee stats
    public int MaxHealt;

    public TMP_Text maxHealthTxt; 
    public TMP_Text crntLvl;
    public TMP_Text coins;

    public Slider healthSlider;
    public Slider levelSlider;

    #region
    public GameObject runPanel; //This is the Lose Panel
    public GameObject pauseMenu;
    #endregion


    private float lastHealth;
    private Enemy enemy;



    void Start()
    {
        playerResources = PlayerResources.Instance;
        if (playerResources != null)
        {
            Debug.Log("PLayer Instance Found");
        }
        else
        {
            Debug.LogError("PlayerResources singleton instance not found.");
        }
        enemy = GetComponent<Enemy>();

        if (enemy != null)
        {
            lastHealth = enemy.health;
        }

        // Health SLider
        healthSlider.maxValue = MaxHealt;
        healthSlider.value = health;

        //Level Slider
        levelSlider.maxValue = playerResources.experienceRequired;
        levelSlider.value = playerResources.experience;


        pauseMenu.SetActive(false);

      

    }


    void Update()
    {

        MaxHealt = playerResources.maxHealth;
        health = playerResources.health;

        healthSlider.maxValue = MaxHealt;
        healthSlider.value = health;

        levelSlider.maxValue = playerResources.experienceRequired;
        levelSlider.value = playerResources.experience;

        // Health Bar Text
        maxHealthTxt.text = health.ToString() + "/" + MaxHealt.ToString();  // For ACtual Game Health Bar Txt

        // Coins Text
        coins.text = playerResources.materials.ToString();

        //LVL Text
        crntLvl.text = "LV." + playerResources.level.ToString();

        if(health<=0)
        {
            runPanel.SetActive(true);
            //Other Lose Panel Logic
        }


        // Monitor the health for changes
        if (enemy != null && enemy.health != lastHealth)
        {
            float damageTaken = lastHealth - enemy.health;
            lastHealth = enemy.health;
            Debug.Log($"Enemy took {damageTaken} damage.");
        }

    }

    //BELOW IS THE LOGIC FOR SHOWING THE LEVEL UP UI
   
    public GameObject[] gameObjects;

    private int currentIndex = -1;

    // THIS IS THE METHOD @JAY_LEE
    public void ActivateLevelUp()
    {      
        // Increment the index
        currentIndex++;

        // If the index goes out of bounds, reset it to the first object
        if (currentIndex >= gameObjects.Length)
        {
            currentIndex = 0;
        }

        // Activate the next object
        if (currentIndex < gameObjects.Length)
        {
            gameObjects[currentIndex].SetActive(true);
        }
    }


    //Method to reset Level Up UI
    public void LevelRestart()
    {
        currentIndex = -1;
        foreach (GameObject obj in gameObjects)
        {
            obj.SetActive(false);
        }
    }

  

   
    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    


}
