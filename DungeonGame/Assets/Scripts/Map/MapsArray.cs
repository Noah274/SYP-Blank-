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
    
    
    /*
        wUp
        wLeft
        wRight
        wDown
        wCorner
        
        dUp
        dRight
        dLeft
        dDown
        
        fGrass
     */
    /*
     * wUpLeft
     * 21
     * 22
     *
     * 
     */
    public static string[,,] spawnBuild =
    {
        {
            {"wUpLeft", "wUp", "wUp", "dUp", "wUp", "wUp", "wUpRight"},
            {"wLeft", "fGrass", "fGrass", "fGrass", "fGrass", "fGrass", "wRight"},
            {"wLeft", "fGrass", "fGrass", "fGrass", "fGrass", "fGrass", "wRight"},
            {"dLeft","fGrass", "fGrass", "test", "fGrass", "fGrass", "dRight"},
            {"wLeft", "fGrass", "fGrass", "fGrass", "fGrass", "fGrass", "wRight"},
            {"wLeft", "fGrass", "fGrass", "fGrass", "fGrass", "fGrass", "wRight"},
            {"wDownLeft","wDown", "wDown", "dDown", "wDown","wDown", "wDownRight"},
        }
    };
    
    public static string[,,] bossBuild =
    {
        {
            {"wUpLeft", "wUp", "wUp", "dUp", "wUp", "wUp", "wUpRight"},
            {"wLeft", "fGrass", "fGrass", "fGrass", "fGrass", "fGrass", "wRight"},
            {"wLeft", "fGrass", "fGrass", "fGrass", "fGrass", "fGrass", "wRight"},
            {"dLeft","fGrass", "fGrass", "fGrass", "fGrass", "fGrass", "dRight"},
            {"wLeft", "fGrass", "fGrass", "fGrass", "fGrass", "fGrass", "wRight"},
            {"wLeft", "fGrass", "fGrass", "fGrass", "fGrass", "fGrass", "wRight"},
            {"wDownLeft","wDown", "wDown", "dDown", "wDown","wDown", "wDownRight"},
        }
    };
    
    /*
    public static string[,,] roomBuild =
    {
        //y
        {
            //x
            {"wUpLeft", "wUp", "wUp", "dUp", "wUp", "wUp", "wUpRight"},
            {"wLeft", "fGrass", "fGrass", "fGrass", "fGrass", "fGrass", "wRight"},
            {"wLeft", "fGrass", "fGrass", "test", "fGrass", "fGrass", "wRight"},
            {"dLeft","fGrass", "test", "test", "fGrass", "fGrass", "dRight"},
            {"wLeft", "fGrass", "fGrass", "fGrass", "fGrass", "fGrass", "wRight"},
            {"wLeft", "fGrass", "fGrass", "fGrass", "fGrass", "fGrass", "wRight"},
            {"wDownLeft","wDown", "wDown", "dDown", "wDown","wDown", "wDownRight"},
        },
        {
            //x
            {"wUpLeft", "wUp", "wUp", "dUp", "wUp", "wUp", "wUp", "wUpRight"},
            {"wLeft", "fGrass", "fGrass", "fGrass", "fGrass", "fGrass", "fGrass", "wRight"},
            {"wLeft", "fGrass", "fGrass", "test", "fGrass", "fGrass", "fGrass", "wRight"},
            {"dLeft","fGrass", "test", "test", "test", "test", "fGrass", "dRight"},
            {"wLeft", "fGrass", "fGrass", "test", "fGrass", "fGrass", "fGrass", "wRight"},
            {"wLeft", "fGrass", "fGrass", "fGrass", "fGrass", "fGrass", "fGrass", "wRight"},
            {"wDownLeft","wDown", "wDown", "dDown", "wDown","wDown","wDown", "wDownRight"},
        }

    };*/
    
    public static string[,,] roomBuild =
    {
        //y
        {
            {"wUpLeft", "wUp", "wUp", "dUp", "wUp", "wUp", "wUpRight", ""},
            {"wLeft", "fGrass", "fGrass", "fGrass", "fGrass", "fGrass", "wRight", ""},
            {"wLeft", "fGrass", "fGrass", "test", "fGrass", "fGrass", "wRight", ""},
            {"dLeft","fGrass", "test", "test", "fGrass", "fGrass", "dRight", ""},
            {"wLeft", "fGrass", "fGrass", "fGrass", "fGrass", "fGrass", "wRight", ""},
            {"wLeft", "fGrass", "fGrass", "fGrass", "fGrass", "fGrass", "wRight", ""},
            {"wDownLeft","wDown", "wDown", "dDown", "wDown","wDown", "wDownRight", ""},
        },
        {
            //x
            {"wUpLeft", "wUp", "wUp", "dUp", "wUp", "wUp", "wUp", "wUpRight"},
            {"wLeft", "fGrass", "fGrass", "fGrass", "fGrass", "fGrass", "fGrass", "wRight"},
            {"wLeft", "fGrass", "fGrass", "test", "fGrass", "fGrass", "fGrass", "wRight"},
            {"dLeft","fGrass", "test", "test", "test", "test", "fGrass", "dRight"},
            {"wLeft", "fGrass", "fGrass", "test", "fGrass", "fGrass", "fGrass", "wRight"},
            {"wLeft", "fGrass", "fGrass", "fGrass", "fGrass", "fGrass", "fGrass", "wRight"},
            {"wDownLeft","wDown", "wDown", "dDown", "wDown","wDown","wDown", "wDownRight"},
        }

    };
    
}
