using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Scenes/MainGame-Test");
    }

    public void GoBackToMenu()
    {
        SceneManager.LoadScene("StartScreen");
    }
    
    public void SwitchToOptionMenu()
    {
        SceneManager.LoadScene("OptionMenu");
    }
        
    public void OnApplicationQuit()
    {
        Application.Quit();
    }
    
}