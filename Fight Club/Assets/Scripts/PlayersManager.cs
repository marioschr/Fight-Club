using Cinemachine;
using Photon.Pun;
using UnityEngine;

public class PlayersManager : MonoBehaviour // Στο script αυτό προσθέτουμε τους 2 παίχτες στο target group που σημαδεύει η camera
{
    private CinemachineTargetGroup ctg;
    public bool both = false;
    private GameObject[] players;
    void Start()
    {
        ctg = GetComponent<CinemachineTargetGroup>();
    }

    void Update()
    {
        if (!both)
        {
            players = GameObject.FindGameObjectsWithTag("Player");
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
