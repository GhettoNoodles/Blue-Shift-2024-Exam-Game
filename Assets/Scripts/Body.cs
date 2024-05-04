using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    [SerializeField] private float mass;
    [SerializeField] private float radius;
    [SerializeField] private Vector3 initialVelocity;
    private Vector3 _currentVelocity;

    private void Start()
    {
        _currentVelocity = initialVelocity;
    }

    public void UpdateVelocity(Body[] allBodies, float timeStep)
    {
        foreach (var otherBody in allBodies)
        {
            if (otherBody != this)
            {
                float distSqrd = Mathf.Pow(Vector3.Distance(otherBody.transform.position, transform.position),2);
                Vector3 forceDir = (otherBody.GetComponent<Rigidbody>().position - GetComponent<Rigidbody>().position).normalized;
                Vector3 force = forceDir * Universe.Instance.gravitationalConst * mass * otherBody.mass / distSqrd;//Newton's Law of Gravitation formula
                Vector3 acceleration = force / mass;
                _currentVelocity += acceleration * timeStep;
            }
        }
    }
    public void UpdatePosition(float timeStep)
    {
        GetComponent<Rigidbody>().position += _currentVelocity * timeStep;
    }
}