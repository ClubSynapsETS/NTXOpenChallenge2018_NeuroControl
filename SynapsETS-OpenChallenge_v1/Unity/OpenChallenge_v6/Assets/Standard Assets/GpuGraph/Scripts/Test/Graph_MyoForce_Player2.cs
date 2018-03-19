﻿// © 2016 BRANISLV GRUJIC ALL RIGHTS RESERVED
// Provided AS IS
// For any official support, please use the contact on the unity asset store

using System.Collections;

public class Graph_MyoForce_Player2: MonoBehaviour

    public float EMG_value = 0;

    // Use this for initialization
    void Start()

        // calculate current time in ms
        //float currentDeltaTime = Time.deltaTime * 1000.0f;


        if (EMG_value == 0)
            EMG_value = Random.Range(0.01f, 0.2f); // Keep Graph Active

        //Debug.Log("EMG_value:" + EMG_value);

        if (GraphManager_MyoForce_Player2.Graph != null)
            //GraphManager.Graph.Plot("Test_WorldSpace", currentDeltaTime, Color.green, new GraphManager.Matrix4x4Wrapper(transform.position, transform.rotation, transform.localScale));
            GraphManager_MyoForce_Player2.Graph.Plot("Test_ScreenSpace", EMG_value, Color.green, GraphRect);