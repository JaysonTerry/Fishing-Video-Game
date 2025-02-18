using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneData : MonoBehaviour
{
public static string[] Scenes;
 public static int[] dangerLvls;
 public static string currentScene;
 public static int currentSceneDangerLvl;
 

    // Update is called once per frame
    void Update()
    {
        Scenes = SetDangerLevel.Scenes;
        currentScene = SetDangerLevel.currentScene;
        dangerLvls = SetDangerLevel.DangerLvls;
        currentSceneDangerLvl = SetDangerLevel.currentSceneDangerLvl;
    }
}
