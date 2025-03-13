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
    public GameStateController stateController;
    public PlayerPlacer playerPlacer;
    public string nextSpawnDirection;
    [SerializeField] GameObject canvas;
    

    public void Start()
    {
        canvas = GameObject.Find("Canvas 1");
        stateController = canvas.GetComponent<GameStateController>();
        roomDisplay = canvas.GetComponent<RoomDisplay>();
        roomsInfo = canvas.GetComponent<RoomsInfo>();
        playerPlacer = gameObject.GetComponent<PlayerPlacer>();
        
    }

    private void OnDestroy()
    {
        stateController.OnLoadingZone -= Set_Next_Room;
    }




    private void Set_Next_Room(object sender, EventArgs e)
    {
        playerPlacer.enterPos = playerPlacer.SetEnterPositon(nextSpawnDirection);
        GameObject player = GameObject.FindWithTag("Player");
        playerPlacer.PlacePlayer(player);
    }

        public void OnTriggerEnter(Collider other)
    {
       

        if (other.CompareTag("Player") && !other.isTrigger)
        {
         GameObject player = GameObject.FindWithTag("Player");
            stateController.OnLoadingZone += Set_Next_Room;
            playerHealth healthScript = other.gameObject.GetComponent<playerHealth>();
            StateController.startingHealth = healthScript.health;
           player.GetComponent<playerMovement>().Return2Normal();
            //sceneInfo.tracking.playerInRoom = false;
            transitioned = true;
            SceneManager.LoadScene(sceneToLoad);



        }
    }

  
  
        
        

}
