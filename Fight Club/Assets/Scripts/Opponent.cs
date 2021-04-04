using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Opponent : MonoBehaviourPunCallbacks
{
    public Transform opponent = null;
    private bool foundOpponent = false;
    private void Start()
    {
        FindOpponent();
    }

    private void Update()
    {
        if (!foundOpponent) FindOpponent();
        if (!photonView.IsMine) return;
        transform.LookAt(opponent);
    }

    private void FindOpponent() // βρίσκουμε τον αντίπαλο για να κοιτάμε προς την κατεύθυνση του
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length == 2)
        {
            foreach (GameObject player in players)
            {
                if (!player.GetPhotonView().IsMine)
                {
                    opponent = player.GetPhotonView().transform;
                    foundOpponent = true;
                }
            }
        }
    }
}
