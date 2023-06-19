using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
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
        public void ResumeGame()
        {
            PauseMenu.Resume();
        }
    
    }
}