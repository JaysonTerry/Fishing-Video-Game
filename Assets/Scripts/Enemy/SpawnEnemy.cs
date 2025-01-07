using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy
{
      public string name;
      public int maxHealth;
      public int currentHealth;
      public GameObject enemy;
      public Enemy(string eName, int eMaxHealth, int eCurrentHealth, GameObject eObj) {
      eName = name;
      eMaxHealth = maxHealth;
      eCurrentHealth = currentHealth;
      eObj = enemy;
      }
} 

public class SpawnEnemy : MonoBehaviour 
{

    
 float cooldown = 0f;
public Enemy ememyToSpawn;
public string name;
public int maxHealth;
public int currentHealth;
public GameObject enemy;
public Enemy enemyToSpawn;

 public event EventHandler OnSpawnEvent;

void Start() {

    enemyToSpawn = new Enemy(name, maxHealth, currentHealth, enemy);
     OnSpawnEvent += SpawnEnmy;
}

void Update() {
    if (Input.GetKeyDown(KeyCode.Q)) {
         OnSpawnEvent?.Invoke(this, EventArgs.Empty);
    }
}
 
   private void SpawnEnmy(object sender, EventArgs e)
    {
    GameObject enemyInstance = Instantiate(enemy, transform.position, transform.rotation);
       
    }
}
