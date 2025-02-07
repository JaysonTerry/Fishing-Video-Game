using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWallCollison : MonoBehaviour
{
  private RaycastHit hit;
  private float distance = 1f; 
  

    void Start()
    {
        
    }

    
    void Update()
    {
         Vector3 direction = transform.forward;  
    if (Physics.Raycast(transform.position, direction, out hit, distance))
    {
        // If we hit a wall, use the surface normal to adjust movement
        Vector3 normal = hit.normal;

        
        Vector3 pushDirection = normal * -1;  // Moving away from the wall

        // Apply a force or set the velocity based on the normal
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(pushDirection * 10f);  // Adjust strength as needed
        }

     
    }
  
    }
}
