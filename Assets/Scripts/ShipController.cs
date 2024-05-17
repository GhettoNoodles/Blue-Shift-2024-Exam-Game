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
    [Header("Requirements")] [SerializeField]
    private ParticleSystem partL;
    [SerializeField] private ParticleSystem partR;
    [SerializeField] private ParticleSystem partB;
    [SerializeField] private GameObject dirInd;
    [Header("Settings")] [SerializeField] private bool particles;
    [SerializeField] private bool roll_allowed;
    [SerializeField] private bool vertical;
    [SerializeField] private float thrust;
    [SerializeField] private float turningThrust;
    [SerializeField] private float matchForce;
    [SerializeField] private Vis vis;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.None;
        if (!roll_allowed)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationZ;
        }

        if (!vertical)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX;
        }
    }

    private void Update()
    {
        Inputs();
        if (particles)
        {
            ParticleEffects(BFMove, horTurn);
        }

        
        updateDir();
        vis.RenderArc(rb.velocity, rb.position, rb.mass);
    }

    private void MatchVelocity()
    {
        var force = (-1 * rb.velocity.normalized * thrust / matchForce);
        rb.AddForce(force);
        force = force.normalized;
        force = transform.InverseTransformDirection(force);
        UI.Instance.UpdateHud(force.z,force.x,force.y);
    }

    private void FixedUpdate()
    {
        rb.AddRelativeForce(LRMove * thrust, DUmove * thrust, BFMove * thrust);
        UI.Instance.UpdateVelocity(rb.velocity.magnitude);
        rb.AddRelativeTorque(verTurn * turningThrust, horTurn * turningThrust, roll * turningThrust);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Planet"))
        {
            UI.Instance.Restart();
        }
    }

    private void Inputs()
    {
        if (roll_allowed)
        {
            var rRoll = Input.GetAxis("rRoll");
            var lRoll = Input.GetAxis("lRoll");
            roll = lRoll - rRoll;
        }
        if (Input.GetButton("X"))
        {
            if (rb.velocity.magnitude>1f)
            {
                MatchVelocity(); 
            }
            else
            {
                UI.Instance.UpdateHud(0,0,0);
            }
            
        }
        else
        {
            horTurn = Input.GetAxis("Right Stick Horizontal");
            verTurn = Input.GetAxis("Right Stick Vertical");
            BFMove = Input.GetAxis("Vertical");
            LRMove = Input.GetAxis("Horizontal");
            if (vertical)
            {
                var up = Input.GetAxis("up");
                var down = Input.GetAxis("down");
                DUmove = up - down;
            }
            UI.Instance.UpdateHud(BFMove, LRMove, DUmove);
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