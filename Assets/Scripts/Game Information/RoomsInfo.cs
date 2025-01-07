using UnityEngine;
using System.Collections.Generic;



public class RoomsInfo : MonoBehaviour
{


    public string roomName;
    public string shortDescription;
    public int roomCol;
    public int roomRow = 2;
    public List<RoomData> Rooms = new List<RoomData>();
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

}