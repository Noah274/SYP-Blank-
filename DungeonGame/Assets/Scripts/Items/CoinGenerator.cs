using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator:MonoBehaviour
{
    public int numberToSpawn = 1;
    public List<GameObject> spawnPool;
    public GameObject quad;


    void Start()
    {
        SpawnObjects();
    }

     public void SpawnObjects()
    {
        destroyObject();
        int randomItem;
        GameObject toSpawn;
        MeshCollider c = quad.GetComponent<MeshCollider>();

        float screenX, screenY;
        Vector2 pos;
        
        for (int i = 0; i < numberToSpawn; i++)
        {
            Debug.Log("Spawned "+ (i+1) );
            randomItem = Random.Range(0, spawnPool.Count);
            toSpawn = spawnPool[randomItem];
            
            screenX = Random.Range(c.bounds.min.x, c.bounds.max.x);
            screenY = Random.Range(c.bounds.min.y, c.bounds.max.y);
            pos = new Vector2(screenX, screenY);
            
            Instantiate(toSpawn, pos, toSpawn.transform.rotation);
        }
    }

     private void destroyObject()
     {
         foreach (GameObject o in GameObject.FindGameObjectsWithTag("spawnable"))
         {
             Destroy(o);
         }
     }
}