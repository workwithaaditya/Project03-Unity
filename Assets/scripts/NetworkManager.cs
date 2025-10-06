using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public string playerPrefabName = "PlayerPrefab";

    void Start()
    {
        // Ensure offline mode is off
        PhotonNetwork.OfflineMode = false;

        // Connect to Photon Cloud
        PhotonNetwork.ConnectUsingSettings();
    }

    // Callback - photon connected to master server
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");

        // Only join lobby if not auto-join
        if (!PhotonNetwork.AutomaticallySyncScene)
            PhotonNetwork.JoinLobby();
        else
            JoinOrCreateRoom();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        JoinOrCreateRoom();
    }

    void JoinOrCreateRoom()
    {
        RoomOptions options = new RoomOptions { MaxPlayers = 10 };
        PhotonNetwork.JoinOrCreateRoom("GameRoom", options, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room: " + PhotonNetwork.CurrentRoom.Name);

        // Spawn player after successfully joining the room
        Vector3 spawnPos = new Vector3(Random.Range(-5f, 5f), 1f, Random.Range(-5f, 5f));
        PhotonNetwork.Instantiate(playerPrefabName, spawnPos, Quaternion.identity);
    }
}
