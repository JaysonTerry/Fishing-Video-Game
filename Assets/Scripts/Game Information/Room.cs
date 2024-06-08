using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public int roomCol;
    public int roomRow;
    public string roomName;
    public bool playerInRoom;
    public Image mapIcon;



    public void Init(int col, int row, string name, bool inRoom, Image icon)  //initialize the room's parameters
    {
    roomCol = col;
    roomRow = row;
    roomName = name;
    playerInRoom = inRoom;
    mapIcon = icon;
        }




   

}






