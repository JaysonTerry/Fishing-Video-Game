using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class HealthDisplay : MonoBehaviour
{
    public static playerHealth healthScript;
    public static TMP_Text healthText;
   



    private void Start()
    {
        healthText = gameObject.GetComponent<TMP_Text>();
        GameObject player = GameObject.Find("Player");
        healthScript = player.GetComponent<playerHealth>();
    }
    void Update()
    {
        
        // Change the text on the text component.
        if (healthScript.tookDamage == true)
        {
            healthText.text = ("Health " + healthScript.health);
            healthScript.tookDamage = false;

        }
    }
   
}
