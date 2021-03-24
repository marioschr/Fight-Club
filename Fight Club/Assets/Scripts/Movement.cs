using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Animations;

public class Movement : MonoBehaviourPunCallbacks
{
    public float speed;
    private Rigidbody rig;
    private Animator animator;
    private static readonly int X = Animator.StringToHash("X");
    private static readonly int Y = Animator.StringToHash("Y");

    void Start()
    {
        rig = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!photonView.IsMine) return;
        float t_hmove = Input.GetAxis("Horizontal");
        float t_vmove = Input.GetAxis("Vertical");
        animator.SetFloat(X,-t_hmove);
        animator.SetFloat(Y,-t_vmove);
        Vector3 t_direction = new Vector3(-t_hmove, 0, -t_vmove);
        t_direction = t_direction.normalized * speed * Time.deltaTime;
        rig.MovePosition(transform.position + t_direction);
    }
}
