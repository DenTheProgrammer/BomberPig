using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using System;

public class MultiplayerManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private byte maxPlayers = 3;
    [SerializeField]
    private GameObject[] playerPrefabs;
    [SerializeField]
    private Transform[] spawnPoints;
    [SerializeField]
    private Image loadingScreen;
    [SerializeField]
    private Image winScreen;
    [SerializeField]
    private Image loseScreen;
    private List<Photon.Realtime.Player> alivePlayers;
    private PhotonView view;

    void Start()
    {
        alivePlayers = new List<Photon.Realtime.Player>();
        //Player.onDeathOfPlayer += OnPlayerDeath;
        loadingScreen.gameObject.SetActive(true);
        PhotonNetwork.ConnectUsingSettings();
        view = GetComponent<PhotonView>();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master...");
        PhotonNetwork.JoinRandomOrCreateRoom();
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.CurrentRoom.MaxPlayers = maxPlayers;
        Debug.Log("You joined the room!");
        alivePlayers.Add(PhotonNetwork.LocalPlayer);
        Debug.Log($"player {PhotonNetwork.LocalPlayer.ToString()} is added to alive");
        SpawnPlayer();
        Debug.Log($"Players in the room: {PhotonNetwork.CurrentRoom.PlayerCount}");
        loadingScreen.gameObject.SetActive(false);
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.Log("Another player joined the room!");
        alivePlayers.Add(newPlayer);
        Debug.Log($"player {newPlayer.ToString()} is added to alive");
        Debug.Log($"Players in the room: {PhotonNetwork.CurrentRoom.PlayerCount}");
    }

    private void SpawnPlayer()
    {
        int playersInTheRoom = PhotonNetwork.CurrentRoom.PlayerCount;
        PhotonNetwork.Instantiate((playerPrefabs[playersInTheRoom - 1]).name,
            spawnPoints[playersInTheRoom - 1].position,
            Quaternion.identity);
    }

  

    public void ShowLoseScreen()
    {
        loseScreen.gameObject.SetActive(true);
    }
    public void ShowWinScreen()
    {
        winScreen.gameObject.SetActive(true);
    }
 
    void Update()
    {
        
    }
}
