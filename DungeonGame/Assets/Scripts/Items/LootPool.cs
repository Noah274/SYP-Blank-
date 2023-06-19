using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootPool : MonoBehaviour
{
    public List<GameObject> itemsToDrop;
    private int minDropCount = 1;
    public int maxDropCount = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            int dropCount = Random.Range(minDropCount, maxDropCount + 1);

            for (int i = 0; i < dropCount; i++)
            {
                int randomIndex = Random.Range(0, itemsToDrop.Count);
                GameObject itemToDrop = itemsToDrop[randomIndex];

                GameObject obj = Instantiate(itemToDrop, transform.position, Quaternion.identity);
                GameObject createdObjectsContainer = GameObject.Find("createdObjects");
                obj.transform.SetParent(createdObjectsContainer.transform);
            }

            gameObject.SetActive(false);
        }
    }
}