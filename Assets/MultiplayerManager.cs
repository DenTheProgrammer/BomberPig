using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MultiplayerManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private byte maxPlayers = 3;
    [SerializeField]
    private GameObject[] playerPrefabs;
    [SerializeField]
    private Transform[] spawnPoints;
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
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
        SpawnPlayer();
        Debug.Log($"Players in the room: {PhotonNetwork.CurrentRoom.PlayerCount}");
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.Log("Another player joined the room!");
        Debug.Log($"Players in the room: {PhotonNetwork.CurrentRoom.PlayerCount}");
    }

    private void SpawnPlayer()
    {
        int playersInTheRoom = PhotonNetwork.CurrentRoom.PlayerCount;
        PhotonNetwork.Instantiate((playerPrefabs[playersInTheRoom - 1]).name,
            spawnPoints[playersInTheRoom - 1].position,
            Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
