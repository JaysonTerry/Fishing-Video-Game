using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveBobber : MonoBehaviour
{
    
    float speed = 0.5f; // Movement speed
    GameObject reticle;
    [SerializeField] playerCasting castScript;
    [SerializeField] playerMovement moveScript;
    Vector3 bobPosition;
    Vector3 endPosition;
    Transform bobTrans;
    float current = 0;
   public bool isMove = false;
   public bool bobLanded = false;
    GameObject player;
    Rigidbody rb;
    void Start()
    {
        player = GameObject.Find("Player");
        castScript = player.GetComponent<playerCasting>();
        moveScript = player.GetComponent<playerMovement>();
        rb = player.GetComponent<Rigidbody>();
    }

    void Update()
    {
       
        if (castScript.isCasting && Input.GetKeyDown(KeyCode.Space) && !bobLanded || isMove)
        {
        
            isMove = true;

            reticle = GameObject.Find("Reticle(Clone)");
            bobTrans = GetComponent<Transform>();
            bobPosition = bobTrans.position;
            endPosition = reticle.transform.position;
            rb.constraints = RigidbodyConstraints.FreezeAll;





            // Move to reticle
            current = Mathf.MoveTowards(current, 1f, speed * Time.deltaTime);
            transform.Translate(transform.forward * speed);
            transform.position = Vector3.Slerp(bobPosition, endPosition, current);


            if (current >= 0.60f)
            {
                isMove = false;
                bobLanded = true;
              //  Debug.Log("Rauru");
                current = 0;
            }
        }


        else if (bobLanded && (Input.GetKeyDown(KeyCode.Space) || !Input.GetKey(KeyCode.Z)))
        {




            // Debug.Log("Mineru");

           
          



            // Move back to player
            transform.position = new Vector3(player.transform.position.x + 2f, player.transform.position.y + 2f, player.transform.position.z);



            isMove = false;
                bobLanded = false;
            castScript.isCasting = false;
             moveScript.reticleActive = false;
            

        } 
        
        } 


        }
    

  

