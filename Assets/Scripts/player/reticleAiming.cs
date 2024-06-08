using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reticleAiming : MonoBehaviour
{
    public float movementSpeed = 6f;
    public Rigidbody rb;
    public bool casting = false;
    [SerializeField] playerMovement Movescript;
    [SerializeField] moveBobber Bobberscript;
    [SerializeField] playerCasting Castscript;
    [SerializeField] playerTraveling travelScript;
    public GameObject reticle;
    public GameObject boundary;
    public GameObject player;
    public bool activeRet = false;
    [SerializeField] bool isMovable = true;
    [SerializeField] bool bobLanded = false;

   
    private void Start()
    {
        Movescript = GameObject.Find("Player").GetComponent<playerMovement>();
        Castscript = GameObject.Find("Player").GetComponent<playerCasting>();
        Bobberscript = GameObject.Find("Bobber").GetComponent<moveBobber>();
        travelScript = GameObject.Find("Player").GetComponent<playerTraveling>();
        player = GameObject.Find("Player");
        Instantiate(boundary, new Vector3(player.transform.position.x, player.transform.position.y + 4.5f, player.transform.position.z), Quaternion.identity);
        transform.DetachChildren();
    }

//unloads active reticle
void DestroyReticle()
    {
        Destroy(reticle);
        Destroy(GameObject.Find("Boundary(Clone)"));
    }
    //Moves the reticle while aiming
    public void moveReticle(bool casting)
    {
        if(!isMovable)
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
        
        if (casting && isMovable && !bobLanded)
        {
                float horizontalInput = Input.GetAxis("Horizontal");
                float verticalInput = Input.GetAxis("Vertical");

                rb.velocity = new Vector3(horizontalInput * movementSpeed, rb.velocity.y, verticalInput * movementSpeed);
            
           
        }
        else
        {
            if (travelScript.rising == true)
            {
                activeRet = false;
            }

            if (!activeRet)
            {
                DestroyReticle();
            }
        }
    }

    
    void Update()
    {
        casting = Castscript.isCasting;
       activeRet = Movescript.reticleActive;
        isMovable = !Bobberscript.isMove;
        bobLanded = Bobberscript.bobLanded;
        moveReticle(casting);
    }
        
    }
