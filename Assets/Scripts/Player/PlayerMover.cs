using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerMover : NetworkBehaviour
{
    public float SpeedMove;
    public float SpeedRotate;
    public GameObject CameraFollow;

    CharacterController _characterController;
    TakeChair _takeChair;


    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _takeChair = GetComponent<TakeChair>();
    }

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            Destroy(this);
            Destroy(CameraFollow);
        }
    }

    private void Update()
    {
        //if (IsOwner)
            PlayerInput();
    }

    private void PlayerInput()
    {
        if (!_takeChair.IsSeat)
        {
            if (Input.GetKey(KeyCode.W))
            {
                Move(-Vector3.forward * SpeedMove * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.S))
            {
                Move(-Vector3.back * SpeedMove * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.A))
            {
                Move(-Vector3.left * SpeedMove * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D))
            {
                Move(-Vector3.right * SpeedMove * Time.deltaTime);
            }
        }

        var xMouse = Input.GetAxis("Mouse X");
        var yMouse = Input.GetAxis("Mouse Y");

        if (xMouse != 0)
            Rotate(new Vector3(0, xMouse * SpeedRotate, 0));
        if (yMouse != 0)
            Rotate(new Vector3(-yMouse * SpeedRotate, 0, 0));
    }

    public void Move(Vector3 vector)
    {
        _characterController.Move(_characterController.transform.TransformDirection(vector));
    }

    public void Rotate(Vector3 vector)
    {
        transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles + Vector3.up * vector.y);
        CameraFollow.transform.localRotation = Quaternion.Euler(CameraFollow.transform.localRotation.eulerAngles + Vector3.right * vector.x);
    }
}
