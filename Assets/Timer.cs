using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
  
    public static TMP_Text timerText;

    // Total time in seconds 
    public float totalTime = 10f; 
    public static float remainingTime;

    void Start() {
        remainingTime = totalTime;
        timerText = gameObject.GetComponent<TMP_Text>();
    }

    void Update()
    {
   
            if (remainingTime <= 0f) {
                Time.timeScale = 0f;
                 timerText.text = "You lose...";
                }
        // Only update if the game is not paused
        if (Time.timeScale != 0f)
        {
           
            remainingTime -= Time.deltaTime;

           

            int minutes = Mathf.FloorToInt(remainingTime / 60f);
            int seconds = Mathf.FloorToInt(remainingTime % 60f);

            // Display the time on the timerText UI element
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

 
    public void ResetTimer()
    {
        remainingTime = totalTime;
    }
}
