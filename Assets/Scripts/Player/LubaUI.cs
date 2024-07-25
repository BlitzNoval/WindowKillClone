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

   

    
    #region
    public GameObject runPanel; //This is the Lose Panel
    public GameObject pauseMenu;
    #endregion

// Level Up UI 
    public GameObject[] upgradeUISlots;
    public GameObject levelUpSprite;

    

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

        //LevelUp UI SLots
        upgradeUISlots = new GameObject[5];
        for (int i = 0; i < upgradeUISlots.Length; i++)
        {
            upgradeUISlots[i] = GameObject.Find("UpgradeSlot" + i);
        }

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

    //BELOW IS THE LOGIC FOR SHOWING THE LEVEL UP UI
    //I used empty game objects as place Holders then when the script is called it instatiates the UI at an open GO

    public GameObject spriteToClone;
    public GameObject[] slots;

    public void CloneSpriteAtRandomSlot()
    {
        if (slots.Length == 0)
        {
            Debug.LogWarning("No available slots to clone the sprite.");
            return;
        }

    
        int randomIndex = Random.Range(0, slots.Length);
        Vector3 clonePosition = slots[randomIndex].transform.position;
        Instantiate(spriteToClone, clonePosition, Quaternion.identity);
        RemoveSlotAtIndex(randomIndex);
    }
    private void RemoveSlotAtIndex(int index)
    {
        if (index < 0 || index >= slots.Length)
        {
            Debug.LogError("Index out of range.");
            return;
        }
        GameObject[] newSlots = new GameObject[slots.Length - 1];
        for (int i = 0, j = 0; i < slots.Length; i++)
        {
            if (i != index)
            {
                newSlots[j++] = slots[i];
            }
        }
        slots = newSlots;
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
