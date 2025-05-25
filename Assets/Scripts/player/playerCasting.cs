using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCasting : MonoBehaviour
{
    public bool isCasting = false;
    public moveBobber bobScript;
    public bool bobMoving;
    public GameObject reticleSpawn;
    public reticleAiming aimScript;
    public Reticle reticle;
    [SerializeField] playerMovement moveScript;
    
  private RaycastHit hit;
  private float distance = 6f; 
  

  
    public Reticle Casting()
    {
         Vector3 direction = transform.forward;  
         bobMoving = bobScript.isMove;

                //check if aiming
                if (Input.GetKey(KeyCode.Z) && moveScript.isGrounded() && moveScript.CastCheck && moveScript.isDigging == false)
                {
           
                    isCasting = true;

                    if (moveScript.reticleActive == false && bobScript.bobLanded == false)
                    {
                      if (Physics.Raycast(transform.position, direction, out hit, distance))
                     {
                   //  Debug.Log("Hit a wall!");
                        Instantiate(reticleSpawn, hit.point, Quaternion.Euler(90, 0, 0));
                        }
                        else {
                         Instantiate(reticleSpawn, new Vector3(transform.position.x + (6 * transform.forward.x), transform.position.y, transform.position.z + (6 * transform.forward.z)), Quaternion.Euler(90, 0, 0));
                        }
                        reticle = new Reticle(reticleSpawn, aimScript);
                        moveScript.reticleActive = true;
                        moveScript.positionReticle = GameObject.Find("Reticle(Clone)");
                        reticle.retObj = moveScript.positionReticle;
                    }

                }

                if (isCasting)
                {
             
                        moveScript.rb.velocity = new Vector3(0, 0, 0);
                    
                 /*
                   //start traveling by digging underground
                    if (bobScript.bobLanded == true && Input.GetKeyDown(KeyCode.C) && moveScript.isDigging == false)
                    {
                        moveScript.rb.constraints = RigidbodyConstraints.None;
                        moveScript.isDigging = true;
                        moveScript.isTraveling = true;
                        //Digging: jumps then burrows into the ground
                        moveScript.rb.useGravity = false;
                        moveScript.rb.velocity = new Vector3(moveScript.rb.velocity.x, moveScript.digHop, moveScript.rb.velocity.z);
                        moveScript.Invoke("StartDigging", 0.5f);
                    } */

                }



               else
                {
                    reticle = null;
                }

                if (!Input.GetKey(KeyCode.Z) && isCasting && !bobMoving && moveScript.isHooking == false)
                {
                    isCasting = false;
                    moveScript.reticleActive = false;
                } 
       
       

        return reticle;
    }

}
