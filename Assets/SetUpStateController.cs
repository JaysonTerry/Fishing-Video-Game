using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpStateController : MonoBehaviour
{
    
    void Awake()
    {
        Debug.Log("Awake");
        gameObject.AddComponent<StateController>();
    }

}
