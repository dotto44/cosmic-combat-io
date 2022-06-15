using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

/*public class PlayerSyncComb : NetworkBehaviour
{
    public class DataPoint
    {
        public bool flip;
        public Vector2 position;
        public float rotation;
        public bool armActive;
    }

    [SyncVar(hook = "SyncPositionValues")]
    //[SyncVar]
    private Vector2 syncPos;

    [SerializeField] Transform myTransform;
    private float lerpRate;
    private float normalLerpRate = 15;
    private float fasterLerpRate = 20;
    // Use this for initialization
    Vector2 oldPos;
    Vector2 oldPosForUse;
    private Vector2 lastPos;
    float progress = 0;
    private float threshold = 0.1f;

    private List<Vector2> syncPosList = new List<Vector2>();
    private NetworkClient nClient;
    private int latency;
    private Text latencyText;
    private float previousTime;
    private bool useHistoricalLerping = false;
    private float closeEnough = 0.11f;

    private float lastTime = 0.0f;
    private float timeForLastUpdate = 1.0f;
    float timeSinceLastUpdateArr = 0.0f;
    private Vector2 pastSyncPos;
    private Vector2 checker;

    float timeExpired = 0;

    float[] recentUpdates = new float[4];
    float percentError = 0.3f;

    public float myPing = 0;
    private List<float> pingStartTime = new List<float>();
    private int isXNumberUpdate = 0;
    bool newPos = false;
    Text myPingText;



    void Start()
    {
        myPingText = GameObject.FindWithTag("Ping").GetComponent<Text>();
        lerpRate = normalLerpRate;

    }
    void FixedUpdate()
    {
        TransmitPosition();
    }
    void Update()
    {
        if (isClient)
        {
        
          //  Debug.Log(NetworkManager.singleton.client.GetRTT());
        }
        LerpPosition();
        //ShowLatency ();
    }
    void LerpPosition()
    {
        if (!isLocalPlayer && !isServer)
        {

            OrdinaryLerping();

        }

        //Debug.Log (Time.deltaTime.ToString ());
    }
    [Command]
    void CmdProvidePositionToServer(Vector2 pos)
    {
        if (syncPos == pos)
        {
            newPos = true;
            RpcUpdatePingWhenNoMovement();
        }
        syncPos = pos;
        myTransform.position = syncPos;
        TargetPing(connectionToClient);
    }
    public override bool OnSerialize(NetworkWriter writer, bool initialState)
    {
       
        writer.Write(myTransform.position.x);
        writer.Write(myTransform.position.y);
        writer.Write(myTransform.rotation.z);
        return true;
    }

    [ClientCallback]
    void TransmitPosition()
    {

        if (isLocalPlayer && Time.time - previousTime > 0.05)
        {

            previousTime = Time.time;
            pingStartTime.Add(Time.time);
            CmdProvidePositionToServer(myTransform.position);

            lastPos = myTransform.position;
        }
    }
    [Client]
    void SyncPositionValues(Vector2 latestPos)
    {

        syncPos = latestPos;
        
    }
    void OrdinaryLerping()
    {

       

        if (newPos || checker != syncPos)
        {
            newPos = false;
            float t;
            t = (Time.time * 1000) - lastTime;
            lastTime = Time.time * 1000;

            recentUpdates[3] = recentUpdates[2];
            recentUpdates[2] = recentUpdates[1];
            recentUpdates[1] = recentUpdates[0];
            recentUpdates[0] = t;
            float count = 0;
            foreach (float f in recentUpdates)
            {
                if (f > 0.1)
                {
                    timeForLastUpdate += f;
                    count++;
                }

            }

            timeForLastUpdate /= count;
            timeForLastUpdate += timeForLastUpdate * percentError;
            timeSinceLastUpdateArr = 0;
            pastSyncPos = myTransform.position;
            checker = syncPos;
            timeExpired = 0;
        }
        if (timeSinceLastUpdateArr < 1)
        {
            timeExpired += Time.deltaTime * 1000;
            timeSinceLastUpdateArr = (timeExpired) / timeForLastUpdate;
        }
       
        myTransform.position = LerpWithoutClamp(pastSyncPos, syncPos, timeSinceLastUpdateArr);

    }
    [TargetRpc]
    public void TargetPing(NetworkConnection target)
    {
        myPing = (Time.time - pingStartTime[0]) * 1000;

        pingStartTime.RemoveAt(0);
        if (isXNumberUpdate == 4)
        {
            isXNumberUpdate = 0;
            myPingText.text = "" + Mathf.Round(myPing);


        }
        else
        {
            isXNumberUpdate++;
        }
    }
    [ClientRpc]
    public void RpcUpdatePingWhenNoMovement()
    {
        lastTime = Time.time * 1000;
        newPos = true;

    }
    Vector2 LerpWithoutClamp(Vector2 A, Vector2 B, float t)
    {
        return A + (B - A) * t;
    }
    public float GetAverageUpdateTimes()
    {
        return timeForLastUpdate;
    }
    void HistoricalLerping()
    {
        if (syncPosList.Count > 0)
        {
            myTransform.position = Vector2.Lerp(myTransform.position, syncPosList[0], Time.deltaTime * lerpRate);

            if (Vector3.Distance(myTransform.position, syncPosList[0]) < closeEnough)
            {
                syncPosList.RemoveAt(0);
            }

            if (syncPosList.Count > 2)
            {
                lerpRate = fasterLerpRate;
            }
            else if (syncPosList.Count > 4)
            {
                lerpRate = fasterLerpRate * 1.5f;
            }
            else
            {
                lerpRate = normalLerpRate * 1.5f;
            }
            Debug.Log(syncPosList.Count);
        }
    }
}*/
