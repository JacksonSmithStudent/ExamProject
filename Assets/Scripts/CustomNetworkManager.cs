using Mirror;
using UnityEngine;

public class CustomNetworkManager : NetworkManager
{
    public Transform[] spawnPoints;

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject player = Instantiate(playerPrefab, spawn.position, spawn.rotation);
        NetworkServer.AddPlayerForConnection(conn, player);
    }
}
