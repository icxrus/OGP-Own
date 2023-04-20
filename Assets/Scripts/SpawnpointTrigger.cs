using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using TMPro;

public class SpawnpointTrigger : MonoBehaviour
{
    public GameObject collectablePrefab;
    GameObject go;
    private int p1Score = 0;
    private int p2Score = 0;
    private int p3Score = 0;
    private int p4Score = 0;
    public TMP_Text scoreText;
    [SerializeField] private GameObject networkManagerObject; 

    private void Start()
    {
        NetworkManager.Singleton.OnServerStarted += RespawnCollectible;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (go != null && go.transform.parent.CompareTag("Player"))
            {
                if (gameObject.CompareTag("1") && other.transform.Find("1"))
                {
                    p1Score += 1;
                    Debug.Log("Player 1 score adjusted by 1");
                }
                else if (gameObject.CompareTag("2") && other.transform.Find("2"))
                {
                    p2Score += 1;
                    Debug.Log("Player 2 score adjusted by 1");
                }
                else if (gameObject.CompareTag("3") && other.transform.Find("3"))
                {
                    p3Score += 1;
                    Debug.Log("Player 3 score adjusted by 1");
                }
                else if (gameObject.CompareTag("4") && other.transform.Find("4"))
                {
                    p4Score += 1;
                    Debug.Log("Player 4 score adjusted by 1");
                }
                GameObject[] array = GameObject.FindGameObjectsWithTag("Collectible");
                foreach (var item in array)
                {
                    item.GetComponent<NetworkObject>().Despawn(destroy: true);
                    Debug.Log("Collectible destroyed.");
                }
                RespawnCollectible();
                UpdateScores();
                networkManagerObject.GetComponent<NetworkCommands>().CheckScoresForWinner(p1Score, p2Score, p3Score, p4Score, scoreText);
            }

            
        }
    }
    
    private void RespawnCollectible()
    {
        go = Instantiate(collectablePrefab, new Vector3(0, 0.5f, 0), Quaternion.identity);
        go.GetComponent<NetworkObject>().Spawn();
        Debug.Log("Collectible respawned.");
    }

    private void UpdateScores()
    {
        scoreText.text = "Player 1 Score: " + p1Score + "<br>Player 2 Score: " + p2Score + "<br>Player 3 Score: " + p3Score + "<br>Player 4 Score: " + p4Score;
        Debug.Log("Player Scores Updated.");
    }

    
}
