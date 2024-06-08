using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public class playerHealth : MonoBehaviour
    {
        public int health = 5;
        public playerMovement moveScript;
        public float cooldown = 0f;

        private void Start()
        {
            health = 5;
        }

        private void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.tag == "Player")
            {

            }
            Destroy(col.gameObject);
        }




        void takeDamage()
        {
            health = health - 1;
           // tookDamage = true;
            if (health < 1)
            {
                Destroy(gameObject);
            }
            return;
        }
    }
}

