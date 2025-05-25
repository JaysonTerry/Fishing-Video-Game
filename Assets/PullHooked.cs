using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullHooked : MonoBehaviour
{
GameObject player;
public playerCasting castScript;
public playerMovement moveScript;
public moveBobber bobScript;

    void Start()
    {
     player = GameObject.Find("Player");
     castScript = player.GetComponent<playerCasting>();
     moveScript = player.GetComponent<playerMovement>();
     bobScript = gameObject.GetComponent<moveBobber>();
    }

   void Update() {
            // Move back to player
           if (moveScript.isHooking == true && Input.GetKeyDown(KeyCode.Space)) {
            Vector3 rightPosition = player.transform.position + player.transform.right;
            rightPosition.y = rightPosition.y + 1f;
           transform.position = rightPosition;

           //Reset Status
           moveScript.InvokeReturn2Normal(0.2f);
             } 
   }
    void OnTriggerEnter(Collider col) 
    {
        if(col.gameObject.tag == "Enemy") {
         castScript.isCasting = true;
        }
    }
}
