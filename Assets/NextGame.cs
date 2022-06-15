using TMPro;
using UnityEngine;
using Mirror;
using System.Collections;
public class NextGame : NetworkBehaviour
{
    
    public class ClickedMessage : MessageBase
    {
        public int map;
        public string userName;
    }

    static readonly int numberOfMapOptions = 3;

    public static bool hasAddedMessageHandler = false;

    [SerializeField] TMP_Text title;
    [SerializeField] GameObject holder;
    [SerializeField] GameObject timer;
    [SerializeField] MapOption[] mapOptions;

    MapInfo mapInfo;

    ArrayList playersWhoSent = new ArrayList();

    int[] voteTallies = new int[numberOfMapOptions];
    string[] userLists = new string[numberOfMapOptions];

    public void gameWon(string byWho)
    {
        if (!isServer)
        {
            return;
        }

        if (!hasAddedMessageHandler)
        {
            NetworkServer.RegisterHandler<ClickedMessage>(pickedMap);
        }
        mapInfo = GameObject.FindGameObjectWithTag("SpawnPointManager").GetComponent<MapInfo>();
        string comboString = mapInfo.getNRandomCombos(numberOfMapOptions);
        Debug.Log(comboString);

        RpcEndGameOnAllClient(byWho, comboString);
        holder.SetActive(true);
        timer.SetActive(true);
    }
    [ClientRpc]
    void RpcEndGameOnAllClient(string byWho, string comboString)
    {
        title.text = byWho + " WON";

        updateMapOptions(comboString);

        holder.SetActive(true);
        timer.SetActive(true);
    }
    [ClientRpc]
    void RpcUpdateVotesAndLists(int votes, string userName, int n)
    {
        mapOptions[n].setVote(votes);
        mapOptions[n].setNames(userName);
    }
    public void pickedMap(NetworkConnection conn, ClickedMessage message)
    {
        if (playersWhoSent.Contains(conn.connectionId))
        {
            return;
        }
        playersWhoSent.Add(conn.connectionId);
        voteTallies[message.map]++;
        userLists[message.map] += message.userName + "\n";
        RpcUpdateVotesAndLists(voteTallies[message.map], userLists[message.map], message.map);
    }
    public void clickedMap(int n)
    {
        ClickedMessage msg = new ClickedMessage()
        {
            map = n,
            userName = GetName.userName
        };

        NetworkClient.Send(msg);
    }
    public void updateMapOptions(string comboString)
    {
        string[] combos = comboString.Split(',');
        mapInfo = GameObject.FindGameObjectWithTag("SpawnPointManager").GetComponent<MapInfo>();

        for (int i = 0; i < numberOfMapOptions * 2; i+=2)
        {
            int map = int.Parse(combos[i]);
            int mode = int.Parse(combos[i + 1]);
            
            mapOptions[i / 2].setMap(mapInfo.getMapName(map));
            mapOptions[i / 2].setMode(mapInfo.getMode(mode));
            mapOptions[i / 2].setIcon(mapInfo.getIcon(map));
            mapOptions[i / 2].setBackdrop(mapInfo.getBackdrop(map));
        }
        
    }
}
