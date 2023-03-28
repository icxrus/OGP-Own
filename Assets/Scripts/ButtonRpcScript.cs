using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ButtonRpcScript : NetworkBehaviour
{
    [SerializeField] GameObject playerPrefab;
    public GameObject[] spawnPoints = new GameObject[4];
    private Vector3 currentSpawnpoint;

    [ServerRpc(RequireOwnership = false)]
    public void SpawnClientPlayerServerRpc(ulong clientID, int spawnPosNumber)
    {
        GameObject go;
        currentSpawnpoint = spawnPoints[spawnPosNumber - 1].transform.position;
        go = SpawnPlayerClient(clientID);

        GameObject goNew = new GameObject("" + spawnPosNumber);
        goNew.transform.parent = go.transform;
    }

    private GameObject SpawnPlayerHost() //Host spawning
    {
        GameObject go = Instantiate(playerPrefab, currentSpawnpoint, Quaternion.identity);
        NetworkObject no = go.GetComponent<NetworkObject>();
        no.SpawnAsPlayerObject(NetworkManager.Singleton.LocalClientId);

        return go;
    }

    private GameObject SpawnPlayerClient(ulong clientID) //Client spawn with saved clientID variable
    {

        GameObject go = Instantiate(playerPrefab, currentSpawnpoint, Quaternion.identity);
        NetworkObject no = go.GetComponent<NetworkObject>();
        no.SpawnAsPlayerObject(clientID);

       return go;
       
    }

    private void DontDestroyOnLoad()
    {
        DontDestroyOnLoad();
    }
}
