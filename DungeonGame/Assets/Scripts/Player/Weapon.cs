using System.Collections;
using System.Collections.Generic;
using Menu;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireForce = 20f;

    public void Fire()
    {
        if (!pauseMenu.GameIsPaused)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            GameObject createdObjectsContainer = GameObject.Find("createdObjects");
            bullet.transform.SetParent(createdObjectsContainer.transform);
            
            bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
        }
        
    }
}
