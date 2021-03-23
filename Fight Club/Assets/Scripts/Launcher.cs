using UnityEngine;
using Photon.Pun;

namespace com.SikkimeStudios.FightClub
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        public void Awake()
        { 
            Debug.Log("Awake");
            PhotonNetwork.AutomaticallySyncScene = true;
            Connect();
        }
    
        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected!");
            Join();
            base.OnConnectedToMaster();
        }
    
        public override void OnJoinedRoom()
        {
            Debug.Log("Joined Room");
            StartGame();
            base.OnJoinedRoom();
        }
    
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("Joined Room Failed");
            Create();
            base.OnJoinRandomFailed(returnCode, message);
        }
    
        public void Connect()
        {
            Debug.Log("Connect");
            //PhotonNetwork.GameVersion = "0.0.0";
            PhotonNetwork.ConnectUsingSettings();
        }
    
        public void Join()
        {
            Debug.Log("Join Random Room");
            PhotonNetwork.JoinRandomRoom();
        }
    
        public void Create()
        {
            Debug.Log("Create Room");
            PhotonNetwork.CreateRoom("");
        }
        public void StartGame()
        {
            Debug.Log("Start");
            if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                Debug.Log("Load Level");
                PhotonNetwork.LoadLevel(1);
            }
        }
    }
}