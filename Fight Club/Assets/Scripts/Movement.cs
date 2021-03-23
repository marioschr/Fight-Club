using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Animations;

public class Movement : MonoBehaviourPunCallbacks
{
    private CharacterController charController;
    public float movement_Speed = 3f;
    public float gravity = 9.8f;
    public float rotation_Speed = 0.15f;
    public float rotateDegreesPerSecond = 180f;
    void Awake()
    {
        charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) return;
        Move();
    }

    void Move()
    {
        if (Input.GetAxis(Axis.HORIZONTAL_AXIS) < 0)
        {
            Vector3 moveDirection = transform.forward;
            moveDirection.y -= gravity * Time.deltaTime;
            charController.Move(moveDirection * movement_Speed * Time.deltaTime);
        } else if (Input.GetAxis(Axis.HORIZONTAL_AXIS) > 0)
        {
            Vector3 moveDirection = -transform.forward;
            moveDirection.y -= gravity * Time.deltaTime;
            charController.Move(moveDirection * movement_Speed * Time.deltaTime);
        }
    }
}
