using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class LubaUI : MonoBehaviour
{
    public int health;
    public int MaxHealt;

    public TMP_Text maxHealthTxt;
    public TMP_Text crntLvl;

    public int ishu;

    //private PlayerResources playerResources;
    public Slider healthSlider;

    private PlayerResources playerResources;

    //Lose Panel
    public GameObject runPanel;

    //
    public GameObject pauseMenu;


    // 

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


        healthSlider.maxValue = MaxHealt;
        healthSlider.value = health;

        pauseMenu.SetActive(false);

    }


    void Update()
    {

        MaxHealt = playerResources.maxHealth;
        health = playerResources.health;

        healthSlider.maxValue = MaxHealt;
        healthSlider.value = health;

        // Health Bar Text

        maxHealthTxt.text = health.ToString(); //+ "/" + MexHelt.ToString();  // For ACtual Game Health Bar Txt

      
        //LVL Text
          crntLvl.text = "LV." + playerResources.level.ToString();

        if(health<=0)
        {
            runPanel.SetActive(true);
            //Other Lose Panel Logic
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
