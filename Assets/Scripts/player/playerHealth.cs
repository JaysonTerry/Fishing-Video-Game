using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerHealth : MonoBehaviour
{
    public int health = 5;
    public playerMovement moveScript;
    public float cooldown = 0f;
    public bool heldDownZ;
    public bool tookDamage;

    private void Awake()
    {
        moveScript = gameObject.GetComponent<playerMovement>();
    }
    private void Start()
    {
        health = 5;
        
    }

    private void OnTriggerEnter(Collider col)
    {
         if(col.gameObject.tag == "Projectile")
         {
        takeDamage();
            moveScript.Return2Normal();
            moveScript.enabled = false;
            Vector3 projVelocity = col.gameObject.GetComponent<Rigidbody>().velocity;
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(projVelocity.x/2, 5, projVelocity.z/2);
            if (Input.GetKey(KeyCode.Z))
            {
                heldDownZ = true;
            }
            Destroy(col.gameObject);
         }   
    }
   
    void Update()
    {
        if (moveScript != null)
        {
            if (!moveScript.enabled)
            {
                cooldown = cooldown + Time.deltaTime;
            }
            if (!moveScript.enabled && cooldown > 0.8f)
            {
                moveScript.enabled = true;
                cooldown = 0;
                tookDamage = false;
            }
        }
    }

        void takeDamage()
    {
        health = health - 1;
        tookDamage = true;
        if (health < 1)
        {
            gameObject.transform.position = new Vector3(0, 3, 0);
            moveScript.Return2Normal();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            health = 5;
        }
        return;
    }
}
