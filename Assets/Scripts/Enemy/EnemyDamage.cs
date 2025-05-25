using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyDamage : MonoBehaviour
{
 public event EventHandler OnDamageTaken;
 public EnemyInfo info;
 private EnemyNotice notice;
 private shootBullet shoot;
 GameObject player;
 playerMovement moveScript;
 
    // Start is called before the first frame update
    void Start()
    {
     player = GameObject.Find("Player");
     moveScript = player.GetComponent<playerMovement>();
      info = gameObject.GetComponentInParent<EnemyInfo>();
      notice = gameObject.GetComponentInParent<EnemyNotice>();
      shoot = gameObject.GetComponentInParent<shootBullet>();
    }

    void Update() {
      //kill enemy after bobber leaves contact with enemy
     if( info.isHooked == true && Input.GetKeyDown(KeyCode.Space)) {
          Destroy(gameObject.transform.parent.gameObject);
        }
    }

   void OnTriggerEnter(Collider col) {

   //hook enemy if hit by bobber

      if(col.gameObject.tag == "Bobber") {
            info.isHooked = true;
             moveScript.Return2Normal();
             moveScript.isHooking = true;
             notice.enabled = false;
             shoot.enabled = false;
          // Debug.Log("Is the enemy hooked? " + info.isHooked);
        }
    }

  
        void takeDamage(int dmgToTake)
    {
        OnDamageTaken?.Invoke(this, EventArgs.Empty);
        info.currentHealth = info.currentHealth - dmgToTake;

    
       
        return;
    }
}
