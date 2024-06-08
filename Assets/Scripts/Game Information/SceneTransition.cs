using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    public int currentHealth;
    public RoomsInfo roomsInfo;
    public RoomDisplay roomDisplay;
    public bool transitioned;
   

    public int nextRoomCol;
    public  int nextRoomRow;
    public string nextRoomName;
    public Image roomIcon;
    public StateController stateController;
    public PlayerPlacer playerPlacer;
    public string nextSpawnDirection;
    [SerializeField] GameObject canvas;
    

    public void Start()
    {
        canvas = GameObject.Find("Canvas 1");
        stateController = canvas.GetComponent<StateController>();
        roomDisplay = canvas.GetComponent<RoomDisplay>();
        playerPlacer = gameObject.GetComponent<PlayerPlacer>();
        
    }




    private void Set_Next_Room(object sender, EventArgs e)
    {
        GameObject canvas = GameObject.Find("Canvas 1");
        Room newRoom = canvas.AddComponent<Room>();
        newRoom.Init(nextRoomCol, nextRoomRow, nextRoomName, true, roomIcon);
        Debug.Log(newRoom.roomName);
        roomsInfo.AddRoom(newRoom);
        roomDisplay.Update_Map(nextRoomCol, nextRoomRow);
        playerPlacer.enterPos = playerPlacer.SetEnterPositon(nextSpawnDirection);
    }

        public void OnTriggerEnter(Collider other)
    {
       

        if (other.CompareTag("Player") && !other.isTrigger)
        {
            stateController.OnLoadingZone += Set_Next_Room;
            playerHealth healthScript = other.gameObject.GetComponent<playerHealth>();
            StateController.startingHealth = healthScript.health;
            //sceneInfo.tracking.playerInRoom = false;
            transitioned = true;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
   
}
