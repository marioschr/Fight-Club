using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightersCenter : MonoBehaviour
{
    private bool both = false;
    private GameObject[] players;

    // Update is called once per frame
    void Update()
    {
        if (!both)
        {
            players = GameObject.FindGameObjectsWithTag("Player");
            if (players.Length == 2)
            {
                both = true;
            }
            else return;
        }

        foreach (GameObject player in players)
        {
            if (player == null)
            {
                both = false;
                return;
            }
        }
        transform.position = Vector3.Lerp(players[0].transform.position, players[1].transform.position, 0.5f);
        Vector3 delta = players[0].transform.position - players[1].transform.position;
        Quaternion look = Quaternion.LookRotation(delta);
        float horizontal = look.eulerAngles.y;
        transform.rotation = Quaternion.AngleAxis(horizontal, Vector3.up);
    }
}
