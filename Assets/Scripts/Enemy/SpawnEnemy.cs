using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Enemy
{
      public string name;
      public int maxHealth;
      public int currentHealth;
      public GameObject enemy;
       public string currentScene;

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
public int dangerLvlToSpawn;
public Enemy enemyToSpawn;
public bool spawned;

 public event EventHandler OnSpawnEvent;

void Start() {

    enemyToSpawn = new Enemy(name, maxHealth, currentHealth, enemy);
    spawned = false;
     OnSpawnEvent += SpawnEnmy;
}

 

void Update() {
    if (dangerLvlToSpawn == SceneData.currentSceneDangerLvl && spawned == false) {
         OnSpawnEvent?.Invoke(this, EventArgs.Empty);
         spawned = true;
    }
}
 
   private void SpawnEnmy(object sender, EventArgs e)
    {
    GameObject enemyInstance = Instantiate(enemy, transform.position, transform.rotation);
       
    }
}
