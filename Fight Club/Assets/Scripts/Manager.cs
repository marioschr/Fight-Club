using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine;

namespace com.SikkimeStudios.FightClub
{ 
    public class Manager : MonoBehaviour
    {
        public string player_prefab;
        public Transform spawn_point;

        public void Start()
        { 
            Spawn();
        }

        public void Spawn()
        {
            PhotonNetwork.Instantiate(player_prefab, spawn_point.position, spawn_point.rotation);
        }
    }
}