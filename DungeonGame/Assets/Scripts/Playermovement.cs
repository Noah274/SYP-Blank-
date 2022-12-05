using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Playermovement : MonoBehaviour
{
    public float speed;
    void Update()
    {
        
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(inputX *speed, inputY *speed, 0);


        movement *= Time.deltaTime;

        transform.Translate(movement);
    }
}
