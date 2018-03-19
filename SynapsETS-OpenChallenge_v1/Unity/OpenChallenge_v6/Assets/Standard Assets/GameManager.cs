using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {


    public static float Player1_eeg1 = 0;
    public static float Player1_eeg2 = 0;
    public static float Player1_eeg3 = 0;
    public static float Player1_eeg4 = 0;

    public static float Player2_eeg1 = 0;
    public static float Player2_eeg2 = 0;
    public static float Player2_eeg3 = 0;
    public static float Player2_eeg4 = 0;


    public static float Player1_RP_Alpha_1 = 0;
    public static float Player1_RP_Alpha_2 = 0;
    public static float Player1_RP_Alpha_3 = 0;
    public static float Player1_RP_Alpha_4 = 0;

    public static float Player2_RP_Alpha_1 = 0;
    public static float Player2_RP_Alpha_2 = 0;
    public static float Player2_RP_Alpha_3 = 0;
    public static float Player2_RP_Alpha_4 = 0;



    // Use this for initialization
    void Start () {


        foreach (Button btn in Resources.FindObjectsOfTypeAll<Button>())
        {
            string functionName = btn.name;

            //Debug.Log(functionName);

            btn.onClick.AddListener(() => Invoke(functionName, 0));
        }

        ActivateAllButtons();

        DeactivateButton("Player1_EEG_CH1");
        DeactivateButton("Player1_EEG_RP_Alpha");
        DeactivateButton("Player1_EMG_GripStrength");
        DeactivateButton("Player2_Muse_2016");
        DeactivateeButtonsEvents("Player1");



        DeactivateButton("Player2_EEG_CH1");
        DeactivateButton("Player2_EEG_RP_Alpha");
        DeactivateButton("Player2_EMG_GripStrength");
        DeactivateButton("Player2_Muse_2016");
        DeactivateeButtonsEvents("Player2");



        Player1_Event_CloseHand = GameObject.Find("Player1_Event_CloseHand");
        MyoForceData_Player1 = GameObject.Find("MyoForceData_Player1");

        Player2_Event_CloseHand = GameObject.Find("Player2_Event_CloseHand");
        MyoForceData_Player2 = GameObject.Find("MyoForceData_Player2");


        MuseEEGData_Player1 = GameObject.Find("MuseEEGData_Player1");
        MuseEEGData_Player2 = GameObject.Find("MuseEEGData_Player2");

        MuseAlphaData_Player1 = GameObject.Find("MuseAlphaData_Player1");
        MuseAlphaData_Player2 = GameObject.Find("MuseAlphaData_Player2");
    }


    GameObject Player1_Event_CloseHand;
    GameObject MyoForceData_Player1;

    GameObject Player2_Event_CloseHand;
    GameObject MyoForceData_Player2;

    GameObject MuseEEGData_Player1;
    GameObject MuseEEGData_Player2;

    GameObject MuseAlphaData_Player1;
    GameObject MuseAlphaData_Player2;


    string Player1__EMGstate = "hand_opened";
    string Player2__EMGstate = "hand_opened";


    float ArduinoUpdate_Time = 0;




    void RoboticHand_updateState()
    {

        string hand_state = "hand_opened";

        if (Player1__EMGstate == "hand_closed")
            hand_state = "hand_closed";

        if (Player2__EMGstate == "hand_closed")
            hand_state = "hand_closed";


        if(Time.time - ArduinoUpdate_Time > 2f)
        {
            ArduinoUpdate_Time = Time.time;

            if (hand_state == "hand_closed")
                GetComponent<ArduinoSend>().CloseHand();

            if (hand_state == "hand_opened")
                GetComponent<ArduinoSend>().OpenHand();
        }


    }



    void Update()
    {

        if(Player1_Event_CloseHand)
        {
            float EMG_value = MyoForceData_Player1.GetComponent<Graph_MyoForce_Player1>().EMG_value;
            EMG_value = Mathf.Floor(EMG_value * 100);
            Player1_Event_CloseHand.transform.GetChild(0).GetComponent<Text>().text = "Grip " + EMG_value + " %";

            if (EMG_value > 50)
            {
                Player1_Event_CloseHand.GetComponent<Button>().interactable = true;
                Player1__EMGstate = "hand_closed";
            }
                
            else
            {
                Player1_Event_CloseHand.GetComponent<Button>().interactable = false;
                Player1__EMGstate = "hand_opened";
            }
               
        }

        
        if (Player2_Event_CloseHand)
        {
            float EMG_value = MyoForceData_Player2.GetComponent<Graph_MyoForce_Player2>().EMG_value;
            EMG_value = Mathf.Floor(EMG_value * 100);
            Player2_Event_CloseHand.transform.GetChild(0).GetComponent<Text>().text = "Grip " + EMG_value + " %";

            if (EMG_value > 50)
            {
                Player2_Event_CloseHand.GetComponent<Button>().interactable = true;
                Player2__EMGstate = "hand_closed";
            }

            else
            {
                Player2_Event_CloseHand.GetComponent<Button>().interactable = false;
                Player2__EMGstate = "hand_opened";
            }
        }

        MuseEEGData_Player1.GetComponent<Graph_MuseEEG_Player1>().EEG_value = Player1_eeg1;
        MuseEEGData_Player2.GetComponent<Graph_MuseEEG_Player2>().EEG_value = Player2_eeg1;

        MuseAlphaData_Player1.GetComponent<Graph_MuseAlpha_Player1>().EEG_value = Player1_RP_Alpha_1;
        MuseAlphaData_Player2.GetComponent<Graph_MuseAlpha_Player2>().EEG_value = Player2_RP_Alpha_1;





        RoboticHand_updateState();

    }


    // Use this for initialization
    void ActivateAllButtons()
    {
        foreach (Button btn in Resources.FindObjectsOfTypeAll<Button>())
        {
            if (!btn.name.Contains("Indicator"))
                btn.interactable = true;
        }

    }

    // Use this for initialization
    void DeactivateButton(string name)
    {
        foreach (Button btn in Resources.FindObjectsOfTypeAll<Button>())
        {
            if (btn.name == name)
                btn.interactable = false;
        }

    }


    void ActivateButtonsEEGRaw(string player)
    {
        foreach (Button btn in Resources.FindObjectsOfTypeAll<Button>())
        {
            if (btn.name.Contains(player) && btn.name.Contains("EEG_CH"))
                btn.interactable = true;
        }
    }

    void ActivateButtonsEEGRelative(string player)
    {
        foreach (Button btn in Resources.FindObjectsOfTypeAll<Button>())
        {
            if (btn.name.Contains(player) && btn.name.Contains("EEG_RP"))
                btn.interactable = true;
        }
    }


    void DeactivateeButtonsEvents(string player)
    {
        foreach (Button btn in Resources.FindObjectsOfTypeAll<Button>())
        {
            if (btn.name.Contains(player) && btn.name.Contains("Event"))
                btn.interactable = false;
        }
    }




    ///////////////////////////////////////
    // PLAYER 1
    ///////////////////////////////////////

    void Player1_EEG_CH1()
    {
        Debug.Log("Player1_EEG_CH1");

        ActivateButtonsEEGRaw("Player1");
        DeactivateButton("Player1_EEG_CH1");
    }
    void Player1_EEG_CH2()
    {
        Debug.Log("Player1_EEG_CH2");

        ActivateButtonsEEGRaw("Player1");
        DeactivateButton("Player1_EEG_CH2");
    }
    void Player1_EEG_CH3()
    {
        Debug.Log("Player1_EEG_CH3");

        ActivateButtonsEEGRaw("Player1");
        DeactivateButton("Player1_EEG_CH3");
    }
    void Player1_EEG_CH4()
    {
        Debug.Log("Player1_EEG_CH4");

        ActivateButtonsEEGRaw("Player1");
        DeactivateButton("Player1_EEG_CH4");
    }
    void Player1_EEG_CH5()
    {
        Debug.Log("Player1_EEG_CH5");

        ActivateButtonsEEGRaw("Player1");
        DeactivateButton("Player1_EEG_CH5");
    }
    void Player1_EEG_CH6()
    {
        Debug.Log("Player1_EEG_CH6");

        ActivateButtonsEEGRaw("Player1");
        DeactivateButton("Player1_EEG_CH6");
    }
    void Player1_EEG_CH7()
    {
        Debug.Log("Player1_EEG_CH7");

        ActivateButtonsEEGRaw("Player1");
        DeactivateButton("Player1_EEG_CH7");
    }



    void Player1_EEG_RP_Delta()
    {
        Debug.Log("Player1_EEG_RP_Delta");

        ActivateButtonsEEGRelative("Player1");
        DeactivateButton("Player1_EEG_RP_Delta");
    }
    void Player1_EEG_RP_Theta()
    {
        Debug.Log("Player1_EEG_RP_Theta");

        ActivateButtonsEEGRelative("Player1");
        DeactivateButton("Player1_EEG_RP_Theta");
    }
    void Player1_EEG_RP_Alpha()
    {
        Debug.Log("Player1_EEG_RP_Alpha");

        ActivateButtonsEEGRelative("Player1");
        DeactivateButton("Player1_EEG_RP_Alpha");
    }
    void Player1_EEG_RP_Beta()
    {
        Debug.Log("Player1_EEG_RP_Beta");

        ActivateButtonsEEGRelative("Player1");
        DeactivateButton("Player1_EEG_RP_Beta");
    }
    void Player1_EEG_RP_Gamma()
    {
        Debug.Log("Player1_EEG_RP_Gamma");

        ActivateButtonsEEGRelative("Player1");
        DeactivateButton("Player1_EEG_RP_Gamma");
    }




    ///////////////////////////////////////
    // PLAYER 2
    ///////////////////////////////////////

    void Player2_EEG_CH1()
    {
        Debug.Log("Player2_EEG_CH1");

        ActivateButtonsEEGRaw("Player2");
        DeactivateButton("Player2_EEG_CH1");
    }
    void Player2_EEG_CH2()
    {
        Debug.Log("Player2_EEG_CH2");

        ActivateButtonsEEGRaw("Player2");
        DeactivateButton("Player2_EEG_CH2");
    }
    void Player2_EEG_CH3()
    {
        Debug.Log("Player2_EEG_CH3");

        ActivateButtonsEEGRaw("Player2");
        DeactivateButton("Player2_EEG_CH3");
    }
    void Player2_EEG_CH4()
    {
        Debug.Log("Player2_EEG_CH4");

        ActivateButtonsEEGRaw("Player2");
        DeactivateButton("Player2_EEG_CH4");
    }
    void Player2_EEG_CH5()
    {
        Debug.Log("Player2_EEG_CH5");

        ActivateButtonsEEGRaw("Player2");
        DeactivateButton("Player2_EEG_CH5");
    }
    void Player2_EEG_CH6()
    {
        Debug.Log("Player2_EEG_CH6");

        ActivateButtonsEEGRaw("Player2");
        DeactivateButton("Player2_EEG_CH6");
    }
    void Player2_EEG_CH7()
    {
        Debug.Log("Player2_EEG_CH7");

        ActivateButtonsEEGRaw("Player2");
        DeactivateButton("Player2_EEG_CH7");
    }



    void Player2_EEG_RP_Delta()
    {
        Debug.Log("Player2_EEG_RP_Delta");

        ActivateButtonsEEGRelative("Player2");
        DeactivateButton("Player2_EEG_RP_Delta");
    }
    void Player2_EEG_RP_Theta()
    {
        Debug.Log("Player2_EEG_RP_Theta");

        ActivateButtonsEEGRelative("Player2");
        DeactivateButton("Player2_EEG_RP_Theta");
    }
    void Player2_EEG_RP_Alpha()
    {
        Debug.Log("Player2_EEG_RP_Alpha");

        ActivateButtonsEEGRelative("Player2");
        DeactivateButton("Player2_EEG_RP_Alpha");
    }
    void Player2_EEG_RP_Beta()
    {
        Debug.Log("Player2_EEG_RP_Beta");

        ActivateButtonsEEGRelative("Player2");
        DeactivateButton("Player2_EEG_RP_Beta");
    }
    void Player2_EEG_RP_Gamma()
    {
        Debug.Log("Player2_EEG_RP_Gamma");

        ActivateButtonsEEGRelative("Player2");
        DeactivateButton("Player2_EEG_RP_Gamma");
    }





}
