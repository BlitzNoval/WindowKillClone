using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManaer : MonoBehaviour
{
    public void loadGame()
    {
        SceneManager.LoadScene("SampleScene");

        if(Time.timeScale != 1)
        {
            Time.timeScale = 1;
        }
    }



    public void loadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }


}
