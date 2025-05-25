
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Reticle
{
    public GameObject retObj;
    public reticleAiming aiScript;


    //constructor

    public Reticle(GameObject Ret, reticleAiming Aim)
    {
        retObj = Ret;
        aiScript = Aim;
    }
}
public class playerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public Transform playerTransform;
    public Reticle playerReticle;
    public GameObject reticleSpawn;
    public GameObject positionReticle = null;
    public GameObject arrow;
    public moveBobber bobScript;
    public reticleAiming aimScript;
    public playerHealth healthScript;
    [SerializeField] playerCasting castScript;
    [SerializeField] playerTraveling travelScript;
    [SerializeField] GameObject arrowClone;
    public float movementSpeed = 6f;
    public float digForce = 5f;
    public float digHop = 10f;
    public float rotationSpeed = 720f;
    public bool isDigging = false;
    public bool isDiggingDown = false;
    public bool reticleActive = false;
    public bool bobMoving;
    [SerializeField] bool arrowActive = false;
    public float topHeight;
    bool isUnderground = false;
    public bool isTraveling = false;
    public bool riseUp = false;
    public bool risingFromDig = false;
    private float timer;
    public bool CastCheck = true;
    public static GameObject instance;
    [SerializeField] Vector3 movementDirection;
    public bool isHooking;
    public float holdTimeThreshold = 0.5f;
    private float timePressed = 0f;
    private bool isSpacebarHeld = false;





    public Transform groundCheck;
    public LayerMask ground;

      
    bool accelTrigger = false;
    float digAccel = 2200f;
    float digDecel = 100f;







    public void StartDigging()
    {
     
     isDiggingDown = false;
       isDigging = true;
       rb.AddForce(0, -3, 0, ForceMode.Impulse);
    }
    //resets Player Status to Default
    public void Return2Normal()
    {
        isTraveling = false;
        isDigging = false;
        castScript.isCasting = false;
        isHooking = false;
        rb.velocity = new Vector3(0, 0f, 0);
        this.rb.useGravity = true;
        reticleActive = false;
        riseUp = false;
        CastCheck = false;
    }

    //for delaying Return2Normal
    public void InvokeReturn2Normal(float delay)
{
    Invoke("Return2Normal", delay);
}

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = gameObject;
    }


    void Start()
    {
        DontDestroyOnLoad(gameObject);
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX;
         rb.constraints = RigidbodyConstraints.FreezeRotationZ;
        playerTransform = GetComponent<Transform>();
        CastCheck = true;
        playerReticle = new Reticle(this.gameObject, aimScript);
        arrowClone = this.gameObject;
        digHop = 7f;


    }


    void Update()
    {
        
        //Movement: left/right input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();



        //rotation while casting
        if (movementDirection != Vector3.zero)
        {

            if (castScript.isCasting)
            {
             GameObject reticle = GameObject.Find("Reticle(Clone)");

             if (reticle != null) {
            Vector3 direction = reticle.transform.position - transform.position;
             Quaternion toRotation = Quaternion.LookRotation(direction);
              direction = reticle.transform.rotation * direction;
                direction.y = 0;
              transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
            

          


                if (bobScript.bobLanded)
                {
                    rotationSpeed = 0f;
                }
                else
                {
                    rotationSpeed = 720f;
                }
            }

            else
            {
            if (!isHooking){
                rotationSpeed = 720f;
                 Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
                }
            }

       
           
        }
        //MOVEMENT
        if (castScript.isCasting == false && !isDiggingDown)
        {
            rb.velocity = new Vector3(horizontalInput * movementSpeed, rb.velocity.y, verticalInput * movementSpeed);
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        //DIGGING && JUMPING
        

        // Check if spacebar is pressed and the object is grounded and Jumps
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded() && !castScript.isCasting)
        {
            // Apply upward force using AddForce (Impulse mode)
            rb.AddForce(Vector3.up * digHop, ForceMode.Impulse);
        }

        
    if (Input.GetKey(KeyCode.Space) && isGrounded())
    {
        timePressed += Time.deltaTime;

        if (timePressed >= holdTimeThreshold && !isSpacebarHeld)
        {
            isSpacebarHeld = true;
        }
    }
    else
    {
        timePressed = 0f;
        isSpacebarHeld = false;
    }

        
        if (Input.GetKeyUp(KeyCode.Space) || isSpacebarHeld == true)
        {
           rb.AddForce(Vector3.down * digHop, ForceMode.Impulse);
            isDigging = false;
        }

         else if (Input.GetKeyDown(KeyCode.X) && !castScript.isCasting)
        {
        isDiggingDown = true;
         rb.velocity = Vector3.zero;
         topHeight = playerTransform.position.y;
         Invoke("StartDigging", 0.5f);
        } 



        //Digging: check if fully underground
        if (isDigging && Mathf.Abs(topHeight - playerTransform.position.y) > 3.5f && !isTraveling)
        {
            rb.velocity = new Vector3(0, 0, 0);
            isUnderground = true;
            gameObject.GetComponent<digCheck>().enabled = true;
        }
        
        //if player is fully underground
        if (isUnderground)
        {
            this.rb.useGravity = false;

        //place an arrow that floats above player and faces camera
            if (!arrowActive)
            {
                arrowClone = Instantiate(arrow, transform.position, transform.rotation);
                arrowClone.transform.position = new Vector3(transform.position.x, transform.position.y + 4, transform.position.z);
                arrowActive = true;
            }
            else
            {
                arrowClone.transform.position = new Vector3(transform.position.x, transform.position.y + 4, transform.position.z);
            }

            //accelration while underground
            if (Input.GetKey(KeyCode.X)){
           
            if(accelTrigger == false){
             rb.velocity = new Vector3(horizontalInput * movementSpeed, 0f, verticalInput * movementSpeed);
             accelTrigger = true;
             }
            
           
             else {

               if (movementDirection == Vector3.zero) {
                rb.velocity -= movementDirection * digAccel * Time.deltaTime;
                digDecel = Mathf.Pow(digDecel, 2f);

             }
             else {
               rb.velocity += movementDirection * digAccel * Time.deltaTime;
               Debug.Log("ACCEL" + rb.velocity);
               }
                  }

            }

           


            else if (playerTransform.position.y < topHeight)
            {
                rb.velocity = new Vector3(horizontalInput * 2f, 5 + (0.02f * movementSpeed), verticalInput * 2f);
                risingFromDig = true;
             digDecel = 100f;
            }
            else
            {
                this.rb.useGravity = true;
                isDigging = false;
                isUnderground = false;
                arrowActive = false;
                risingFromDig = false;
                Destroy(arrowClone);
                movementSpeed = 6;
                accelTrigger = false;
            }
        }


        //CASTING

        playerReticle = castScript.Casting();

        //TRAVELING
       // if(Input.GetKeyDown(KeyCode.C)) {
        //        isTraveling = true;
      //  }
       /* if (playerReticle != null)
        {
            travelScript.Traveling(playerReticle);
        } */
        if (healthScript.heldDownZ == true)
        {
            if (!Input.GetKey(KeyCode.Z))
            {
                healthScript.heldDownZ = false;
            }
        }
        if (isGrounded() && !healthScript.heldDownZ)
        {
            CastCheck = true;
        }
    }

    public bool isGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, 0.1f, ground);
    }

    public void pullHooked() {

    }
}

  