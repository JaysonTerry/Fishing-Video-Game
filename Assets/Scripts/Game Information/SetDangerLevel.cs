using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetDangerLevel : MonoBehaviour
{
    public static string[] Scenes;
    public static int[] DangerLvls;
    public static float[] setTimes;
    public SetText setText;
    public static string currentScene;
    public static int currentSceneDangerLvl;
    private bool ranOnce;
    public float limitTimer = 0f;
    public bool criticalDanger = false;

    void Start()
    {
        GameObject text = GameObject.Find("Content");
        setText = text.GetComponent<SetText>();
        DontDestroyOnLoad(gameObject);

        // Set up the static arrays when the first scene is loaded
        if (Scenes == null || Scenes.Length == 0)
        {
            int sceneCount = SceneManager.sceneCountInBuildSettings;
            Scenes = new string[sceneCount];
            DangerLvls = new int[sceneCount];
            setTimes = new float[sceneCount];
            for (int i = 0; i < sceneCount; i++)
            {
                string sceneName = SceneUtility.GetScenePathByBuildIndex(i);
                sceneName = System.IO.Path.GetFileNameWithoutExtension(sceneName); // Get only the scene name without the path
                Scenes[i] = sceneName; // Add the scene name to the list
                setTimes[i] = 9999999999999; // Add default value for time
            }
        }

        // Register to listen for scene changes
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Update the current scene when a new scene is loaded
        currentScene = scene.name;
        ranOnce = false;
    }

    void Update()
    {
        currentScene = SceneManager.GetActiveScene().name;
        SetTimer();
        SetDangerLvl();
        GetCurrentSceneDangerLvl();
        ResetDanger();
        DangerLimit();
    }

    void SetDangerLvl()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        string dangerText = null;

        for (int i = 0; i < sceneCount; i++)
        {
            if (Scenes[i] != currentScene)
            {
                if (Time.time - setTimes[i] > 5)
                {
                if( DangerLvls[i] != 3) {
                    DangerLvls[i] = DangerLvls[i] + 1;
                    dangerText = Scenes[i] + " has a danger level of " + DangerLvls[i] + "!";
                    Debug.Log(Scenes[i] + " has a danger level of " + DangerLvls[i] + "!");
                    setText.AddLine(dangerText);
                    setTimes[i] = Time.time;
                    }
                }
            }
        }
    }

     void GetCurrentSceneDangerLvl()
    {
    int sceneCount = SceneManager.sceneCountInBuildSettings;
       for (int i = 0; i < sceneCount; i++)
        { 
         if (Scenes[i] == currentScene)
            {
               currentSceneDangerLvl =  DangerLvls[i];
            }
        }
    }

    void SetTimer()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;

        for (int i = 0; i < sceneCount; i++)
        {
            if (Scenes[i] == currentScene)
            {
                setTimes[i] = Time.time;
            }
        }
    }
    void ResetDanger() {

    int sceneCount = SceneManager.sceneCountInBuildSettings;
    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
     string dangerText = null;

       
        if (enemies.Length == 0)
        {
     for (int i = 0; i < sceneCount; i++)
        { 
         if (Scenes[i] == currentScene && !ranOnce)
            {
              ranOnce = true;
               DangerLvls[i] = 0;
               limitTimer = 0f;
               criticalDanger = false;
               dangerText = Scenes[i] + " is ALL CLEAR!";
               setText.AddLine(dangerText);
               break;
            }
        }
       }
    }

  void DangerLimit() {
    int sceneCount = SceneManager.sceneCountInBuildSettings;
    
    if (!criticalDanger) {
        bool foundCritical = false;

       
        for (int i = 0; i < sceneCount; i++) {
            if (DangerLvls[i] == 3) {
                foundCritical = true; 
                criticalDanger = true;
                break;
            }
        }

        
        if (!foundCritical) {
            limitTimer = 0;
        }
    }
    else {
        
        limitTimer += Time.deltaTime;
    }

    if (limitTimer > 20f) {
       Timer.remainingTime = 0f;
    }
}

  
    
}
