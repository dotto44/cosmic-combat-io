using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;


public class LagScript : MonoBehaviour {
	private Text texter;
    [SerializeField] Text average;
    private NetworkClient client;
    private List<int> averageOverTime = new List<int>();
	// Use this for initialization
	void Start () {
		//texter = GetComponent<Text> ();
       
	//	client = GameObject.Find("CustomNetworkManager").GetComponent<NetworkManager>().client;
	}



	// Update is called once per frame
	void Update () {
       
			/*texter.text = "" + client.GetRTT() + "ms";
        if(averageOverTime.Count < 1001)
        {
            averageOverTime.Add(client.GetRTT());
        }
      if(averageOverTime.Count == 999)
        {
            int avg = 0;
            for (int i = 0; i < averageOverTime.Count; i++)
            {
                avg += averageOverTime[i];
            }
            avg /= averageOverTime.Count;
            average.text = "" + avg + "ms";
        }

       */

    }
}
