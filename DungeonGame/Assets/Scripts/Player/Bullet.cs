using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
    void OnTriggerEnter2D(Collider2D c2d)
    {
        if (c2d.CompareTag("Enemy") || c2d.CompareTag("Wall")) {
            Destroy(gameObject);
        }
    }
}
