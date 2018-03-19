using UnityEngine;
using System.Collections;
 
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
 
public class ArduinoSend : MonoBehaviour
{

    // Adaptation Neuroforce (Begin)

    bool message_send = false;

    public void CloseHand()
    {
        string strMessage = "1";
        sendString(strMessage);
    }

    public void OpenHand()
    {
        string strMessage = "0";
        sendString(strMessage);
    }



    private static int localPort;

   // prefs
   private string IP;  // define in init
   public int port;  // define in init

   // "connection" things
   IPEndPoint remoteEndPoint;
   UdpClient client;

   // gui
   string strMessage="";



   // start from unity3d
   public void Start()
   {
       init();
   }


        // init
    public void init()
    {
        IP="127.0.0.1";
        port=8053;
       
        remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), port);
        client = new UdpClient();
       
    }
 

    private void sendString(string message)
    {
        StartCoroutine("sendString_with_delay", message);
    }


    public IEnumerator sendString_with_delay(string message)
    {

        Debug.Log("sendString_with_delay: " + message);
        yield return new WaitForSeconds(0.15f);

        try
        {
            //if (message != "")
            //{

            // Daten mit der UTF8-Kodierung in das Binärformat kodieren.
            byte[] data = Encoding.UTF8.GetBytes(message);

            // Den message zum Remote-Client senden.
            client.Send(data, data.Length, remoteEndPoint);
            //}
        }
        catch (Exception err)
        {
            print(err.ToString());
        }

        yield return new WaitForSeconds(0.15f);
    }



   
}
 
 