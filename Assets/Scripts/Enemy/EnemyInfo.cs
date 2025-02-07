using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
 public string name;
 public int maxHealth;
 public int currentHealth;
 public GameObject enemy;
 public bool isHooked;
  
 void Start() {
 currentHealth = maxHealth;
 enemy = this.gameObject;
 }
   
    void Update()
    {
       if (currentHealth < 1) {
              Destroy(gameObject);
       }

    }

    
  
}
