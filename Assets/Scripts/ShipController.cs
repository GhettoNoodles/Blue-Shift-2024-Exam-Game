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
    }

    private void FixedUpdate()
    {
        rb.AddForce(LRMove*MoveSpeed,DUmove*MoveSpeed,BFMove*MoveSpeed);
    }
}
