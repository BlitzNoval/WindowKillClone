using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class LubaUI : MonoBehaviour
{
    // Just Need To Add playerResources script and assign it in inspector
    public int healt;
    public int MexHelt;

    public TMP_Text maxHealthTxt;
    public TMP_Text crntLvl;

    public int ishu;

    //private PlayerResources playerResources;
    public Slider healthSlider;

    private PlayerResources playerResources;

    void Start()
    {
        playerResources = PlayerResources.Instance;
        if (playerResources != null)
        {
            Debug.Log("It is not Null");
        }
        else
        {
            Debug.LogError("PlayerResources singleton instance not found.");
        }


        //Link Jay-Lee's Values Max Health Values to values of SLiders
        //For now I used local values for testing



        healt = MexHelt;



        healthSlider.maxValue = MexHelt;
        healthSlider.value = healt;
        /*  healthSlider.maxValue = playerResources.maxHealth;
          healthSlider.value = playerResources.health;*/

      //  Debug.Log(PlayerResources.Instance.maxHealth);
    }


    void Update()
    {
        //   playerResources = GetComponent<PlayerResources>();

        healthSlider.value = healt;

        maxHealthTxt.text = healt.ToString() + "/" + MexHelt.ToString();

      // ishu = PlayerResources.Instance.maxHealth;

       // Debug.Log(ishu);


        //   crntLvl.text = "LV." + playerResources.level.ToString();

    }


}
