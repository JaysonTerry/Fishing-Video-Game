using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyDamage : MonoBehaviour
{
 public event EventHandler OnDamageTaken;
 public EnemyInfo info;
 GameObject player;
 playerMovement moveScript;
 
    // Start is called before the first frame update
    void Start()
    {
     player = GameObject.Find("Player");
     moveScript = player.GetComponent<playerMovement>();
      info = gameObject.GetComponentInParent<EnemyInfo>();
    }

   void OnTriggerEnter(Collider col) {
      if(col.gameObject.tag == "Bobber") {
            info.isHooked = true;
             moveScript.Return2Normal();
             moveScript.isHooking = true;
           //Debug.Log("Is the enemy hooked? " + info.isHooked);
        }
    }

    void OnTriggerExit(Collider col) {
      if(col.gameObject.tag == "Bobber") {
          Destroy(gameObject.transform.parent.gameObject);
        }
    }
        void takeDamage(int dmgToTake)
    {
        OnDamageTaken?.Invoke(this, EventArgs.Empty);
        info.currentHealth = info.currentHealth - dmgToTake;

    
       
        return;
    }
}
