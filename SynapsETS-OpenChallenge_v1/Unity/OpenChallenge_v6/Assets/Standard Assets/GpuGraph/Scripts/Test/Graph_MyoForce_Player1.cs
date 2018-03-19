﻿// © 2016 BRANISLV GRUJIC ALL RIGHTS RESERVED
// Provided AS IS
// For any official support, please use the contact on the unity asset store

using System.Collections;using System.Collections.Generic;using UnityEngine;

public class Graph_MyoForce_Player1: MonoBehaviour{    private int Count;    private Rect GraphRect;

    public float EMG_value = 0;


    // Update is called once per frame
    void Update()    {        float xSize = 0.3f * Screen.width;        float ySize = 0.1f * Screen.height;        float xPos = 0.2f * Screen.width;        float yPos = 0.5f * Screen.height;        GraphRect = new Rect(xPos, yPos, xSize, ySize);

        if(EMG_value == 0)
            EMG_value = Random.Range(0.01f, 0.2f); // Keep Graph Active

        //Debug.Log("EMG_value:" + EMG_value);        if (GraphManager_MyoForce_Player1.Graph != null)        {
            //GraphManager.Graph.Plot("Test_WorldSpace", currentDeltaTime, Color.green, new GraphManager.Matrix4x4Wrapper(transform.position, transform.rotation, transform.localScale));
            GraphManager_MyoForce_Player1.Graph.Plot("Test_ScreenSpace", EMG_value, Color.green, GraphRect);        }    }}