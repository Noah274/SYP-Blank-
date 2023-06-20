using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGhost : MonoBehaviour
{
    public GameObject player;

    void Update()
    {
        transform.position = player.transform.position;        
    }
}
