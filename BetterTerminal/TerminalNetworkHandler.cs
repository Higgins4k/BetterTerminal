using Unity.Netcode;
using System.Collections.Generic;
using UnityEngine;

public class TerminalNetworkHandler : NetworkBehaviour
{
    public static TerminalNetworkHandler Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void SyncOrderedItems(List<int> orderedItems)
    {
        if (IsServer)
        {
            SyncOrderedItemsClientRpc(orderedItems);
        }
        else
        {
            SyncOrderedItemsServerRpc(orderedItems);
        }
    }

    [ClientRpc]
    private void SyncOrderedItemsClientRpc(List<int> orderedItems)
    {
        Terminal terminalInstance = UnityEngine.GameObject.FindObjectOfType<Terminal>();
        if (terminalInstance != null)
        {
            terminalInstance.orderedItemsFromTerminal = orderedItems;
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void SyncOrderedItemsServerRpc(List<int> orderedItems)
    {
        SyncOrderedItemsClientRpc(orderedItems);
    }
}
