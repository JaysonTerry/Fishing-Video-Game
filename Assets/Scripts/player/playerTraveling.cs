using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTraveling : MonoBehaviour
{
   /* [SerializeField] playerMovement moveScript;
    [SerializeField] moveBobber bobberScript;
    [SerializeField] playerCasting castScript;
    [SerializeField] GameObject player;
    public bool rising;
  
    void Start()
    {
        moveScript = player.GetComponent<playerMovement>();
        castScript = player.GetComponent<playerCasting>();
    }

    public void Traveling(Reticle Ret)
    {
        Vector3 endPosition = Ret.retObj.transform.position;

        if (moveScript.isTraveling)
        {
            moveScript.rb.constraints = RigidbodyConstraints.None;
        
            //Digging: check if fully underground
            if (moveScript.isDigging && Mathf.Abs(moveScript.topHeight - Mathf.Abs(moveScript.playerTransform.position.y)) > 5f)
            {
               // Debug.Log("gamers");
                if (moveScript.riseUp)
                {
                    moveScript.rb.velocity = new Vector3(moveScript.rb.velocity.x, moveScript.digHop, moveScript.rb.velocity.z);
                    moveScript.Invoke("Return2Normal", 2f);
                    return;

                }
                else
                {

                    transform.position = new Vector3(endPosition.x, endPosition.y - 5, endPosition.z);
                        Debug.Log("at Reticle");
                        moveScript.isTraveling = false;
                        moveScript.isDigging = false;
                        castScript.isCasting = false;
                    StartCoroutine(waiter());
                      
                    }
                }
            }

        }
    IEnumerator waiter()
    {
        rising = true;
        player.layer = LayerMask.NameToLayer("GroundPass");
        moveScript.enabled = false;
        moveScript.rb.velocity = new Vector3(0, 10f, 0);

        yield return new WaitForSeconds(0.6f);
        rising = false;
        player.layer = LayerMask.NameToLayer("Default");
        moveScript.enabled = true;
        moveScript.rb.useGravity = true;
        Debug.Log("grav on");
    } */
    }

