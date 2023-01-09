
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    public Button fire_ball;

    // Start is called before the first frame update
    void start()
    {
        fire_ball = GetComponent<Button>(Fire_Ball);
    }

    public void Back()
    {
        SceneManager.LoadScene("StartScreen");
    }
    public void Fire()
    {
        Debug.Log("butten prest");
        fire_ball.interactable = false;
    }
}