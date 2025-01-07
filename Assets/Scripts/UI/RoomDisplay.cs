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
    public RoomsInfo roomsInfo;
    public Image roomIcon;



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


        float h = canvas.GetComponent<RectTransform>().rect.height;
        float w = canvas.GetComponent<RectTransform>().rect.width;

        spawnOffset = new Vector2(-0.375f * w, 0.38f * h); // offset from the top left corner

        //set up the starting room
        Update_Map(2, 2, "Room B2");
        RoomData roomToAdd = (RoomData)ScriptableObject.CreateInstance("RoomData");
        roomToAdd.Init(2, 2, "Room B2", true, roomIcon);
        roomsInfo.Rooms.Add(roomToAdd);
    }

    public void Update_Map(int roomCol, int roomRow, string roomName)
    {
        
        GameObject imgObject = new GameObject(roomName);
        RectTransform trans = imgObject.AddComponent<RectTransform>();
        trans.transform.SetParent(canvas.transform); // setting parent
        trans.localScale = Vector3.one;
        trans.anchorMin = new Vector2(0, 1);
        trans.anchorMax = new Vector2(0, 1);
        trans.pivot = new Vector2(0, 1); // Set the pivot to the top-left corner

        trans.localPosition = spawnOffset; // Use anchoredPosition to position the RectTransform


        trans.sizeDelta = new Vector2(10, 10); // setting size
        imgObject.AddComponent<Image>();
        Image image = imgObject.GetComponent<Image>();
        image.sprite = roomSprite;
        imgObject.transform.SetParent(canvas.transform);
        imgObject.transform.position += new Vector3(60 * (roomCol - 2), -40 * (roomRow - 2), 0); //setting new room icon position

    }





}
