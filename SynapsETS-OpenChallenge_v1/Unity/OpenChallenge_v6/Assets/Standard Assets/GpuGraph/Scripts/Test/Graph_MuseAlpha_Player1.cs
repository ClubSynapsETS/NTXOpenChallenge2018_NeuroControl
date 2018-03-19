// © 2016 BRANISLV GRUJIC ALL RIGHTS RESERVED
// Provided AS IS
// For any official support, please use the contact on the unity asset store

using System.Collections;using System.Collections.Generic;using UnityEngine;

public class Graph_MuseAlpha_Player1: MonoBehaviour{    private int Count;    private Rect GraphRect;

    public float EEG_value = 0;

    // Use this for initialization
    void Start()    {        Count = 0;    }        // Update is called once per frame    void Update()    {        Count++;        float xSize = 0.3f * Screen.width;        float ySize = 0.1f * Screen.height;        float xPos = 0.2f * Screen.width;        float yPos = 0.3f * Screen.height;        GraphRect = new Rect(xPos, yPos, xSize, ySize);

        // calculate current time in ms
        //float currentDeltaTime = Time.deltaTime * 1000.0f;

        if (EEG_value == 0)
            EEG_value = Random.Range(0.01f, 0.2f); // Keep Graph Active

        if (GraphManager_MuseAlpha_Player1.Graph != null)        {
            //GraphManager.Graph.Plot("Test_WorldSpace", currentDeltaTime, Color.green, new GraphManager.Matrix4x4Wrapper(transform.position, transform.rotation, transform.localScale));
            GraphManager_MuseAlpha_Player1.Graph.Plot("Test_ScreenSpace", EEG_value, Color.green, GraphRect);        }    }}