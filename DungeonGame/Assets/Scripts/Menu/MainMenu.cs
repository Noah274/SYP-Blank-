using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Scenes/MainGame-Test");
        Debug.Log("Button Press");
    }

    public void GoBackToMenu()
    {
        SceneManager.LoadScene("StartScreen");
        Debug.Log("Jonas Schönbaß hat deine Mom verändert");
    }
    
    public void SwitchToOptionMenu()
    {
        SceneManager.LoadScene("OptionMenu");
    }
        
    public void OnApplicationQuit()
    {
        Debug.Log("My benis is hoat");
        Application.Quit();
    }
    
}