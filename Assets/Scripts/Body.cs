using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    [SerializeField] private float mass;
    [SerializeField] private float radius;
    [SerializeField] private Vector3 initialVelocity;
    [SerializeField] private Rigidbody rb;
    public bool immovable;
    public Vector3 _currentVelocity;
    private Vector3 largestPull;
    public Vector3 closestPos;
    

    private void Start()
    {
        _currentVelocity = initialVelocity;
       
    }

    public void UpdateVelocity(Body[] allBodies, float timeStep)
    {
        if (!immovable)
        {   largestPull = Vector3.zero;
            foreach (var otherBody in allBodies)
            {
                if (otherBody != this)
                {
                    float distSqrd = Mathf.Pow(Vector3.Distance(otherBody.transform.position, transform.position), 2);
                    Vector3 forceDir =
                        (otherBody.GetComponent<Rigidbody>().position - GetComponent<Rigidbody>().position).normalized;
                    Vector3 force = forceDir * Universe.Instance.gravitationalConst * mass * otherBody.mass /
                                    distSqrd; //Newton's Law of Gravitation formula
                    if (largestPull.magnitude<force.magnitude)
                    {
                        largestPull = force;
                       closestPos = otherBody.transform.position;
                       Debug.Log(otherBody.gameObject.name);
                    }
                    Vector3 acceleration = force / mass;
                    rb.velocity += acceleration * timeStep;
                }
            }
        }
    }

    public void UpdatePosition(float timeStep)
    {
        if (!immovable)
        {
            GetComponent<Rigidbody>().position += _currentVelocity * timeStep;
        }
    }
}