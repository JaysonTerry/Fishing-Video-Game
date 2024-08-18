using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class playerHealth : MonoBehaviour
{
    public int health = 5;
    public playerMovement moveScript;
    public float cooldown = 0f;
    public bool heldDownZ;
    public bool tookDamage;
    public bool isInvincible;

    private void Awake()
    {
        moveScript = gameObject.GetComponent<playerMovement>();
        OnDamageTaken += I_Frames;
    }
    private void Start()
    {
        health = 5;
        
    }

    public event EventHandler OnDamageTaken;
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Projectile" && !isInvincible)
        {
            takeDamage();
            moveScript.Return2Normal();
            moveScript.enabled = false;
            Vector3 projVelocity = col.gameObject.GetComponent<Rigidbody>().velocity;
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(projVelocity.x / 2, 5, projVelocity.z / 2);
            if (Input.GetKey(KeyCode.Z))
            {
                heldDownZ = true;
            }
            Destroy(col.gameObject);
        }

        else if (col.gameObject.tag == "Enemy" && !isInvincible && col.gameObject.layer != 3)
        {
            takeDamage();
            moveScript.Return2Normal();
            moveScript.enabled = false;
            Quaternion rotation = gameObject.transform.rotation;
            Vector3 moveDir = rotation * Vector3.forward;
            
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(-moveDir.x * 2, 2, -moveDir.z * 2);
            cooldown = 0.4f;
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
        OnDamageTaken?.Invoke(this, EventArgs.Empty);
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


    private void I_Frames(object sender, EventArgs e)
    {
        isInvincible = true;
        StartCoroutine("I_Frame_Flicker");
    }
    IEnumerator I_Frame_Flicker()
    {
        SkinnedMeshRenderer meshRenderer = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
        int flickersLeft = 120; //sets how many flickers before becoming vulrable again

        while (isInvincible == true && flickersLeft > 0)
        {
            meshRenderer.enabled = false;
            yield return null;
            meshRenderer.enabled = true;
            yield return null;
            flickersLeft = flickersLeft - 1; //counts down each flicker
        }
        isInvincible = false;

    }
}
