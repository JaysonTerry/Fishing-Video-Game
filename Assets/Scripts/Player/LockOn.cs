using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOn : MonoBehaviour
{
   public float lockOnRadius = 2.5f;
   public GameObject[] enemys;
    private float current = 0;
    private float speed = 2f;
    public bool moveToEnemy = false;
    private float shortestDist = 9999999;
    private  GameObject closestEnemy = null;
   
   void Start() {
     enemys = GameObject.FindGameObjectsWithTag("Enemy");        
   }

   GameObject FindClosestValidEmeny() {
 
    
     foreach (GameObject enemy in enemys) {
      if (enemy != null) {

     if (Vector2.Distance(transform.position, enemy.transform.position) < shortestDist) {
       shortestDist = Vector2.Distance(transform.position, enemy.transform.position);
       closestEnemy = enemy;
     }

     }
     }
      shortestDist = 99999999;
   // Debug.Log(closestEnemy);
          return closestEnemy;
   }

   void LockOnEnemy(GameObject enemy)
    {
    if (enemy != null) {

     
              if (lockOnRadius >= Vector2.Distance(transform.position, enemy.transform.position)) {

              if (Input.GetKeyDown(KeyCode.E)  && Vector2.Distance(transform.position, enemy.transform.position) > 0.05f){
              moveToEnemy = true;
              }

              if (moveToEnemy) {
               current = Mathf.MoveTowards(current, 1f, speed * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z), current);
            //Debug.Log("The Distance is: " + Vector2.Distance(transform.position, enemy.transform.position));
            Debug.Log(current);
            if (current > 0.60f) {
            moveToEnemy = false;
            current = 0f;
            shortestDist = 99999999;
            closestEnemy = null;
            }
           } 

              
            
            }
         }
       }
    
    void Update()
    {
        LockOnEnemy(FindClosestValidEmeny());
    }
}
