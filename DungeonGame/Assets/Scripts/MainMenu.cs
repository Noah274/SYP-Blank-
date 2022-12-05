using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
<<<<<<< HEAD
        Debug.Log("Button Press");
    }
    public void SkillTree()
    {
        SceneManager.LoadScene("SkillTree");
        Debug.Log("Button Press");
=======
        Debug.Log("Press of Button");
>>>>>>> c6c8653c50c59471c9addc154980292ed1e00f1b
    }
}
