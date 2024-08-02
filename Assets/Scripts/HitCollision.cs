using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollision : MonoBehaviour
{


    private void OnTriggerEnter(Collider col)
    {
        playerHealth healthScript = gameObject.GetComponentInParent<playerHealth>();
        //Debug.Log(col.gameObject.name);
        if (col.gameObject.tag == "Enemy")
        {
            Destroy(col.gameObject);
        }
    }
}
