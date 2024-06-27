using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
//2 scripts: Casting, Traveling

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
    private float accleration = 20;
    private float timer;
    public bool CastCheck = true;
    private float speedDif = 0f;
    public static GameObject instance;

    
    

    public Transform groundCheck;
    public LayerMask ground;
   





    public void StartDigging()
    {
        if (isDigging)
        {
            rb.velocity = new Vector3(rb.velocity.x, -1 * digForce, rb.velocity.z);
            Debug.Log("hit");
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
        Debug.Log(CastCheck);
        //Movement: left/right input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
       
       
        

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();
        if (movementDirection != Vector3.zero)
        {
            if (castScript.isCasting)
            {
                if (bobScript.bobLanded)
                {
                    rotationSpeed = 0f;
                }
                else
                {
                    rotationSpeed = 90f;
                }
            }

            else
            {
                rotationSpeed = 720f;
            }
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        //MOVEMENT
        if (castScript.isCasting == false)
        {
            rb.velocity = new Vector3(horizontalInput * movementSpeed, rb.velocity.y, verticalInput * movementSpeed);
            rb.constraints = RigidbodyConstraints.None;
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
                speedDif = 10f - rb.velocity.magnitude;
                movementSpeed = speedDif * accleration;

                Vector3 moveDirection = new Vector3(horizontalInput , 0, verticalInput);

                    rb.AddForce(movementSpeed * moveDirection);
                
            }


           


            else if (playerTransform.position.y < topHeight)
            {
                rb.velocity = new Vector3(horizontalInput* 2f, 5 + (0.02f*movementSpeed), verticalInput * 2f);
            }
            else
            {
                this.rb.useGravity = true;
                isDigging = false;
                isUnderground = false;
                arrowActive = false;
                Destroy(arrowClone);
                movementSpeed = 6;
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

   /* Reticle Casting()
    {
        Reticle reticle = new Reticle(reticleSpawn, aimScript);
        bobMoving = bobScript.isMove;
        if (isCasting)
        {
           
            if (!isTraveling)
            {
                rb.velocity = new Vector3(0, 0, 0);
            }
            //FIX: make popping out work like dig hop
            if (bobScript.bobLanded == true && Input.GetKeyDown(KeyCode.C) && !isDigging)
            {
                rb.constraints = RigidbodyConstraints.None;
                isDigging = true;
                isTraveling = true;
                //Digging: jumps then burrows into the ground
                this.rb.useGravity = false;
                rb.velocity = new Vector3(rb.velocity.x, digHop, rb.velocity.z);
                Invoke("StartDigging", 0.5f);
            }

        }
        
        
        if (Input.GetKey(KeyCode.Z) && isGrounded() && CastCheck && !isDigging)
        {
            isCasting = true;

            if (!reticleActive)
            {
             Instantiate(reticleSpawn, new Vector3(transform.position.x + (6 * transform.forward.x), transform.position.y, transform.position.z + (6 * transform.forward.z)), Quaternion.Euler(90, 0, 0));
             reticle =  new Reticle(reticleSpawn, aimScript);
             reticleActive = true;
             positionReticle = GameObject.Find("Reticle(Clone)");
            }

        }
        
        if (Input.GetKeyUp(KeyCode.Z) && isCasting && !bobMoving)
        {
            isCasting = false;
            reticleActive = false;

        }
        if (positionReticle != null)
        {
            reticle.retObj = positionReticle;
        }
        return reticle;
    } */

    /*void Traveling(Reticle Ret)
    {
        Vector3 startPosition = this.transform.position;
        Vector3 endPosition = this.transform.position;

        if (Ret != null)
        {
            startPosition = this.transform.position;
            endPosition = Ret.retObj.transform.position;
        }
 
        if (isTraveling)
        {
            rb.constraints = RigidbodyConstraints.None;
            float speed = 25f;
            
            //Digging: check if fully underground
            if (isDigging && Mathf.Abs(topHeight - Mathf.Abs(playerTransform.position.y)) > 5f)
            {
                if (!Input.GetKey(KeyCode.C)) {
                    riseUp = true;
                }
                if (riseUp)
                {
                    rb.velocity = new Vector3(rb.velocity.x, digHop, rb.velocity.z);
                    Invoke("Return2Normal", 2f);
                    return;
                    
                }
                else 
                {
                   
                    current = Mathf.MoveTowards(current, 1f, speed * Time.deltaTime);
                    transform.Translate(transform.forward * speed);
                    transform.position = Vector3.Lerp(startPosition, endPosition, current);

                    if (current == 1)
                    {
                        Debug.Log("at Reticle");
                        isTraveling = false;
                        isDigging = false;
                        rb.velocity = new Vector3(0, 5f, 0);
                        this.rb.useGravity = true;
                        current = 0;

                    }
                }
            }
            
        }
    } */
}
        


    
    