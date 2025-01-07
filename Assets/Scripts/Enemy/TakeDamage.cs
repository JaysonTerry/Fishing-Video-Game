using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public EnemyInfo enemyInfo;
   
    void Start()
    {
        enemyInfo = gameObject.GetComponentInParent<EnemyInfo>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hitbox")
        {
              enemyInfo.currentHealth = enemyInfo.currentHealth - 1;
              Debug.Log( enemyInfo.currentHealth );
        }
    }
}
