using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
public class PauseMenu :MonoBehaviour
{
    [SerializeField] public GameObject PauseObj;
    
    public static GameObject PauseMenuUI;
    
    public static bool GameIsPaused = false;
    
    private void Start()
    {
        GameIsPaused = false;
        PauseMenuUI = PauseObj;
        PauseMenuUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (!GameIsPaused)
            {
                Pause();
            }
        }
    }

    public static void Resume()
        {
            PauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
        }

        private void Pause()
        {
            PauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        }
    }
}