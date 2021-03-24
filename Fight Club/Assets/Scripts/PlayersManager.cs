using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Photon.Pun;
using UnityEngine;

public class PlayersManager : MonoBehaviour
{
    private CinemachineTargetGroup ctg;
    public bool both = false;
    void Start()
    {
        ctg = GetComponent<CinemachineTargetGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!both)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            if (players.Length == 2)
            {
                foreach (GameObject player in players)
                {
                    if (!player.GetPhotonView().IsMine)
                    {
                        ctg.AddMember(player.transform, 1f, 0f);
                        both = true;
                    }
                }
            }
        }
    }
}
