using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class HandlingFinishedLayer : MonoBehaviour
{
    private bool bossStarted = false;
    private List<GameObject> createdObjects = new List<GameObject>();
    private GeneratorOptions gOptions;

    private void Update()
    {
        if (!bossStarted)
        {
            if (GameObject.FindGameObjectWithTag("EnemyBoss") != null)
            {
                bossStarted = true;
            }
        }
        else
        {
            if (GameObject.FindGameObjectWithTag("EnemyBoss") == null)
            {
                gOptions = gameObject.GetComponent<GeneratorOptions>();
                bossStarted = false;
                DestroyCreatedObjects();
                ClearAllTilemaps();

                gOptions.layerLevel++; 
                
                StartCoroutine(WaitForTwoSeconds());
            }
        }
    }
    
    private IEnumerator WaitForTwoSeconds()
    {
        //Debug.Log("Start waiting");
        gOptions.levelText.gameObject.SetActive(true);
        gOptions.levelText.text = "Nächstes Level: " +  gOptions.layerLevel;
        yield return new WaitForSeconds(gOptions.waitingTime);
        
        //Debug.Log("Finish waiting");        
        gOptions.levelText.gameObject.SetActive(false);
        
        MakeNewLayer();
    }

    private void DestroyCreatedObjects()
    {
        // Finde das GameObject "createdObjects"
        GameObject createdObjectsContainer = GameObject.Find("createdObjects");

        if (createdObjectsContainer != null)
        {
            // Gehe durch alle Kinder des "createdObjects"-Objekts
            foreach (Transform child in createdObjectsContainer.transform)
            {
                // Zerstöre das Kindobjekt
                Destroy(child.gameObject);
            }
        }
    }

    private void MakeNewLayer()
    {
        Main main = FindObjectOfType<Main>();
        if (main != null)
        {
            main.MakeNewLayer();
        }
        else
        {
            Debug.LogError("Error: Main script not found!");
        }
    }
    
    private void ClearAllTilemaps()
    {
        Tilemap[] tilemaps = FindObjectsOfType<Tilemap>();

        foreach (Tilemap tilemap in tilemaps)
        {
            tilemap.ClearAllTiles();
        }
    }

}