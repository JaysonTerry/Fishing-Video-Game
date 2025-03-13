using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisplayRoom : MonoBehaviour
{
  public CanvasGroup canvasGroup;
  public string roomName;

   
    void Start()
    {
       canvasGroup = gameObject.GetComponent<CanvasGroup>();
       canvasGroup.alpha = 0f;
    }

    
    void Update()
    {
     if ( SceneManager.GetActiveScene().name == roomName) {
        canvasGroup.alpha = 1f;
        }
    }
}
