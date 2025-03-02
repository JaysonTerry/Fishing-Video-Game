using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveBobber : MonoBehaviour
{
    
    private float speed = 10f; // Movement speed
   public GameObject reticle;
    [SerializeField] playerCasting castScript;
    [SerializeField] playerMovement moveScript;
    Vector3 bobPosition;
    Vector3 endPosition;
    Transform bobTrans;
    private float current = 0;
    private float current2 = 0;
   public bool isMove = false;
   public bool bobLanded = false;
    GameObject player;
    Rigidbody rb;
    Vector3 midPoint;
   bool midReached = false;
    void Start()
    {
        player = GameObject.Find("Player");
        castScript = player.GetComponent<playerCasting>();
        moveScript = player.GetComponent<playerMovement>();
        rb = player.GetComponent<Rigidbody>();
    }

    void Update()
    {
    
        if (castScript.isCasting && Input.GetKeyDown(KeyCode.Space) && !bobLanded)
        {
        
            isMove = true;

            reticle = GameObject.Find("Reticle(Clone)");
            bobTrans = GetComponent<Transform>();
            bobPosition = bobTrans.position;
            endPosition = reticle.transform.position;
            midPoint = (bobPosition + endPosition)/2;
            midPoint = new Vector3(midPoint.x, midPoint.y + 5f, midPoint.z);
           rb.constraints = RigidbodyConstraints.FreezeAll;
           }



           if(isMove){

            // Move to reticle
           Vector3 bobPos = bobTrans.position;
          

            if (Mathf.Abs((bobPos - midPoint).magnitude) >= 0.05f)
            {
             current = Mathf.MoveTowards(current, 1f, speed * Time.deltaTime);
            transform.position = Vector3.Lerp(bobPosition, midPoint, current);
            }
            else if(!midReached){
            midReached = true;
            bobPosition = bobTrans.position;
            }
            if (midReached) {
                 current2 = Mathf.MoveTowards(current2, 1f, speed * Time.deltaTime);
            transform.position = Vector3.Lerp(bobPosition, endPosition, current2);
           
            }
            if (Mathf.Abs((bobTrans.position - endPosition).magnitude) <= 0.05f)
            {
                isMove = false;
                bobLanded = true;
                moveScript.reticleActive = false;
                rb.constraints = RigidbodyConstraints.FreezeAll;
               //  Debug.Log("Rauru");
                current = 0;
                current2 = 0;
                midReached = false;
                 castScript.isCasting = false;
            }
        }


        else if (bobLanded && (Input.GetKeyDown(KeyCode.Space) || !Input.GetKey(KeyCode.Z)))
        {
        

            // Debug.Log("Mineru")
            // Move back to player
            if (moveScript.isHooking == false) {
            Vector3 rightPosition = player.transform.position + player.transform.right;
            rightPosition.y = rightPosition.y + 1f;
           transform.position = rightPosition;
            isMove = false;
            bobLanded = false;
             moveScript.reticleActive = false;
             }
            

        } 
        
        } 


        }
    

  

