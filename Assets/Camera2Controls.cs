using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2Controls : MonoBehaviour
{
 public GameObject player;       
 public playerCasting castScript;
 private Rigidbody rb;
 public GameObject camera1;
 public GameObject camera2;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject); 
        player = GameObject.Find("Player");
        castScript = player.GetComponent<playerCasting>();
        camera1 = GameObject.Find("Camera1");
        camera2 = GameObject.Find("Camera2");
        rb = player.GetComponent<Rigidbody>();
     
    }

   
    void Update()
    {
  
    if (rb.velocity.magnitude != 0f || castScript.isCasting == true) {
      camera1.SetActive(true); 
      camera2.SetActive(false);
    }


   
        }
}
