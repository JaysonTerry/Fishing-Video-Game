using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;


public class GameStateController : MonoBehaviour
{
    public static int startingHealth = 5;
    public GameObject[] loadingZones; //stores all current loading zones in scene

    static private GameStateController instance = null;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
        gameObject.tag = "Room Canvas";


        UpdateLoadingZones();

        // Subscribe to the sceneLoaded event to refresh loading zones when a new scene is loaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // Unsubscribe from the sceneLoaded event to prevent memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public event EventHandler OnLoadingZone;



    private void Update()
    {

        for (int i = 0; i < loadingZones.Length; i++)
        {

            SceneTransition sceneTransition = loadingZones[i].GetComponent<SceneTransition>();

            if (sceneTransition.transitioned == true)
            {
                OnLoadingZone?.Invoke(this, EventArgs.Empty);
                break;
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateLoadingZones();
    }

    public void UpdateLoadingZones()
    {
        loadingZones = GameObject.FindGameObjectsWithTag("Loading Zone");

    }
}
