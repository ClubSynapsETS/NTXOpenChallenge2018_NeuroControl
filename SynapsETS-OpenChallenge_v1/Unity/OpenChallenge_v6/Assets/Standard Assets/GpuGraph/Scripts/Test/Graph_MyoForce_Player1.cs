﻿// © 2016 BRANISLV GRUJIC ALL RIGHTS RESERVED
// Provided AS IS
// For any official support, please use the contact on the unity asset store

using System.Collections;

public class Graph_MyoForce_Player1: MonoBehaviour

    public float EMG_value = 0;


    // Update is called once per frame
    void Update()

        if(EMG_value == 0)
            EMG_value = Random.Range(0.01f, 0.2f); // Keep Graph Active

        //Debug.Log("EMG_value:" + EMG_value);
            //GraphManager.Graph.Plot("Test_WorldSpace", currentDeltaTime, Color.green, new GraphManager.Matrix4x4Wrapper(transform.position, transform.rotation, transform.localScale));
            GraphManager_MyoForce_Player1.Graph.Plot("Test_ScreenSpace", EMG_value, Color.green, GraphRect);