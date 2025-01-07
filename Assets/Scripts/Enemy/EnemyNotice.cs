using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNotice : MonoBehaviour
{
    public float sightRadius;
    public bool inSight;
    public Vector3 lookPos;
    void Start()
    {
        sightRadius = 10f;
    }


    void OnSightingPlayer()
    {

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float speed = 10f;

        if (sightRadius >= Vector2.Distance(transform.position, player.transform.position))
        {
            lookPos = player.transform.position - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed);
            inSight = true;
        }
        else
        {
            inSight = false;
        }
    }
    void Update()
    {
        OnSightingPlayer();

    }
}
