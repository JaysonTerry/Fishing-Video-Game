﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCamera : MonoBehaviour
{
    public GameObject player;        //Public variable to store a reference to the player game object
    private Vector3 offset;    
    //Private variable to store the offset distance between the player and camera

    // Start is called before the first frame update
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = new Vector3(0, 7.5f, -15);
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        transform.position = player.transform.position + offset;
    }
}
