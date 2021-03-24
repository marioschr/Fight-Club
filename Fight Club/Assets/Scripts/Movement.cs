using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Animations;

public class Movement : MonoBehaviourPunCallbacks
{
    public Transform opponent = null;
    private Transform camera;
    private Rigidbody rig;
    //private CharacterController controller;
    private Animator animator;
    private static readonly int X = Animator.StringToHash("X");
    private static readonly int Z = Animator.StringToHash("Z");
    public float speed;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private bool foundOpponent = false;

    void Start()
    {
        camera = GameObject.FindWithTag("MainCamera").transform;
        rig = GetComponent<Rigidbody>();
        //controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        FindOpponent();
    }

    private void Update()
    { 
        if (!foundOpponent) FindOpponent();
        if (!photonView.IsMine) return;
        transform.LookAt(opponent);
        float t_hmove = Input.GetAxis("Horizontal");
        float t_vmove = Input.GetAxis("Vertical");

        Animating(t_hmove,t_vmove);
        Vector3 direction = new Vector3(t_hmove, 0f, t_vmove).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward; 
            rig.MovePosition(transform.position + moveDir.normalized * (speed * Time.deltaTime));
        }
    }

    private bool FindOpponent()
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
                    return true;
                }
            }
        }
        return false;
    }
    
    private Vector3 animDirection = Vector3.zero;
    void Animating(float h, float v)
    {
        animDirection = new Vector3(h, 0, v);
 
        if (animDirection.magnitude > 1.0f)
        {
            animDirection = animDirection.normalized;
        }
 
        animDirection = transform.InverseTransformDirection(animDirection);
 
        animator.SetFloat(X, -animDirection.x, 0.05f, Time.deltaTime);
        animator.SetFloat(Z, animDirection.z, 0.05f, Time.deltaTime);
    }
}
