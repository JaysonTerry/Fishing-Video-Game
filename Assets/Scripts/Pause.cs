using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

   
    void PauseGame()
    {
        Time.timeScale = 0f; // Freezes time in the scene
        isPaused = true;
        // Optionally, you can display a pause menu here
    }

    // Function to resume the game
    void ResumeGame()
    {
        Time.timeScale = 1f; // Resumes time flow
        isPaused = false;
    }
}
