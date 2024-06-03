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
    public Rigidbody rb;

    [Header("Requirements")] [SerializeField]
    private ParticleSystem particle_Fwd_Left;

    [SerializeField] private ParticleSystem particle_Fwd_mid;
    [SerializeField] private ParticleSystem particle_Fwd_right;
    [SerializeField] private ParticleSystem particle_back_left;
    [SerializeField] private ParticleSystem particle_back_right;
    [SerializeField] private ParticleSystem particle_Left;
    [SerializeField] private ParticleSystem particle_Right;
    [SerializeField] private ParticleSystem particle_Up;
    [SerializeField] private ParticleSystem particle_Down;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            other.GetComponent<Checkpoint>().ClearCP();
        }
    }

    private void Update()
    {
        Inputs();
        vis.RenderArc(rb.velocity, rb.position, rb.mass);
    }

    private void MatchVelocity()
    {
        var force = (-1 * rb.velocity.normalized * thrust / matchForce);
        rb.AddForce(force);
        force = force.normalized;
        force = transform.InverseTransformDirection(force);
        UI.Instance.UpdateHud(force.z, force.x, force.y);
        ParticleEffects(force.z,force.x,force.y);
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
            if (rb.velocity.magnitude > 1f)
            {
                MatchVelocity();
            }
            else
            {
                UI.Instance.UpdateHud(0, 0, 0);
            }
        }
        else
        
        {
            if (particles)
            {
                ParticleEffects(BFMove, LRMove, DUmove);
            }
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

    

    private void ParticleEffects(float _BFMove, float _LRMove, float _DUMove)
    {
        if (_BFMove > 0.1f)
        {
            particle_Fwd_Left.Play();
            particle_Fwd_mid.Play();
            particle_Fwd_right.Play();
            particle_back_left.Stop();
            particle_back_right.Stop();
        }
        else if (_BFMove < -0.1f)
        {
            particle_back_left.Play();
            particle_back_right.Play();
            particle_Fwd_Left.Stop();
            particle_Fwd_mid.Stop();
            particle_Fwd_right.Stop();
        }
        else
        {
            particle_back_left.Stop();
            particle_back_right.Stop();
            particle_Fwd_Left.Stop();
            particle_Fwd_mid.Stop();
            particle_Fwd_right.Stop();
        }

        if (_LRMove > 0.1f)
        {
            particle_Right.Play();
            particle_Left.Stop();
        }
        else if (_LRMove < -0.1f)
        {
            particle_Left.Play();
            particle_Right.Stop();
        }
        else
        {
            particle_Left.Stop();
            particle_Right.Stop();
        }

        if (_DUMove > 0.1f)
        {
            particle_Up.Play();
            particle_Down.Stop();
        }
        else if (_DUMove < -0.1f)
        {
            particle_Down.Play();
            particle_Up.Stop();
        }
        else
        {
            particle_Up.Stop();
            particle_Down.Stop();
        }
    }
}