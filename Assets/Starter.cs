using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror.Websocket;
public class Starter : MonoBehaviour {
	private GameObject hoster;
	public Animator needName;
	[SerializeField] WindowManager windowManager;
	[SerializeField] LoginWindow modeSelect;
	[SerializeField] Animator title;
	[SerializeField] Animator player;
	[SerializeField] MapInfo mapInfo;
	private static int[] ports = { 7777, 8888, 5555 };
	private static string[] sceneNames = { "Geometric", "LostLab", "IcicleCaverns" };
	private static string[] mapNames = { "THEMOON", "LOSTLAB", "ICICLECAVERNS" };
	// Use this for initialization
	void Start () {
		//hoster = GameObject.Find("CustomNetworkManager").GetComponent<HostGame>();
	}
	public void startGame(int num){
		hoster = GameObject.Find("CustomNetworkManager");
        if(hoster == null)
        {
			return;
        }
		mapInfo.toggleMap(mapNames[num]);
		hoster.GetComponent<CustomNetworkManager>().onlineScene = sceneNames[num];
		hoster.GetComponent<WebsocketTransport>().port = ports[num];
		hoster.GetComponent<HostGame>().startGame ();
	}
	public void needsName()
	{
		if (GetName.userName == "Player" || GetName.userName == "")
		{
			needName.SetBool("need", true);
        }
        else
        {
			windowManager.turnOn();
			modeSelect.turnOnWindow();
			player.SetInteger("playerNum", -1);
            player.CrossFade("PlayerMenuClear", 0f);
			title.SetBool("hide", true);
		}
	}
}
