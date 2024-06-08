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
        float h = canvas.GetComponent<RectTransform>().rect.height;
        float w = canvas.GetComponent<RectTransform>().rect.width;

        spawnOffset = new Vector2(-0.40f*w, 0.38f*h); // offset from the top left corner

          RectTransform trans = imgObject.AddComponent<RectTransform>();
          trans.transform.SetParent(canvas.transform); // setting parent
          trans.localScale = Vector3.one;
          trans.anchorMin = new Vector2(0, 1);
          trans.anchorMax = new Vector2(0, 1);
         trans.localPosition = spawnOffset; 

          trans.sizeDelta = new Vector2(225, 125); // custom size
          imgObject.AddComponent<Image>();
          Image image = imgObject.GetComponent<Image>();
          image.sprite = mapSprite;
          imgObject.transform.SetParent(canvas.transform);
      } 



}
