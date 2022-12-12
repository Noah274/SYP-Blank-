using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkillTree : MonoBehaviour
{
    // Start is called before the first frame update

    public void Back()
    {
        SceneManager.LoadScene("StartScreen");
    }
    public void Fire()
    {
        Debug.Log("butten prest");
    }
}