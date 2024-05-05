using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ShipController : MonoBehaviour
{
    private float LRMove;
    private float BFMove;
    private float DUmove;
    private Rigidbody rb;
    [SerializeField] private float thrust;
    [SerializeField] private float turningThrust;
    private float _lookX;
    private float _mouseY;
    private float _xRot;
    private float _yRot;
    [SerializeField] private float lookSensitivity;
    [SerializeField] private Transform cam;
    [SerializeField] private Transform cameraOrbit;
    [SerializeField] private ParticleSystem partL;
    [SerializeField] private ParticleSystem partR;
    [SerializeField] private ParticleSystem partB;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        LRMove = Input.GetAxis("Horizontal");
        if (LRMove < -0.1f)
        {
            if (!partR.isPlaying)
            {
                partR.Play();
            }

            if (partL.isPlaying)
            {
                partL.Stop();
            }
        }
        else if (LRMove > 0.1f)
        {
            if (!partL.isPlaying)
            {
                
                partL.Play();
               
            }

            if (partR.isPlaying)
            {
                partR.Stop();
            }
        }
        else
        {
            if (partR.isPlaying)
            {
                partR.Stop();
            }

            if (partL.isPlaying)
            {
                partL.Stop();
            }
        }

        if (BFMove>0.1f)
        {
            if (partB.isStopped)
            {
                partB.gameObject.SetActive(true);
                partB.Play();
            }
        }
        else
        {
            if (partB.isPlaying)
            {
                partB.Stop();
                
            }
        }

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
        _yRot += _mouseY;
        _lookX = Input.GetAxis("Mouse Y") * Time.deltaTime * lookSensitivity * 1000;
        _xRot -= _lookX;
        _xRot = Mathf.Clamp(_xRot, -30, 30);
        _yRot = Mathf.Clamp(_yRot, -60, 60);
        cameraOrbit.localRotation = Quaternion.Euler(_xRot, _yRot, 0);
    }

    private void FixedUpdate()
    {
        rb.AddRelativeForce(0f, DUmove * thrust, BFMove * thrust);
        rb.AddRelativeTorque(0f, LRMove * turningThrust, 0f);
    }
}