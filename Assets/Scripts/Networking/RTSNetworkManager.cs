using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class RTSNetworkManager : NetworkManager
{
    [SerializeField] private GameObject unitSpawnerPrefab = null;

    public override void OnServerAddPlayer(NetworkConnection conn) // Add player by server
    {
        base.OnServerAddPlayer(conn);
        //Spawn base on server
        GameObject unitSpawnerInstance = Instantiate( 
            unitSpawnerPrefab, 
            conn.identity.transform.position, 
            conn.identity.transform.rotation);

        NetworkServer.Spawn(unitSpawnerInstance, conn); // Spawn on client & Give ownership
    }
}
