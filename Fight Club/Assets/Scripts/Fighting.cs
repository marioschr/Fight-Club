using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Fighting : MonoBehaviourPunCallbacks
{
    private Health health; 
    private Animator animator;
    public Attack[] attackMoves;
    private static readonly int Left = Animator.StringToHash("Left");
    private static readonly int Right = Animator.StringToHash("Right");
    private static readonly int Left1 = Animator.StringToHash("Left1");
    private static readonly int Right1 = Animator.StringToHash("Right1");
    private static readonly int Block = Animator.StringToHash("Block");

    private WaitForSeconds regenTick = new WaitForSeconds(0.2f);
    private Coroutine regen;
    private static readonly int Attacking = Animator.StringToHash("Attacking");

    void Start()
    {
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) return;
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && !Input.GetKey(KeyCode.LeftShift) && !animator.GetBool(Attacking))
        {
            animator.SetBool(Block, false);
            Debug.Log("Left from fighting");
            photonView.RPC("Attack", RpcTarget.All, 0);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && !Input.GetKey(KeyCode.LeftShift) && !animator.GetBool(Attacking))
        {
            animator.SetBool(Block, false);
            Debug.Log("Right from fighting");
            photonView.RPC("Attack", RpcTarget.All, 1);
            return;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl) && !animator.GetBool(Attacking))
        {
            Debug.Log("Started Blocking from fighting");
            animator.SetBool(Block, true);
            return;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl) && !animator.GetBool(Attacking) && animator.GetBool(Block))
        {
            Debug.Log("Stopped Blocking from fighting");
            animator.SetBool(Block, false);
            return;
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Mouse0) && !animator.GetBool(Attacking))
        {
            animator.SetBool(Block, false);
            Debug.Log("Left Kick from fighting");
            photonView.RPC("Attack", RpcTarget.All, 2);
            return;
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Mouse1) && !animator.GetBool(Attacking))
        {
            animator.SetBool(Block, false);
            Debug.Log("Right Kick from fighting");
            photonView.RPC("Attack", RpcTarget.All, 3);
            return;
        }
    }

    [PunRPC]
    void Attack(int attackID)
    {
        animator.SetBool(Block, false);
        animator.SetTrigger(attackMoves[attackID].trigger);
        health.currentStamina -= attackMoves[attackID].staminaDrain;
        Debug.Log("Current Stamina:" + health.currentStamina);
        if (regen != null)
        {
            StopCoroutine(regen);
        }
        regen = StartCoroutine(RegenerateStamina());
    }

    IEnumerator RegenerateStamina()
    {
        yield return new WaitForSeconds(1.5f);
        while (health.currentStamina < health.maxStamina)
        {
            health.currentStamina += health.maxHealth / 100;
            yield return regenTick;
        }
        regen = null;
    }
}
