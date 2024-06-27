using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlacer : MonoBehaviour
{
    public Vector3 enterPos;


    public void Start()
    {
       //GameObject player = GameObject.Find("Player");
       // PlacePlayer(player);
    }



    public Vector3 SetEnterPositon(string spawnDirection)
    {
        switch (spawnDirection)
        {
            case "Left":
                return new Vector3(12,0.2f,0);
         
            case "Right":
                return new Vector3(-12, 0.2f, 0);

            case "Up":
                return new Vector3(0, 0.2f, -9);

            case "Down":
                return new Vector3(0, 0.2f, 9);
            default:
                Debug.Log("Unrecongnized Loading Zone Spawn Direction!");
                return new Vector3(0,0,0);
        }
       

    }
    public void PlacePlayer(GameObject player2)
    {
        player2.transform.position = enterPos;
    }
}
