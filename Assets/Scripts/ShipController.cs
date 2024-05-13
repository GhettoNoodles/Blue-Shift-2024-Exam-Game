using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ShipController : MonoBehaviour
{
    private float LRMove;
    private float horTurn;
    private float verTurn;
    private float roll;
    private float BFMove;
    private float DUmove;
    private Rigidbody rb;
    [SerializeField] private bool particles;
    [SerializeField] private float thrust;
    [SerializeField] private float turningThrust;
    private float _lookX;
    private float _mouseY;
    private float _xRot;
    private float _yRot;

    [SerializeField] private GameObject dirInd;
    [SerializeField] private float lookSensitivity;
    [SerializeField] private Transform cam;
    [SerializeField] private Transform cameraOrbit;
    [SerializeField] private ParticleSystem partL;
    [SerializeField] private ParticleSystem partR;
    [SerializeField] private ParticleSystem partB;
    [SerializeField] private Vis vis;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        horTurn = Input.GetAxis("Right Stick Horizontal");
        verTurn = Input.GetAxis("Right Stick Vertical");
        BFMove = Input.GetAxis("Vertical");
        LRMove = Input.GetAxis("Horizontal");
        var up = Input.GetAxis("up");
        var down = Input.GetAxis("down");
        var rRoll = Input.GetAxis("rRoll");
        var lRoll = Input.GetAxis("lRoll");
        if (Input.GetButton("X"))
        {
            MatchVelocity();
        }
        if (particles)
        {
            ParticleEffects(BFMove, horTurn);
        }

        DUmove = up - down;
        roll = lRoll - rRoll;
        Debug.Log(roll);
        _mouseY = Input.GetAxis("Mouse X") * Time.deltaTime * lookSensitivity * 1000;
        // _controllerY = Input.GetAxis("Controller X") * Time.deltaTime * lookSensitivity * 1000;
        _yRot += _mouseY;
        _lookX = Input.GetAxis("Mouse Y") * Time.deltaTime * lookSensitivity * 1000;
        _xRot -= _lookX;
        _xRot = Mathf.Clamp(_xRot, -30, 30);
        _yRot = Mathf.Clamp(_yRot, -60, 60);
        //cameraOrbit.localRotation = Quaternion.Euler(_xRot, _yRot, 0);

        UI.Instance.UpdateHud(BFMove, LRMove);
        updateDir();
        vis.RenderArc(rb.velocity, rb.position, rb.mass);
    }

    private void MatchVelocity()
    {
        rb.AddForce(-1*rb.velocity.normalized*thrust/2f);
       // rb.rotation =Quaternion.LookRotation(Vector3.RotateTowards(rb.rotation.eulerAngles,new Vector3(0,rb.rotation.y,0),Time.deltaTime,0.0f));
    }

    private void FixedUpdate()
    {
        rb.AddRelativeForce(LRMove * thrust, DUmove * thrust, BFMove * thrust);
        UI.Instance.UpdateVelocity(rb.velocity.magnitude);
        rb.AddRelativeTorque(verTurn * turningThrust, horTurn * turningThrust, roll*turningThrust);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Planet"))
        {
            UI.Instance.Restart();
        }
    }

    private void updateDir()
    {
        var dir = rb.velocity.normalized;
        var angle = Vector3.SignedAngle(Vector3.forward, dir, Vector3.up);
        /*float xtwist;
        float ztwist;
        if (angle is > 0f and <= 90f)
        {
            xtwist = -10;
            ztwist = -10;
        }
        else if (angle is > 90f and <= 180f)
        {
            xtwist = 10;
            ztwist = -10;
        }else if (angle is <0f  and >= -90f)
        {
            xtwist = -10;
            ztwist = 10;
        }
        else
        {
            xtwist = 10;
            ztwist = 10;
        }*/
        dirInd.transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
    }

    private void ParticleEffects(float _BFMove, float _turn)
    {
        if (_turn < -0.1f)
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
        else if (_turn > 0.1f)
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

        if (_BFMove > 0.1f)
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
    }
}