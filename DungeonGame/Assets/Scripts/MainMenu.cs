using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
        Debug.Log("Button Press");
    }
    public void SkillTree()
    {
        SceneManager.LoadScene("SkillTree");
        Debug.Log("Button Press");
    }
}