using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu_Manager : MonoBehaviour
{
    void Start()
    {
        GameObject dontDestroyOnLoad_Reference = GameObject.Find("Stored Information");
        
        if (dontDestroyOnLoad_Reference != null) Destroy(dontDestroyOnLoad_Reference);
    }

    public void ChangeLevel(string nameLevel) => SceneManager.LoadScene(nameLevel);


    public void QuitGame()
    {
        Application.Quit();
    }
}
