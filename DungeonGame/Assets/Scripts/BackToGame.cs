using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToGame : MonoBehaviour
{
    // Start is called before the first frame update
    public void Melee()
    {
        Debug.Log("Why you so stupid");
    }
    public void Back()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
