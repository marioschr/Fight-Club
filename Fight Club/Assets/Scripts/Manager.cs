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

namespace com.SikkimeStudios.FightClub
{ 
    public class Manager : MonoBehaviourPunCallbacks
    {
        public string player_prefab;
        public Transform[] spawn_points;
        public GameObject loading;
        public CinemachineTargetGroup targetGroup;
        private GameObject[] players;
        private bool both = false;
        public void Start()
        { 
            if(PhotonNetwork.IsMasterClient)
            {
                Debug.Log("master client");
                Spawn(0);
                //photonView.RPC("Spawn", RpcTarget.All, 0);
            }
            else
            {
                Debug.Log("non master client");
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
            if (GameObject.FindGameObjectsWithTag("Player").Length == 2)
            {
                both = true;
                loading.SetActive(false);
            }
        }
    }
}