using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class Fighting : MonoBehaviourPunCallbacks
{
    private Health health; 
    private Animator animator;
    public Attack[] attackMoves;
    private static readonly int Block = Animator.StringToHash("Block");

    private WaitForSeconds regenTick = new WaitForSeconds(0.2f);
    private Coroutine regen;
    private static readonly int Attacking = Animator.StringToHash("Attacking");
    private Image clientStaminaUI;
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
            photonView.RPC("Attack", RpcTarget.All, 0);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && !Input.GetKey(KeyCode.LeftShift) && !animator.GetBool(Attacking))
        {
            animator.SetBool(Block, false);
            photonView.RPC("Attack", RpcTarget.All, 1);
            return;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl) && !animator.GetBool(Attacking))
        {
            animator.SetBool(Block, true);
            return;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl) && !animator.GetBool(Attacking) && animator.GetBool(Block))
        {
            animator.SetBool(Block, false);
            return;
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Mouse0) && !animator.GetBool(Attacking))
        {
            animator.SetBool(Block, false);
            photonView.RPC("Attack", RpcTarget.All, 2);
            return;
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Mouse1) && !animator.GetBool(Attacking))
        {
            animator.SetBool(Block, false);
            photonView.RPC("Attack", RpcTarget.All, 3);
            return;
        }
    }

    [PunRPC]
    void Attack(int attackID)
    {
        if (health.currentStamina >= attackMoves[attackID].staminaDrain)
        {
            animator.SetBool(Block, false);
            animator.SetTrigger(attackMoves[attackID].trigger);
            health.currentStamina -= attackMoves[attackID].staminaDrain;
            clientStaminaUI.fillAmount = health.currentStamina / 100f;
            if (regen != null)
            {
                StopCoroutine(regen);
            }
            regen = StartCoroutine(RegenerateStamina());
        }
    }

    IEnumerator RegenerateStamina()
    {
        yield return new WaitForSeconds(1.5f);
        while (health.currentStamina < health.maxStamina)
        {
            health.currentStamina += health.maxStamina / 100;
            clientStaminaUI.fillAmount = health.currentStamina / 100f;
            yield return regenTick;
        }
        regen = null;
    }
    
    [PunRPC]
    public void SetStaminaUI()
    {
        if (photonView.IsMine)
        {
            clientStaminaUI = GameObject.Find("GUI/Canvas/Right/Stamina/Bar").GetComponent<Image>();
        }
        else
        {
            clientStaminaUI = GameObject.Find("GUI/Canvas/Left/Stamina/Bar").GetComponent<Image>();
        }
    }
}
