using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LubaUI : MonoBehaviour
{

    public int healt;
    public int MexHelt;

    public TMP_Text maxHealthTxt;
    public TMP_Text crntLvl;

    public PlayerResources playerResources;
    public Slider healthSlider;
   
    void Start()
    {
        //Link Jay-Lee's Values Max Health Values to values of SLiders
        //For now I used local values for testing

        healt = MexHelt;

        healthSlider.maxValue = MexHelt;
        healthSlider.value = healt;
      /*  healthSlider.maxValue = playerResources.maxHealth;
        healthSlider.value = playerResources.health;*/
    }

   
    void Update()
    {
       
        //  healthSlider.value = playerResources.health;
        healthSlider.value = healt;

        maxHealthTxt.text = healt.ToString() + "/" +  MexHelt.ToString();

        crntLvl.text = "LV." + playerResources.level.ToString();

    }
}
