using UnityEngine;
using Mirror;
//using UnityEngine.Networking.NetworkSystem;

public class CustomNetworkManager : NetworkManager
{

    private NetworkStartPosition[] spawnPoints;
    private NameManager nameManager;

    [SerializeField] MapInfo mapInfo;
    public override void OnStartServer()
    {
        base.OnStartServer();

        NetworkServer.RegisterHandler<CCPlayerMessage>(OnCreateCharacter);

    }
    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

        if (clientLoadedScene)
        {
            mapInfo.toggleMap(networkSceneName);
        }

        CCPlayerMessage characterMessage = new CCPlayerMessage
        {
            playerType = StatsHolder.characterSelected
        };
        conn.Send(characterMessage);
    }
    public override void OnServerRemovePlayer(NetworkConnection conn, NetworkIdentity player)
    {
        base.OnServerRemovePlayer(conn, player);
        Debug.Log("Removed Player Object");
    }
    public override void OnServerDisconnect(NetworkConnection conn)
    {
       // nameManager = GameObject.FindGameObjectWithTag("NameManager").GetComponent<NameManager>();
        //nameManager.removePlayer(conn.connectionId);
        base.OnServerDisconnect(conn);
    }
    void OnCreateCharacter(NetworkConnection conn, CCPlayerMessage message)
    {

        //Select prefab from registered prefabs
        var playerPrefab = spawnPrefabs[message.playerType];

        // Get spawn point array
        Vector3 spawnPoint = Vector3.zero;
        if (spawnPoints == null)
        {
            spawnPoints = FindObjectsOfType<NetworkStartPosition>();
        }

        // If there is a spawn point array and the array is not empty, pick one at random
        if (spawnPoints != null && spawnPoints.Length > 0)
        {
            spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
        }

        // Set the player’s position to the chosen spawn point

        var player = Instantiate(playerPrefab, spawnPoint, Quaternion.identity) as GameObject;

        //Add player
        NetworkServer.AddPlayerForConnection(conn, player);
    }
    public void ReplacePlayer(GameObject newPrefab, Vector3 position)
    {
        NetworkConnection conn = NetworkClient.connection;

        // Cache a reference to the current player object
        GameObject oldPlayer = conn.identity.gameObject;

        // Instantiate the new player object and broadcast to clients
        NetworkServer.ReplacePlayerForConnection(conn, Instantiate(newPrefab, position, Quaternion.identity) as GameObject);

        // Remove the previous player object that's now been replaced
        NetworkServer.Destroy(oldPlayer);
    }
}
