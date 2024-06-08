using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class RoomDisplay : MonoBehaviour
{
    public GameObject canvas;
    public Sprite roomSprite;
    public Vector2 spawnOffset;
    static private RoomDisplay instance = null;



    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
    }

   
   private void Start()
    {
        DontDestroyOnLoad(canvas);
       


        GameObject imgObject = new GameObject("New Room");
        float h = canvas.GetComponent<RectTransform>().rect.height;
        float w = canvas.GetComponent<RectTransform>().rect.width;

        spawnOffset = new Vector2(-0.40f * w, 0.38f * h); // offset from the top left corner

        RectTransform trans = imgObject.AddComponent<RectTransform>();
        trans.transform.SetParent(canvas.transform); // setting parent
        trans.localScale = Vector3.one;
        trans.anchorMin = new Vector2(0, 1);
        trans.anchorMax = new Vector2(0, 1);
        trans.localPosition = spawnOffset; // Use anchoredPosition to position the RectTransform


        trans.sizeDelta = new Vector2(10, 10); // custom size
        imgObject.AddComponent<Image>();
        Image image = imgObject.GetComponent<Image>();
        image.sprite = roomSprite;
        imgObject.transform.SetParent(canvas.transform);
    }

    public void Update_Map(int roomCol, int roomRow)
    {
        
        GameObject imgObject = new GameObject("New Room");
        RectTransform trans = imgObject.AddComponent<RectTransform>();
        trans.transform.SetParent(canvas.transform); // setting parent
        trans.localScale = Vector3.one;
        trans.anchorMin = new Vector2(0, 1);
        trans.anchorMax = new Vector2(0, 1);
        trans.localPosition = spawnOffset; // Use anchoredPosition to position the RectTransform


        trans.sizeDelta = new Vector2(10, 10); // custom size
        imgObject.AddComponent<Image>();
        Image image = imgObject.GetComponent<Image>();
        image.sprite = roomSprite;
        imgObject.transform.SetParent(canvas.transform);
        imgObject.transform.position += new Vector3(60 * (roomCol - 2), 100 * (roomRow - 2), 0); //setting new room icon position

    }





}
