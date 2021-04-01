using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace com.SikkimeStudios.FightClub
{ 
    public class Manager : MonoBehaviourPunCallbacks
    {
        public static Manager Instance = null;
        public Text InfoText;
        public string player_prefab;
        public Transform[] spawn_points;
        public GameObject loading;
        public CinemachineTargetGroup targetGroup;
        private GameObject[] players;
        private bool both = false;

        private void Awake()
        {
            Instance = this;
        }

        public override void OnEnable()
        {
            base.OnEnable();

            CountdownTimer.OnCountdownTimerHasExpired += OnCountdownTimerIsExpired;
        }

        public override void OnDisable()
        {
            base.OnDisable();

            CountdownTimer.OnCountdownTimerHasExpired -= OnCountdownTimerIsExpired;
        }
        public void Start()
        { 
            if(PhotonNetwork.IsMasterClient)
            {
                Spawn(0);
                //photonView.RPC("Spawn", RpcTarget.All, 0);
            }
            else
            {
                Spawn(1);
                //photonView.RPC("Spawn", RpcTarget.All, 1);
            }
        }

        [PunRPC]
        public void Spawn(int index)
        {
            GameObject player = PhotonNetwork.Instantiate(player_prefab, spawn_points[index].position, spawn_points[index].rotation);
            targetGroup.AddMember(player.transform, 1f, 0.3f);
        }

        private void Update()
        {
            if (both) return;
            players = GameObject.FindGameObjectsWithTag("Player");
            int startTimestamp;
            bool startTimeIsSet = CountdownTimer.TryGetStartTime(out startTimestamp);
            if (players.Length == 2)
            {
                both = true;
                loading.SetActive(false);
                foreach (GameObject player in players)
                {
                    player.GetPhotonView().RPC("SetHealthUI", RpcTarget.All);
                    player.GetPhotonView().RPC("SetStaminaUI", RpcTarget.All);
                }
                if (!startTimeIsSet)
                {
                    CountdownTimer.SetStartTime();
                }
            }
        }
        
        [PunRPC]
        void SetHealthUI()
        {
            GetComponent<Health>().SetHealthUI();
        }

        [PunRPC]
        void SetStaminaUI()
        {
            GetComponent<Fighting>().SetStaminaUI();
        }
        
        private IEnumerator EndOfGame(string winner, int score)
        {
            float timer = 5.0f;

            while (timer > 0.0f)
            {
                InfoText.text = string.Format("Player {0} won with {1} points.\n\n\nReturning to login screen in {2} seconds.", winner, score, timer.ToString("n2"));

                yield return new WaitForEndOfFrame();

                timer -= Time.deltaTime;
            }

            PhotonNetwork.LeaveRoom();
        }
        
        public override void OnDisconnected(DisconnectCause cause)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }

        public override void OnLeftRoom()
        {
            PhotonNetwork.Disconnect();
        }
        
        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            CheckEndOfGame();
        }
        
        private void CheckEndOfGame()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                StopAllCoroutines();
            }

            string winner = "";
            int score = -1;

            foreach (Player p in PhotonNetwork.PlayerList)
            {
                if (p.GetScore() > score)
                {
                    winner = p.NickName;
                    score = p.GetScore();
                }
            }

            StartCoroutine(EndOfGame(winner, score));
        }

        private void OnCountdownTimerIsExpired()
        {
            StartGame();
        }

        void StartGame()
        {
            Debug.Log("Start Game Method");
        }
    }
}