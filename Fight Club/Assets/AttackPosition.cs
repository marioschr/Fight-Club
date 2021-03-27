using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class AttackPosition : MonoBehaviourPunCallbacks
{

    public Attack attack;
    private bool isColliding = false;
    
    // Start is called before the first frame update
    void Start()
    { 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isColliding) return;
        isColliding = true;
        if (other.gameObject.layer == 10 && transform.root.GetComponent<Animator>().GetBool(attack.animBool)) 
        {
            other.gameObject.GetPhotonView().RPC("TakeDamage", RpcTarget.All, attack.power);
            Debug.Log("Attacked opponent with " + attack.attackName + ", dealt " + attack.power + " damage");
        }
        StartCoroutine(Reset());
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(0.3f);
        isColliding = false;
    }


    [PunRPC]
    private void TakeDamage(int p_damage)
    {
        GetComponent<Health>().TakeDamage(p_damage);
    }
}
