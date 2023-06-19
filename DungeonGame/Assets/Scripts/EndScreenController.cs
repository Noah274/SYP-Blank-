using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenController : MonoBehaviour
{
    void Start()
    {
        Coin.totalCoins = 0; // Reset the totalCoins to 0
    }

    public void GoToStartScreen()
    {
        SceneManager.LoadScene("StartScreen"); // Replace "StartScreen" with the actual name of your start screen scene
    }
}