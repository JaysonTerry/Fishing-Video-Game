using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetDangerLevel : MonoBehaviour
{
public string[] Scenes;
public int[] DangerLvls;
public float[] setTimes;
    // Start is called before the first frame update
    void Start()
    {
     DontDestroyOnLoad(gameObject);
     int sceneCount = SceneManager.sceneCountInBuildSettings;
     Scenes = new string[sceneCount];
     DangerLvls = new int[sceneCount];
     setTimes = new float[sceneCount];
        for (int i = 0; i < sceneCount; i++)
        {
             string sceneName = SceneUtility.GetScenePathByBuildIndex(i);
            sceneName = System.IO.Path.GetFileNameWithoutExtension(sceneName); // Get only the scene name without the path
             Scenes[i] = sceneName; // Add the scene name to the list
             setTimes[i] = 9999999999999; //add default value for time
             //Debug.Log(Scenes[i]);
        }

     
    }

    // Update is called once per frame
    void Update()
    {
      SetTimer();
      SetDangerLvl();
    }
 void SetDangerLvl() 
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        string currentScene = SceneManager.GetActiveScene().name;

        for (int i = 0; i < sceneCount; i++)
        {
            if (Scenes[i] != currentScene) 
            {
                if (Time.time - setTimes[i] > 5) 
                {
                    DangerLvls[i] = 1;
                    Debug.Log(Scenes[i] + " has a danger level of " + DangerLvls[i] + "!");
                }
            }
        }
    }
      void SetTimer() 
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        string currentScene = SceneManager.GetActiveScene().name;

        for (int i = 0; i < sceneCount; i++)
        {
            if (Scenes[i] == currentScene) 
            {
                setTimes[i] = Time.time;
               //Debug.Log(currentScene + ": " + setTimes[i]);
            }
        }
    }
    
    }

