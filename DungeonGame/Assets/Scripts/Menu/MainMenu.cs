using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("MainGame - 1.0");
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