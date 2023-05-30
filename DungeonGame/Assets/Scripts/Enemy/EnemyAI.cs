
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float distanceBetween;
    public float hitPoints;
    public List<GameObject> spawnPool;

    
    private float distance;
    private Vector3 position;

    private void Start()
    {
        position = transform.position;
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.forward);
        Vector2 direction = player.transform.position - position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y,direction.x)* Mathf.Rad2Deg;
        
        
        if(distance < distanceBetween){
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
        
    }
    
    public void SpawnObjects()
    {
        Vector2 pos;
        
        int randomItem = Random.Range(0, spawnPool.Count);
        GameObject toSpawn = spawnPool[randomItem];
        
        pos = new Vector2(position.x, position.y);
        
        Instantiate(toSpawn, pos, toSpawn.transform.rotation);
    }
    
    void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D c2d)
    {
        
        if (c2d.CompareTag("Bullet"))
        {
            hitPoints -= 10;
            if (hitPoints <= 0) {
                SpawnObjects();
                Destroy(gameObject);
            }
        }
    }
    
}
