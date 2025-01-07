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
    public float digHop = 1.5f;
    public float rotationSpeed = 720f;
    public bool isDigging = false;
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




    public Transform groundCheck;
    public LayerMask ground;

     float secondsToChangeDir = 2f;
     float secondsSoFar = 0.0f;
      bool slipping = false;
       bool frameSwitch = false;
      Vector3 lastMoveVector = Vector3.zero;
       Vector3 moveVector = Vector3.zero;
       float startTime;
       bool movingFwd = false;
       KeyCode pressedKey = KeyCode.None;







    public void StartDigging()
    {
        if (isDigging)
        {
            rb.velocity = new Vector3(rb.velocity.x, -1 * digForce, rb.velocity.z);
            topHeight = playerTransform.position.y;
        }
    }

    public void Return2Normal()
    {
        isTraveling = false;
        isDigging = false;
        castScript.isCasting = false;
        rb.velocity = new Vector3(0, 0f, 0);
        this.rb.useGravity = true;
        reticleActive = false;
        riseUp = false;
        CastCheck = false;
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
        playerTransform = GetComponent<Transform>();
        CastCheck = true;
        playerReticle = new Reticle(this.gameObject, aimScript);
        arrowClone = this.gameObject;


    }


    void Update()
    {
        //Debug.Log(CastCheck);
        Debug.Log(rotationSpeed);
        //Movement: left/right input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        


        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();
        if (movementDirection != Vector3.zero)
        {

            if (castScript.isCasting)
            {
             GameObject reticle = GameObject.Find("Reticle(Clone)");
            Vector3 direction = reticle.transform.position - transform.position;
             Quaternion toRotation = Quaternion.LookRotation(direction);
              direction = reticle.transform.rotation * direction;
                direction.y = 0;

             transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);


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
                rotationSpeed = 720f;
                 Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }

       
           
        }
        //MOVEMENT
        if (castScript.isCasting == false)
        {
            rb.velocity = new Vector3(horizontalInput * movementSpeed, rb.velocity.y, verticalInput * movementSpeed);
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        //DIGGING
        //Digging: jumps then burrows into the ground
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded() && castScript.isCasting == false)
        {
            timer = Time.time;
            this.rb.useGravity = false;
            isDigging = true;
            rb.velocity = new Vector3(rb.velocity.x, digHop, rb.velocity.z);
            Invoke("StartDigging", 0.5f);
        }
        else if (Input.GetKeyUp(KeyCode.Space) && Time.time - timer < 0.5f)
        {
            rb.AddForce(0, -3, 0);
            this.rb.useGravity = true;
            isDigging = false;
        }



        //Digging: check if fully underground
        if (isDigging && Mathf.Abs(topHeight - playerTransform.position.y) > 3.5f && !isTraveling)
        {
            rb.velocity = new Vector3(0, 0, 0);
            isUnderground = true;
            gameObject.GetComponent<digCheck>().enabled = true;
        }

        if (isUnderground)
        {

            if (!arrowActive)
            {
                arrowClone = Instantiate(arrow, transform.position, transform.rotation);
                arrowClone.transform.position = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z);
                arrowActive = true;
            }
            else
            {
                arrowClone.transform.position = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z);
            }

            if (Input.GetKey(KeyCode.Space))
            {
            
              
                if (!slipping && movementDirection != Vector3.zero) {
                moveVector = movementDirection;

                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)) {
                
                 if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                      pressedKey = KeyCode.LeftArrow;
                 }
                  else if (Input.GetKeyDown(KeyCode.RightArrow)) {
                      pressedKey = KeyCode.RightArrow;
                 }
                 else if (Input.GetKeyDown(KeyCode.UpArrow)) {
                      pressedKey = KeyCode.UpArrow;
                 }
                 else if (Input.GetKeyDown(KeyCode.DownArrow)) {
                      pressedKey = KeyCode.DownArrow;
                 }

                    startTime = Time.time;
                    }
                    // Debug.Log(Time.time - startTime);
                    if(Input.GetKeyUp(pressedKey) && (Time.time - startTime) > 1.5f) {
                    movingFwd = true;
                    startTime = Time.time;
                    }
                
              
                }

             /*
               if (lastMoveVector != Vector3.zero) {

                Debug.Log("Last" + lastMoveVector);
                }
                if (moveVector != Vector3.zero) {
                Debug.Log(" This" + moveVector);
                } 
                */

                float t = secondsSoFar / secondsToChangeDir;
          
                if (lastMoveVector == -moveVector && moveVector != Vector3.zero && lastMoveVector != Vector3.zero && movingFwd) {
                secondsSoFar += Time.deltaTime;
                slipping = true;

                 rb.velocity = lastMoveVector * movementSpeed;
                 movementSpeed = Mathf.Lerp(movementSpeed, 0.75f, t);
                //Debug.Log("Portion lerped: " + t);
                }
                else {
                    secondsSoFar = 0;
                }
               
                 
                 if(t > 0.4f) {
                 secondsSoFar = 0;
                slipping = false;
                lastMoveVector = Vector3.zero;
                moveVector = Vector3.zero;
                movementSpeed = 6f;
                startTime = Time.time;
                 if (Input.GetKey(KeyCode.LeftArrow)) {
                      pressedKey = KeyCode.LeftArrow;
                 }
                 else if (Input.GetKey(KeyCode.RightArrow)) {
                      pressedKey = KeyCode.RightArrow;
                 }
                 else if (Input.GetKey(KeyCode.UpArrow)) {
                      pressedKey = KeyCode.UpArrow;
                 }
                 else if (Input.GetKey(KeyCode.DownArrow)) {
                      pressedKey = KeyCode.DownArrow;
                 }
               }
                 if(!slipping)  {
                 
                   rb.velocity = new Vector3(horizontalInput * movementSpeed, rb.velocity.y, verticalInput * movementSpeed);
                 
                lastMoveVector = moveVector;
                }
                


            }

           


            else if (playerTransform.position.y < topHeight)
            {
                rb.velocity = new Vector3(horizontalInput * 2f, 5 + (0.02f * movementSpeed), verticalInput * 2f);
                risingFromDig = true;
             lastMoveVector = Vector3.zero;
             moveVector = Vector3.zero;
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
                secondsSoFar = 0f;
                slipping = false;
            }
        }


        //CASTING

        playerReticle = castScript.Casting();

        //TRAVELING
        if (playerReticle != null)
        {
            travelScript.Traveling(playerReticle);
        }
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
}

  