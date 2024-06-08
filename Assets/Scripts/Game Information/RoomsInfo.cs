using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class RoomsInfo : ScriptableObject
{


    [Header("Information")]
    public string roomName;
    public string shortDescription;
    public int roomCol;
    public int roomRow = 2;
    public List<Room> Rooms = new List<Room>();
    public int[] roomCords;
    public TrackRoom tracking;
    public GameObject sceneManager;


    private void Awake()
    {
        roomCords = new int[2];
        roomCords[0] = roomCol;
        roomCords[1] = roomRow;
        // tracking = GameObject.Find("Player Dot").GetComponent<TrackRoom>();
    }

   



    public void AddRoom(Room roomToAdd)
    {
        //adds a room and displays it on the map
        /* for (int i = 0; i < Rooms.Count; i++)
         {
             //check if room to be added already exists
             if (Rooms[i].roomRow == roomToAdd.roomRow && Rooms[i].roomCol == roomToAdd.roomCol)
             {
                 return;
             }

         } */


        Rooms.Add(roomToAdd);
        
    }
}