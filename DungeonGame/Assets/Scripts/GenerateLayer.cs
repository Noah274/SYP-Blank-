using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLayer : MonoBehaviour
{ 
    public int Layerlevel = 1;
    private System.Random _random = new System.Random();
    int[] possibleRooms = { 0, 0, 0, 0};
    int[] selectedRoom= { 0, 0, 0, 0};
    int countRooms = 0;
    int quantityRooms = 0;
    int[] quantityWay = { 0, 0, 0, 0};
    int[,] roomLayer ={
        //1 = Start
        //2 = End
        //Way1 = 10;
        //Way2 = 11;
        //Way3 = 12;
        //Way4 = 13;
        //0  1  2  3  4  5  6  7  8  9 10
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, //0
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, //1
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, //2
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, //3
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, //4
        { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0}, //5
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, //6
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, //7
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, //8
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, //9
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, //10
            
    };

    public void createLayer()
    {
        int[] roomPos = {5,5};
        /*
        locRoom(roomPos);
        Debug.Log(possibleRooms[0]+ " "+possibleRooms[1]+ " "+possibleRooms[2]+ " "+possibleRooms[3]);
        */

        QuantityRoom();
        spawnRoom(roomPos);

        spawnWays();
    }

    public void spawnWays()
    {
        
    }
    
    public void QuantityRoom()
    {
        quantityRooms = (Random.Range(1, 4) + (5 + (Layerlevel * 2)));
    }
    
    public void locRoom(int[] roomPos)
    {
        countRooms = 0;
        //Über
        if (roomLayer[roomPos[0] - 1,roomPos[1]] == 0)
        {
            addPos(1);
            countRooms++;
        }
        //Rechts
        if (roomLayer[roomPos[0],roomPos[1] + 1] == 0)
        {
            addPos(2);
            countRooms++;
        }
        //Unten 
        if (roomLayer[roomPos[0] + 1,roomPos[1]] == 0)
        {
            addPos(3);
            countRooms++;
        }
        //Links
        if (roomLayer[roomPos[0],roomPos[1] - 1] == 0)
        {
            addPos(4);
            countRooms++;
        }
    }

    public void addPos(int value)
    {
        bool found = false;
        int countPossible = 0;
        while (found == false)
        {
            if (possibleRooms[countPossible] == 0)
            {
                possibleRooms[countPossible] = value;
                found = true;
            }
            countPossible++;
        }
    }
    
    public void spawnRoom(int[] roomPos)
    {
        //int[] possibleRooms = { 1, 2, 3, 4};
        
        locRoom(roomPos);
        Shuffle(possibleRooms);
        selRoom();
        
        //Debug.Log("-" + possibleRooms[0] + "-" + possibleRooms[1] + "-" + possibleRooms[2] + "-" + possibleRooms[3]);
        //Debug.Log("-" + selectedRoom[0] + "-" + selectedRoom[1] + "-" + selectedRoom[2] + "-" + selectedRoom[3]);
        //Debug.Log("-" + quantityWay[0] + "-" + quantityWay[1] + "-" + quantityWay[2] + "-" + quantityWay[3]);
        
        
        //int[] roomPos = {5,5};
        for (int i = 0; i < selectedRoom.GetLength(0); i++)
        {
            if (selectedRoom[i] != 0)
            {
                if (selectedRoom[i] == 1)
                {
                    roomLayer[roomPos[0] - 1, roomPos[1]] = 10;
                }
                if (selectedRoom[i] == 2)
                {
                    roomLayer[roomPos[0], roomPos[1] + 1] = 11;
                }
                if (selectedRoom[i] == 3)
                {
                    roomLayer[roomPos[0] + 1, roomPos[1]] = 12;
                }
                if (selectedRoom[i] == 4)
                {
                    roomLayer[roomPos[0], roomPos[1] - 1] = 13;
                }
                
            }
        }
    }
 
    public void Shuffle(int[] array)
    {
        int[] rooms = new int[4];

        for (int i = 0; i < 4; i++)
        {
            int next = _random.Next(0,4);
            while (true)
            {
                if (rooms[next] == 0)
                {
                    rooms[next] = array[i];
                    break;
                }
                else
                {
                    next= _random.Next(0,4);
                }
            }
        }

        possibleRooms = rooms;
    }
    
    public void selRoom(){
        int randomRange = Random.Range(1, 101); //zwischen 1 - 100
        int randomQuantityWay = Random.Range(1, quantityRooms-2);
        //Debug.Log("===" + randomRange);
        Debug.Log("===" + quantityRooms + "->" + randomQuantityWay);
        
        int num = 0;0
        if (countRooms == 1)
        {
            num = 1;
        }
        else if (countRooms == 2)
        {
            // 2 Räume = 85%
            // 1 Raum = 15%
            if (randomRange <= 15)
            {
                num = 1;
            }
            else
            {
                num = 2;
            }
        }
        else if (countRooms == 3)
        {
            // 3 Räume = 65%
            // 2 Raum = 25%
            // 1 Raum = 10%
            if (randomRange <= 10)
            {
                num = 1;
            }
            else if(randomRange <= 25)
            {
                num = 2;
            }
            else
            {
                num = 3;
            }
        }
        else if (countRooms == 4)
        {
            // 4 Räume = 65%
            // 3 Raum = 25%
            // 2 Räume = 10%
            // 1 Raum = 0%
            if (randomRange <= 10)
            {
                num = 2;
            }
            else if(randomRange <= 25)
            {
                num = 3;
            }
            else
            {
                num = 4;
            }
        }
        
        // 4 Räume
        //  -Way1 = randomQuantityWay / 2
        //  -Way2 = randomQuantityWay / 2
        //  -Way3 = (quantityRooms - randomQuantityWay)/2
        //  -Way4 = (quantityRooms - randomQuantityWay)/2
        // 3 Raum
        //  -Way1 = randomQuantityWay / 2
        //  -Way2 = randomQuantityWay / 2
        //  -Way3 = quantityRooms - randomQuantityWay
        // 2 Räume
        //  -Way1 = randomQuantityWay
        //  -Way2 = quantityRooms - randomQuantityWay
        // 1 Raum
        //  -Way1 = quantityRooms
        if (num == 1)
        {
            quantityWay[0] = quantityRooms;
        }
        else if (num == 2)
        {
            quantityWay[0] = randomQuantityWay;
            quantityWay[1] = quantityRooms - randomQuantityWay;
        }
        else if (num == 3)
        {
            quantityWay[0] = randomQuantityWay / 2;
            quantityWay[1] = randomQuantityWay / 2;
            quantityWay[2] = quantityRooms - randomQuantityWay;
        }
        else if (num == 4)
        {
            quantityWay[0] = randomQuantityWay / 2;
            quantityWay[1] = randomQuantityWay / 2;
            quantityWay[2] = (quantityRooms - randomQuantityWay) /2;
            quantityWay[3] = (quantityRooms - randomQuantityWay) /2;
        }
        
        

        //int num  = Random.Range(1, countRooms+1);
        //Debug.Log("===" + num);
        for (int i = 0; i < num; i++) 
        { 
            selectedRoom[i] = possibleRooms[i];
        }   

    }

    void Start()
    {
        createLayer();
        //printArray();

    }

    public void printArray()
    {

        for (int x = 0; x < roomLayer.GetLength(0); x++)
        {
            string msg = " ";
            for (int y = 0; y < roomLayer.GetLength(1); y++)
            {
                msg += " - " + roomLayer[x, y];
            }

            Debug.Log(x +": "+msg);
        }

    }
}   
