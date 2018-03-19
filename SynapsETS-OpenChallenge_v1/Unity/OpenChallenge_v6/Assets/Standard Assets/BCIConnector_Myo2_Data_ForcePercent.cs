using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BCIConnector_Myo2_Data_ForcePercent : MonoBehaviour {


    GameObject MyoForceData_Player2;

    ThalmicMyo Myo_script;
	bool Myo_Paired = false;
	
	float HistoryTimeSec = 0.5f;
	
	List<int> Myo_ForceHistory_values = new List<int>();
	List<float> Myo_ForceHistory_times = new List<float>();
	
	float Myo_Normal_Low = 999; // (high value here)  
	float Myo_Normal_High = 0; // (low value here)  
	
	// Use this for initialization
	void Start () {
		
		GameObject Myo_go = GameObject.Find("Myo2");
		Myo_script = Myo_go.GetComponent<ThalmicMyo>();

        if (Myo_script) {
  

            if (Myo_script.isPaired){

                Myo_Paired = true;
				Myo_script.Vibrate(Thalmic.Myo.VibrationType.Short);
                Myo_script.Vibrate(Thalmic.Myo.VibrationType.Short);
                Myo_script.Vibrate(Thalmic.Myo.VibrationType.Short);
            }
		}

        MyoForceData_Player2 = GameObject.Find("MyoForceData_Player2");

    }
	
	// Update is called once per frame
	void Update () {
		
		if (Myo_script && Myo_Paired) {
			int Myo_emg_max = 0;
			for (int i = 0; i < Myo_script.emg.Length; i++) {
				int emg_value_abs = Mathf.Abs (Myo_script.emg [i]);
				if (emg_value_abs > Myo_emg_max)
                    Myo_emg_max = emg_value_abs;
			}
			
			Myo_ForceHistory_values.Add (Myo_emg_max);
			Myo_ForceHistory_times.Add (Time.time);
			
			
			//print ("Myo_ForceHistory_times[0]: " + Myo_ForceHistory_times[0]);
			//print ("Myo_ForceHistory_times.Count: " + Myo_ForceHistory_times.Count);
			//print ("Time.time: " + Time.time);
			
			if (Myo_ForceHistory_times [0] + HistoryTimeSec < Time.time) {
				Myo_ForceHistory_values.RemoveAt (0);
				Myo_ForceHistory_times.RemoveAt (0);
				
				
				//print ("Myo_ForceHistory_values.Count: " + Myo_ForceHistory_values.Count);
				
				float Myo_ForceHistory_sum = 0;
				for (int i = 0; i < Myo_ForceHistory_values.Count; i++) {
					Myo_ForceHistory_sum += Myo_ForceHistory_values [i];
				}
				
				float Myo_ForceHistory_median = Myo_ForceHistory_sum / Myo_ForceHistory_values.Count;
				
				//print ("Myo_ForceHistory_median: " + Myo_ForceHistory_median);
				
				
				if (Myo_Normal_Low > Myo_ForceHistory_median)
					Myo_Normal_Low = Myo_ForceHistory_median;
				
				if (Myo_Normal_High < Myo_ForceHistory_median)
					Myo_Normal_High = Myo_ForceHistory_median;
				
				float Myo_Force_Percent = (Myo_ForceHistory_median - Myo_Normal_Low) / (Myo_Normal_High - Myo_Normal_Low);
				
				if (Myo_Force_Percent > 1)
					Myo_Force_Percent = 1;

				// * PUSH VALUE TO BCI SYNC MODULE *
				string DatasLinesToSync = "BCI_Myo_2:ForcePercent:" + Myo_Force_Percent + ":float";
                //  Debug.Log(DatasLinesToSync);

                if (MyoForceData_Player2)
                {
                    MyoForceData_Player2.GetComponent<Graph_MyoForce_Player2>().EMG_value = Myo_Force_Percent;
                }



            } // if (Myo_ForceHistory_times [0] + HistoryTimeSec < Time.time)
		} // if if (Myo_script && Myo_Paired)
		
	} // Update ()
}
