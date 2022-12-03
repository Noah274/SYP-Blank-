using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuffle_jonasS : MonoBehaviour
{
    private System.Random _random = new System.Random();
    
    public void Shuffle()
    {
        int r = _random.Next(1,5);
        int[] rooms = new int[r];
        Debug.Log($"r = {r}");
        for (int i = 0; i < r; i++)
        {
            int next = 0;                
            while (true)
            {
                next = _random.Next(1,5);
                if (!Contains(rooms, next)){
                    break;
                }                  
            }

            rooms[i] = next;
            Debug.Log($"x[{i}] = {rooms[i]}");
        }
        string s = "Atatched Rooms:";
        foreach (int item in rooms)
        {   
            s = s+ $" {item}";
        }
        Debug.Log(s);
    }

    static bool Contains(int[] array, int value)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == value) return true;
        }
        return false;
    }
}