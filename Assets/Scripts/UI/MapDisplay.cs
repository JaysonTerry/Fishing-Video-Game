using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapDisplay : MonoBehaviour
{
    public GameObject canvas;
    public Sprite mapSprite;
    public Vector2 spawnOffset;

    static private MapDisplay instance = null;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
    }
    


    void Start()
      {
         DontDestroyOnLoad(gameObject);

        GameObject imgObject = new GameObject("MapBackground");

      
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();

        // Create a RectTransform for the new image object
        RectTransform trans = imgObject.AddComponent<RectTransform>();
        trans.transform.SetParent(canvas.transform); 
        trans.localScale = Vector3.one;

     // Set the anchor to the top-left corner
       
        trans.anchorMin = new Vector2(0, 1);
        trans.anchorMax = new Vector2(0, 1); 
        trans.pivot = new Vector2(0, 1); 

      
        trans.anchoredPosition = spawnOffset;

        // Set the size of the image (you can adjust this based on your needs)
        trans.sizeDelta = new Vector2(120, 75);

        
        imgObject.AddComponent<Image>();
        Image image = imgObject.GetComponent<Image>();
        image.sprite = mapSprite;

        imgObject.transform.SetParent(canvas.transform);
      } 



}
