using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class Player_ID : NetworkBehaviour {
//	[SyncVar(hook = "SetName")] private string userNameLocal = "";
	[SyncVar] public string userNameLocal;
	//private NetworkInstanceId playerNetID;
	//private Transform myTransform;

	[SerializeField] 
	private Text namePlate;
	[SerializeField] 
	private GameObject namePlateCanvas;

	private NameManager nameManager;
   bool canEditName = true;
	public void Start(){
       
        if (isLocalPlayer) {
            canEditName = GetName.userNameCanChange;
            CmdTellServer(canEditName);
            GetNetIdentity ();
           
            SetIdentity ();
			namePlateCanvas.SetActive (false);
         
        }
	}

	// Update is called once per frame
	void Update () {
		if (namePlate.text == "" || namePlate.text == "BiggieCheese") {
			SetIdentity ();
		}
	}

	[Client]
	void GetNetIdentity(){
		CmdTellServerMyIdentity (MakeUniqueIdentity ());
	}

	void SetIdentity(){

		if (!isLocalPlayer) {
			namePlate.text = userNameLocal;
			gameObject.name = namePlate.text;

		} else {
			namePlate.text = MakeUniqueIdentity();
			gameObject.name = namePlate.text;
		}
		if (isServer) {
			if (nameManager == null) {
				nameManager = GameObject.FindGameObjectWithTag ("NameManager").GetComponent<NameManager> ();
			}
			nameManager.addPlayer (namePlate.text, (int)gameObject.GetComponent<NetworkIdentity> ().connectionToClient.connectionId, gameObject.GetComponent<Player_ID>(), canEditName);
		}
	}

	string MakeUniqueIdentity(){
		string uniqueName = GetName.userName;
		return uniqueName;
	}
	[Command]
	void CmdTellServerMyIdentity(string name){
		userNameLocal = name;

	}
    [Command]
    void CmdTellServer(bool yes)
    {
        canEditName = yes;

    }
    public void adjustName(string newName){
		namePlate.text = newName;
		gameObject.name = newName;
		RpcUpdateMe (newName);
	}
	[ClientRpc]
	void RpcUpdateMe(string newName){
		namePlate.text = newName;
		gameObject.name = newName;
		if (isLocalPlayer) {
			GetName.userName = newName;
            GetName.userNameCanChange = false;
        }
	}
}

