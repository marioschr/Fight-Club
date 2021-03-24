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
        public Transform spawn_point;
        public CinemachineTargetGroup ctg;
        public void Start()
        { 
            Spawn();
            ctg = GetComponent<CinemachineTargetGroup>();
        }

        public void Spawn()
        {
            GameObject player1 = PhotonNetwork.Instantiate(player_prefab, spawn_point.position, spawn_point.rotation);
            ctg.AddMember(player1.transform, 1f, 0f);
        }

        private void Update()
        {
            
        }
    }
}