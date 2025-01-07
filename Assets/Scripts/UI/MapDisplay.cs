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

        // Get the RectTransform of the canvas (used for positioning relative to the canvas)
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();

        // Create a RectTransform for the new image object
        RectTransform trans = imgObject.AddComponent<RectTransform>();
        trans.transform.SetParent(canvas.transform); // setting parent
        trans.localScale = Vector3.one;

     // Set the anchor to the top-left corner
       
        trans.anchorMin = new Vector2(0, 1); // Anchored to the top-left corner
        trans.anchorMax = new Vector2(0, 1); // Anchored to the top-left corner
        trans.pivot = new Vector2(0, 1); // Set the pivot to the top-left corner

        // The position will now be relative to the canvas, not the screen size
        trans.anchoredPosition = spawnOffset;

        // Set the size of the image (you can adjust this based on your needs)
        trans.sizeDelta = new Vector2(225, 125); // custom size

        // Add Image component and assign the sprite
        imgObject.AddComponent<Image>();
        Image image = imgObject.GetComponent<Image>();
        image.sprite = mapSprite;

        imgObject.transform.SetParent(canvas.transform);
      } 



}
