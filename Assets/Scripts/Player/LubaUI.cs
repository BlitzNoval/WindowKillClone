using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class LubaUI : MonoBehaviour
{
    private PlayerResources playerResources;


    public int health; // These are the PlayTest Stats however I have linked themto JayLee stats
    public int MaxHealt;

    public TMP_Text maxHealthTxt; 
    public TMP_Text crntLvl;

   // public int ishu;

    //private PlayerResources playerResources;
    public Slider healthSlider;
    public Slider levelSlider;

   

    
    #region
    public GameObject runPanel; //This is the Lose Panel
    public GameObject pauseMenu;
    #endregion


    

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

      
        //LVL Text
          crntLvl.text = "LV." + playerResources.level.ToString();

        if(health<=0)
        {
            runPanel.SetActive(true);
            //Other Lose Panel Logic
        }

    }

    //BELOW IS THE LOGIC FOR SHOWING THE LEVEL UP UI
    //I used empty game objects as place Holders then when the script is called it instatiates the UI at an open GO



    public GameObject[] gameObjects;

    // Variable to keep track of the current active object index
    private int currentIndex = -1;

    // Method to activate the next game object
    public void ActivateNextObject()
    {
        // Deactivate the currently active object if there is one
        if (currentIndex >= 0 && currentIndex < gameObjects.Length)
        {
            gameObjects[currentIndex].SetActive(false);
        }

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


    public void levelUPUI ()
    {

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
