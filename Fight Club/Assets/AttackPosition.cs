using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class AttackPosition : MonoBehaviourPunCallbacks
{

    public Attack attack;
    private bool isColliding = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isColliding) return;
        if (other.gameObject.layer == 10 && transform.root.GetComponent<Animator>().GetBool(attack.animBool)) 
        {
            isColliding = true;
            other.gameObject.GetPhotonView().RPC("TakeDamage", RpcTarget.All, attack.power);
            StartCoroutine(Reset());
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(0.5f);
        isColliding = false;
    }


    [PunRPC]
    private void TakeDamage(int p_damage)
    {
        GetComponent<Health>().TakeDamage(p_damage);
    }
}
