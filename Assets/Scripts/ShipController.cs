using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    private float LRMove;
    private float BFMove;
    private float DUmove;
    private Rigidbody rb;
    [SerializeField] private float MoveSpeed;
    private float _lookX;
    private float _mouseY;
    private float _controllerY;
    private float _xRot;
    private float _yRot;
    [SerializeField] private float lookSensitivity;
    [SerializeField] private Transform cam;
    [SerializeField] private Transform cameraOrbit;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        LRMove = Input.GetAxis("Horizontal");
        BFMove = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.LeftShift))
        {
            DUmove = 1;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            DUmove = -1;
        }
        else
        {
            DUmove = 0;
        }
        _mouseY = Input.GetAxis("Mouse X") * Time.deltaTime * lookSensitivity * 1000;
       // _controllerY = Input.GetAxis("Controller X") * Time.deltaTime * lookSensitivity * 1000;
        if (Math.Abs(_mouseY) > Math.Abs(_controllerY))
        {
            _yRot += _mouseY;
        }
        else
        {
            _yRot += _controllerY;
        }

        cameraOrbit.rotation = Quaternion.Euler(0, _yRot, 0);
    }

    private void FixedUpdate()
    {
        rb.AddForce(LRMove*MoveSpeed,DUmove*MoveSpeed,BFMove*MoveSpeed);
    }
}
