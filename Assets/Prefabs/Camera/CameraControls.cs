using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
 public GameObject player;   
 public GameObject camera2;
 private static Vector3 initialOffset; 
 private static Vector3 offset;
 private Rigidbody rb;
 private float timer = 0f;
 private static bool set = true;
  
    void Start()
    {
        DontDestroyOnLoad(gameObject);  
        player = GameObject.Find("Player");
        camera2 = GameObject.Find("Camera2");
        rb = player.GetComponent<Rigidbody>();
          

        // Calculate the initial offset between the player and the camera
        initialOffset = transform.position - new Vector3(-0.07f,2.98f, 0f);  
    }

   
    void Update()
    {
   

     if (set == true){ 
      offset = initialOffset;
      set = false;
     }

   
    //Debug.Log(timer);
    if (rb.velocity.magnitude <= 0.01f) {
    timer = timer + Time.deltaTime;
    }
    else {
        timer = 0f;
    }
   

    if(timer > 5f) {
        timer = 0f;
       camera2.SetActive(true);
       gameObject.SetActive(false);
    }
  

        // Set the camera position based on the initial offset from the player
        transform.position = player.transform.position + offset;
        // Keep the camera looking at the player
        transform.LookAt(player.transform);

        

 
        }
}
