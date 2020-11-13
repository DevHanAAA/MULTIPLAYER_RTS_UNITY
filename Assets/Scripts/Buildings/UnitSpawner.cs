using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitSpawner : NetworkBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject unitPrefab = null; //Reference to thing to spawn
    [SerializeField] private Transform unitSpawnPoint = null; // under UnitSpawner

    #region Server

    [Command]
    private void CmdSpawnUnit()
    {
        // Spawn by Server
        GameObject unitInstance = Instantiate(
            unitPrefab,
            unitSpawnPoint.position,
            unitSpawnPoint.rotation);

        NetworkServer.Spawn(unitInstance, connectionToClient); // Spawn by Client & Give Ownership
    }

    #endregion

    #region Client

    // Unity will Call this Event Function whenever client clicks Game Object
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) { return; }

        if (!hasAuthority) { return; }

        CmdSpawnUnit(); // Call Command that's originally in server code
    }

    #endregion
}

