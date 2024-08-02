using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRisingHitbox : MonoBehaviour
{
    public playerMovement moveScript;
    [SerializeField] GameObject risingHitbox;
    private GameObject hitbox;
    [SerializeField] bool hitboxActive;

    void Start()
    {
        moveScript = gameObject.GetComponent<playerMovement>();
    }

    void Update()
    {
        bool playerIsRising = moveScript.risingFromDig;
       

        if (playerIsRising)
        {

            if (!hitboxActive)
            {
                hitbox = Instantiate(risingHitbox, gameObject.transform);
                Vector3 hitboxPosition = hitbox.transform.position;
                hitboxPosition.y = hitboxPosition.y + 0.5f;
                hitbox.transform.position = hitboxPosition;
                hitboxActive = true;
            }
          
        }
        else
        {
            Destroy(hitbox);
            hitboxActive = false;
        }
    }
}
