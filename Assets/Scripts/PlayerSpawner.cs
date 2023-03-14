using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    public GameObject[] spawnPoints = new GameObject[4];
    private Vector3 currentSpawnpoint;
    [SerializeField] private int connectedNUM;

    // Start is called before the first frame update
    void Start()
    {
        NetworkManager.Singleton.OnServerStarted += OnServerStarted;
    }

    private void OnServerStarted()
    {
        connectedNUM = 0;
        if (NetworkManager.Singleton.IsServer)
        {
            if (NetworkManager.Singleton.IsHost)
            {
                currentSpawnpoint = spawnPoints[connectedNUM].transform.position;
                GameObject go = Instantiate(playerPrefab, currentSpawnpoint, Quaternion.identity);
                NetworkObject no = go.GetComponent<NetworkObject>();
                no.SpawnAsPlayerObject(NetworkManager.Singleton.LocalClientId);
                connectedNUM += 1;
                if (connectedNUM > 3)
                {
                    connectedNUM = 0;
                }
            }
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnectedCallback;
            NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnectedCallback;
        }
    }

    private void OnClientConnectedCallback(ulong clientID)
    {
        if (NetworkManager.Singleton.IsServer)
        {
            currentSpawnpoint = spawnPoints[connectedNUM].transform.position;
            GameObject go = Instantiate(playerPrefab, currentSpawnpoint, Quaternion.identity);
            NetworkObject no = go.GetComponent<NetworkObject>();
            no.SpawnAsPlayerObject(clientID);
            connectedNUM += 1;
            if (connectedNUM > 3)
            {
                connectedNUM = 0;
            }
        }
    }

    private void OnClientDisconnectedCallback(ulong clientID)
    {
        connectedNUM -= 1;

    }
}
