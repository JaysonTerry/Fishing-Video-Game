using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackRoom : MonoBehaviour
{
    public RoomsInfo roomsInfo;
    public RoomData[] Rooms;
    [SerializeField] public bool playerInRoom;

    void Start()
    {

      
    }


    void Update()
    {
        
        if (!playerInRoom)
        {
            StartCoroutine(FindPlayerRoom());
        }
    }
    IEnumerator FindPlayerRoom()
    {
         for (int i = 0; i < Rooms.Length; i++)
        {
            if (roomsInfo.roomCol == Rooms[i].roomCol && roomsInfo.roomRow == Rooms[i].roomRow)
            {
                //Set player dot on map to Room Icon position
                this.gameObject.GetComponent<Transform>().position = Rooms[i].mapIcon.GetComponent<Transform>().position;
                playerInRoom = true;

                yield return null;
            }
            else
            {
                playerInRoom = false;
                yield return null;
            }
        } 
    }

}
