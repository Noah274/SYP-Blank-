using System.Collections;
using System.Collections.Generic;
using Menu;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject fireWallPrefab;
    public Transform firePoint;
    public float fireForce = 20f;
    public float spawnDistance = 20;

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
    public void FireWall()
    {
        Vector3 firePointPos = firePoint.position;
        Vector3 firePointDirection = firePoint.forward;
        Quaternion firePointRotation = firePoint.rotation;
        Vector3 spawnPos = firePointPos + firePointDirection*spawnDistance;

        if (!pauseMenu.GameIsPaused)
        {
            Quaternion rotatedRotation = firePointRotation * Quaternion.Euler(0f, 90f, 0f);
            GameObject fireWall = Instantiate(fireWallPrefab, spawnPos, firePointRotation);
            GameObject createdObjectsContainer = GameObject.Find("createdObjects");
            fireWall.transform.SetParent(createdObjectsContainer.transform);
        }
    }
}
