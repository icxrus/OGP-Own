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
    [SerializeField] private GameObject spawnUI;
    private ulong clientID;

    // Start is called before the first frame update
    void Start()
    {
        spawnUI.SetActive(false);
        NetworkManager.Singleton.OnServerStarted += OnServerStarted;
    }

    private void OnServerStarted()
    {
        //connectedNUM = 0;
        if (NetworkManager.Singleton.IsServer)
        {
            if (NetworkManager.Singleton.IsHost)
            {
                /*currentSpawnpoint = spawnPoints[connectedNUM].transform.position;
                GameObject go = Instantiate(playerPrefab, currentSpawnpoint, Quaternion.identity);
                NetworkObject no = go.GetComponent<NetworkObject>();
                no.SpawnAsPlayerObject(NetworkManager.Singleton.LocalClientId);
                connectedNUM += 1;
                if (connectedNUM > 3)
                {
                    connectedNUM = 0;
                }*/

                spawnUI.SetActive(true);

            }
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnectedCallback;
            //NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnectedCallback;
        }
    }

    private void OnClientConnectedCallback(ulong clientIDLocal)
    {
        if (NetworkManager.Singleton.IsServer)
        {
            spawnUI.SetActive(true);
            clientID = clientIDLocal; //save clientID for usage in the spawner

            /*currentSpawnpoint = spawnPoints[connectedNUM].transform.position;
            GameObject go = Instantiate(playerPrefab, currentSpawnpoint, Quaternion.identity);
            NetworkObject no = go.GetComponent<NetworkObject>();
            no.SpawnAsPlayerObject(clientID);
            connectedNUM += 1;
            if (connectedNUM > 3)
            {
                connectedNUM = 0;
            }*/
        }
    }

    /*private void OnClientDisconnectedCallback(ulong clientID)
    {
        connectedNUM -= 1;

    }*/

    //Setup spawnpoint with buttons
    public void SetSpawnPoint1()
    {
        GameObject go;
        currentSpawnpoint = spawnPoints[0].transform.position;
        if (NetworkManager.Singleton.IsHost)
            go = SpawnPlayerHost();
        else
            go = SpawnPlayerClient();

        spawnUI.SetActive(false);
        GameObject goNew = new GameObject("1");
        goNew.transform.parent = go.transform;
    }
    public void SetSpawnPoint2()
    {
        GameObject go;
        currentSpawnpoint = spawnPoints[1].transform.position;
        if (NetworkManager.Singleton.IsHost)
            go = SpawnPlayerHost();
        else
            go = SpawnPlayerClient();

        spawnUI.SetActive(false);
        GameObject goNew = new GameObject("2");
        goNew.transform.parent = go.transform;
    }
    public void SetSpawnPoint3()
    {
        GameObject go;
        currentSpawnpoint = spawnPoints[2].transform.position;
        if (NetworkManager.Singleton.IsHost)
            go = SpawnPlayerHost();
        else
            go = SpawnPlayerClient();

        spawnUI.SetActive(false);
        GameObject goNew = new GameObject("3");
        goNew.transform.parent = go.transform;
    }
    public void SetSpawnPoint4()
    {
        GameObject go;
        currentSpawnpoint = spawnPoints[3].transform.position;
        if (NetworkManager.Singleton.IsHost)
            go = SpawnPlayerHost();
        else
            go = SpawnPlayerClient();

        spawnUI.SetActive(false);
        GameObject goNew = new GameObject("4");
        goNew.transform.parent = go.transform;
    }

    private GameObject SpawnPlayerHost() //Host spawning
    {
        GameObject go = Instantiate(playerPrefab, currentSpawnpoint, Quaternion.identity);
        NetworkObject no = go.GetComponent<NetworkObject>();
        no.SpawnAsPlayerObject(NetworkManager.Singleton.LocalClientId);

        return go;
    }

    private GameObject SpawnPlayerClient() //Client spawn with saved clientID variable
    {
        GameObject go = Instantiate(playerPrefab, currentSpawnpoint, Quaternion.identity);
        NetworkObject no = go.GetComponent<NetworkObject>();
        no.SpawnAsPlayerObject(clientID);

        return go;
    }

}
