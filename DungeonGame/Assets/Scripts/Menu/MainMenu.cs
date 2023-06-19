using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class MainMenu : MonoBehaviour
    {
<<<<<<< HEAD
        public void PlayGame()
        {
            SceneManager.LoadScene("Scenes/MainGame-Test");
        }
=======
        SceneManager.LoadScene("MainGame - 1.0");
    }
>>>>>>> 0ceb7b482cf24352ad44d456eb2bcfbc75cfe388

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