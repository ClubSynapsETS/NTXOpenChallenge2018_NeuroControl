private var RemoteIP : String = "127.0.0.1"; //127.0.0.1 signifies a local host (if testing locally
private var SendToPort : int = 9000; //the port you will be sending from (PUT ANY)
public var ListenerPort : int = 8000; //the port you will be listening on
//public var controller : Transform;
private var handler : Osc;

public var Holder: GameObject;

var ParentName : String = "";

public function Start ()
{
	//Initializes on start up to listen for messages
	//make sure this game object has both UDPPackIO and OSC script attached

	var udp : UDPPacketIO = GetComponent("UDPPacketIO");
	udp.init(RemoteIP, SendToPort, ListenerPort);
	handler = GetComponent("Osc");
	handler.init(udp);
	handler.SetAllMessageHandler(AllMessageHandler);


	ParentName = transform.name;

	Debug.Log("ParentName:" + ParentName);
}
Debug.Log("OSCReceiver is running");

function Update () {

}

//These functions are called when messages are received
//Access values via: oscMessage.Values[0], oscMessage.Values[1], etc


public function AllMessageHandler(oscMessage: OscMessage){


	var msgString = Osc.OscMessageToString(oscMessage); //the message and value combined
	//Debug.Log(msgString);


	var msgAddress = oscMessage.Address; //the message parameters

	//FUNCTIONS YOU WANT CALLED WHEN A SPECIFIC MESSAGE IS RECEIVED
	switch (msgAddress){

		
        case "/hello":
            Debug.Log("/hello and value is: " + oscMessage.Values[0]);
            break;


        case "/muse/elements/alpha_relative":

            Debug.Log("/muse/alpha_relative 1 and value is: " + oscMessage.Values[0]);
			Debug.Log("/muse/alpha_relative 2 and value is: " + oscMessage.Values[1]);
            Debug.Log("/muse/alpha_relative 3 and value is: " + oscMessage.Values[2]);
			Debug.Log("/muse/alpha_relative 4 and value is: " + oscMessage.Values[3]);


			if(ParentName == "OSCModule2")
			{
				GameManager.Player2_RP_Alpha_1 = oscMessage.Values[0];
				GameManager.Player2_RP_Alpha_2 = oscMessage.Values[0];
				GameManager.Player2_RP_Alpha_3 = oscMessage.Values[0];
				GameManager.Player2_RP_Alpha_4 = oscMessage.Values[0];
			}
			else
			{
				GameManager.Player1_RP_Alpha_1 = oscMessage.Values[0];
				GameManager.Player1_RP_Alpha_2 = oscMessage.Values[0];
				GameManager.Player1_RP_Alpha_3 = oscMessage.Values[0];
				GameManager.Player1_RP_Alpha_4 = oscMessage.Values[0];
			}


            break;



        case "/muse/eeg":
		/*
			Debug.Log("/muse/eeg 1 and value is: " + oscMessage.Values[0]);
			Debug.Log("/muse/eeg 2 and value is: " + oscMessage.Values[1]);
            Debug.Log("/muse/eeg 3 and value is: " + oscMessage.Values[2]);
			Debug.Log("/muse/eeg 4 and value is: " + oscMessage.Values[3]);
			*/

			

			if(ParentName == "OSCModule2")
			{
				GameManager.Player2_eeg1 = oscMessage.Values[0];
				GameManager.Player2_eeg2 = oscMessage.Values[1];
				GameManager.Player2_eeg3 = oscMessage.Values[2];
				GameManager.Player2_eeg4 = oscMessage.Values[3];
			}
			else
			{
				GameManager.Player1_eeg1 = oscMessage.Values[0];
				GameManager.Player1_eeg2 = oscMessage.Values[1];
				GameManager.Player1_eeg3 = oscMessage.Values[2];
				GameManager.Player1_eeg4 = oscMessage.Values[3];
			}


            break;



        case "/muse/elements/alpha_relative":
            Debug.Log("/muse/elements/alpha_relative and value is: " + oscMessage.Values[0]);
            break;

			


		default:
			break;
	}

}
