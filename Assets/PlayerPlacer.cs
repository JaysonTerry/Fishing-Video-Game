using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlacer : MonoBehaviour
{
    [SerializeField] public Vector3 enterPos;
    

    private void Awake()
    {
        GameObject player = GameObject.FindWithTag("Player");
        player.transform.position = enterPos;
        
    }
  
    public Vector3 SetEnterPositon(string spawnDirection)
    {
        switch (spawnDirection)
        {
            case "Left":
                return new Vector3(-18,0,0);

            case "Right":
                return new Vector3(18, 0, 0);

            case "Up":
                return new Vector3(0, 18, 0);

            case "Down":
                return new Vector3(0, -18, 0);
            default:
                Debug.Log("Unrecongnized Loading Zone Spawn Direction!");
                return new Vector3(0,0,0);
        }
    }
}
