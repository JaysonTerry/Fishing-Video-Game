using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookEnemy : MonoBehaviour
{
 GameObject player;
[SerializeField] playerMovement moveScript;
    void Start()
    {
          player = GameObject.Find("Player");
          moveScript = player.GetComponent<playerMovement>();
    }

    

    // Update is called once per frame
    void Update()
    {
        if(moveScript.isHooking) {
          
            moveScript.rb.constraints = RigidbodyConstraints.FreezeAll;
            

            if (Input.GetKeyDown(KeyCode.Space)) {
                moveScript.isHooking = false;
                   Debug.Log("Done Hooking Enemy!");
            }
        }
     
    }
}
