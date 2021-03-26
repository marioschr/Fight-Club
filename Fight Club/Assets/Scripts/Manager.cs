using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine;

namespace com.SikkimeStudios.FightClub
{ 
    public class Manager : MonoBehaviourPunCallbacks
    {
        public string player_prefab;
        public Transform[] spawn_points;
        public CinemachineTargetGroup ctg;
        public void Start()
        { 
            Spawn();
            ctg = GetComponent<CinemachineTargetGroup>();
        }

        public void Spawn()
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                GameObject player1 = PhotonNetwork.Instantiate(player_prefab, spawn_points[0].position, spawn_points[0].rotation);
                ctg.AddMember(player1.transform, 1f, 0f);
            } else if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                GameObject player2 = PhotonNetwork.Instantiate(player_prefab, spawn_points[1].position, spawn_points[1].rotation);
                ctg.AddMember(player2.transform, 1f, 0f);
            }

        }

        private void Update()
        {
            
        }
    }
}