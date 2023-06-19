using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenController : MonoBehaviour
{
    public void GoToStartScreen()
    {
        SceneManager.LoadScene("StartScreen"); // Replace "StartScreen" with the actual name of your start screen scene
    }
}