using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class RoomData : ScriptableObject
{
    public int roomCol;
    public int roomRow;
    public string roomName;
    public bool playerInRoom;
    public Image mapIcon;
    float timer;



    public void Init(int col, int row, string name, bool inRoom, Image icon)  //initialize the room's parameters
    {
    roomCol = col;
    roomRow = row;
    roomName = name;
    playerInRoom = inRoom;
    mapIcon = icon;
    //DangerLvl = DgrLvl;
   }

   void Update() {        
 timer = timer + Time.deltaTime;
 Debug.Log("TIMER: " + timer);
}

}

