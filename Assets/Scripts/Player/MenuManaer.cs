using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManaer : MonoBehaviour
{
    public void loadGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}
