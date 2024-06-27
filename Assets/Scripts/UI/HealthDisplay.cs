using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class HealthDisplay : MonoBehaviour
{
    public playerHealth healthScript;
    public TMP_Text healthText;
    public GameStateController stateController;




    private void Start()
    {
        healthText = GetComponentInChildren<TMP_Text>();
        GameObject player = GameObject.Find("Player");
        healthScript = player.GetComponent<playerHealth>();
        //healthText.text = "Health " + StateController.startingHealth;
    }
    void Update()
    {
        
        // Change the text on the text component.
        if (healthScript.tookDamage == true)
        {
            healthText.SetText("Health " + healthScript.health);
            healthScript.tookDamage = false;

        }
    }
   
}
