using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class NetworkCommands : MonoBehaviour
{
    public Button startButton;
    public Button startHostButton;
    public Button startClientButton;
    public Button disconnectButton;

    private int winningScore = 5;

#if UNITY_SERVER && !UNITY_EDITOR
    private void Start()
    {
        NetworkManager.Singleton.StartServer();
    }
#else
    private void Start()
    {
        disconnectButton.gameObject.SetActive(false);
    }
    public void ConnectToServer()
    {
        NetworkManager.Singleton.StartServer();
        disconnectButton.gameObject.SetActive(true);
        startHostButton.gameObject.SetActive(false);
        startClientButton.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);

    }

    public void StartServerAsHost()
    {
        NetworkManager.Singleton.StartHost();
        disconnectButton.gameObject.SetActive(true);
        startHostButton.gameObject.SetActive(false);
        startClientButton.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
    }

    public void JoinServer()
    {
        NetworkManager.Singleton.StartClient();
        disconnectButton.gameObject.SetActive(true);
        startHostButton.gameObject.SetActive(false);
        startClientButton.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
    }

    public void DisconnectFromServer()
    {
        GetComponent<PlayerSpawner>().StopListener();
        NetworkManager.Singleton.Shutdown();
        disconnectButton.gameObject.SetActive(false);
        startHostButton.gameObject.SetActive(true);
        startClientButton.gameObject.SetActive(true);
        startButton.gameObject.SetActive(true);
    }
#endif

    public void CheckScoresForWinner(int p1Score, int p2Score, int p3Score, int p4Score, TMP_Text scoreText)
    {
        if (p1Score >= winningScore)
        {
            scoreText.text = "Player 1 Wins with a score of: " + p1Score;
            Invoke(nameof(RestoreScene), 3f);
        }
        else if (p2Score >= winningScore)
        {
            scoreText.text = "Player 2 Wins with a score of: " + p2Score;
            Invoke(nameof(RestoreScene), 3f);
        }
        else if (p3Score >= winningScore)
        {
            scoreText.text = "Player 3 Wins with a score of: " + p3Score;
            Invoke(nameof(RestoreScene), 3f);
        }
        else if (p4Score >= winningScore)
        {
            scoreText.text = "Player 4 Wins with a score of: " + p4Score;
            Invoke(nameof(RestoreScene), 3f);
        }
        else
            return;
    }

    private void RestoreScene()
    {
        NetworkManager.Singleton.SceneManager.LoadScene("SampleScene", UnityEngine.SceneManagement.LoadSceneMode.Single);
        NetworkManager.Singleton.Shutdown();
        Destroy(this.gameObject);
    }
}

