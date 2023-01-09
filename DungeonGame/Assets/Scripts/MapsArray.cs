using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class MapsArray : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.GetString("firstJason") == "")
        {
            Debug.Log("create-firstJason");
            SaveRoomBuild(roomBuild);
        }
    }
    
    public void SaveRoomBuild(string[,,] roomBuild)
    {
        string jsonString = JsonConvert.SerializeObject(roomBuild);
        //Debug.Log(jsonString);
        PlayerPrefs.SetString("firstJason", jsonString);

    }
    
    public static string[,,] spawnBuild =
    {
        {
            {"wall", "wall", "wall", "doorUp", "wall", "wall", "wall"},
            {"wall", "grass", "grass", "grass", "grass", "grass", "wall"},
            {"wall","grass", "grass", "dirt","grass", "grass", "wall"},
            {"doorLeft","grass", "dirt", "gold", "dirt", "grass", "doorRight"},
            {"wall","grass", "grass", "dirt","grass", "grass", "wall"},
            {"wall","grass", "grass", "grass","grass", "grass", "wall"},
            {"wall","wall", "wall", "doorDown", "wall","wall", "wall"},
        },
        {
            {"wall", "wall", "wall", "doorUp", "wall", "wall", "wall"},
            {"wall", "dirt", "dirt", "grass", "grass", "grass", "wall"},
            {"wall","grass", "grass", "grass","grass", "grass", "wall"},
            {"doorLeft","grass", "grass", "gold", "grass", "grass", "doorRight"},
            {"wall","grass", "grass", "grass","grass", "grass", "wall"},
            {"wall","grass", "grass", "grass","dirt", "dirt", "wall"},
            {"wall","wall", "wall", "doorDown", "wall","wall", "wall"},
        }
    };
    
    public static string[,,] bossBuild =
    {
        {
            {"wall", "wall", "wall", "doorUp", "wall", "wall", "wall"},
            {"wall", "grass", "grass", "grass", "grass", "grass", "wall"},
            {"wall","grass", "dirt", "grass","dirt", "grass", "wall"},
            {"doorLeft","grass", "grass", "dirt", "grass", "grass", "doorRight"},
            {"wall","grass", "dirt", "grass","dirt", "grass", "wall"},
            {"wall","grass", "grass", "grass","grass", "grass", "wall"},
            {"wall","wall", "wall", "doorDown", "wall","wall", "wall"},
        }
    };
    
    public static string[,,] roomBuild =
    {
        {
            {"wall", "wall", "wall", "doorUp", "wall", "wall", "wall"},
            {"wall", "grass", "grass", "grass", "grass", "grass", "wall"},
			{"wall","grass", "grass", "grass","grass", "grass", "wall"},
            {"doorLeft","grass", "grass", "dirt", "grass", "grass", "doorRight"},
            {"wall","grass", "grass", "grass","grass", "grass", "wall"},
			{"wall","grass", "grass", "grass","grass", "grass", "wall"},
            {"wall","wall", "wall", "doorDown", "wall","wall", "wall"},
        },
        {
            { "wall", "wall", "wall", "doorUp", "wall", "wall", "wall"},
            {"wall", "wall", "grass", "grass", "grass", "wall", "wall"},
            {"wall","grass", "grass", "grass","grass", "grass", "wall"},
            {"doorLeft","grass", "grass", "dirt", "grass", "grass", "doorRight"},
            {"wall","grass", "grass", "grass","grass", "grass", "wall"},
            {"wall","wall", "grass", "grass","grass", "wall", "wall"},
            {"wall","wall", "wall", "doorDown", "wall","wall", "wall"},
        }
    };
}
