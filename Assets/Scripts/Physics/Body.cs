using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    public float mass;
    [SerializeField] private float radius;
    [SerializeField] private Vector3 initialVelocity;
    [SerializeField] private Collider shipcollider;
    public Rigidbody rb;
    public bool immovable;

    private Collider col;


    private void Start()
    {
        rb.velocity = initialVelocity;
        col = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
    }

    public void UpdateVelocity(Body[] allBodies, float timeStep)
    {
        if (!immovable)
        {
            var closestDist = 2000f;
            foreach (var otherBody in allBodies)
            {
                if (otherBody != this)
                {
                    var dist = Vector3.Distance(otherBody.transform.position, transform.position);
                    float distSqrd = Mathf.Pow(dist, 2);
                    Vector3 forceDir =
                        (otherBody.rb.position - rb.position).normalized;
                    Vector3 force = forceDir * (Universe.Instance.gravitationalConst * mass * otherBody.mass) /
                                    distSqrd;
                    Vector3 acceleration = force / mass;
                    rb.velocity += acceleration * timeStep;
                    if (dist<closestDist)
                    {
                        closestDist = dist;
                    }
                }
            }
            UI.Instance.Vibrate(closestDist);
        }
    }

    public bool Contains(Vector3 pt)
    {
        if (col == null)
        {
            col = shipcollider;
        }
        return col.bounds.Contains(pt);
    }
}