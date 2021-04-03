using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace com.SikkimeStudios.FightClub
{ 
    public class Manager : MonoBehaviourPunCallbacks
    {
        public static Manager Instance = null;
        public Text InfoText;
        public string[] player_prefabs;
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
            }
            else
            {
                Spawn(1);
            }
        }

        [PunRPC]
        public void Spawn(int index)
        {
            GameObject player = PhotonNetwork.Instantiate(player_prefabs[PlayerPrefs.GetInt("playerPrefab", 0)], spawn_points[index].position, spawn_points[index].rotation);
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
        
        private IEnumerator EndOfGame(string winner)
        {
            float timer = 5.0f;

            while (timer > 0.0f)
            {
                InfoText.text = string.Format("{0} won because the opponent left the match.\n\n\nReturning to login screen in {1} seconds.", winner, timer.ToString("n2"));

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
            StartCoroutine(EndOfGame(GameObject.FindGameObjectWithTag("Player").GetPhotonView().Owner.NickName));
        }

        private void OnCountdownTimerIsExpired()
        {
            StartGame();
        }

        void StartGame()
        {
            Debug.Log("Start Game Method");
        }
        
        public void LeaveGame() {
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadSceneAsync(0);
        }
    }
}