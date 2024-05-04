using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodySimulation : MonoBehaviour
{
    private Body[] bodies;

    private void Start()
    {
        bodies = FindObjectsOfType<Body>();
        Time.fixedDeltaTime = Universe.Instance.physicsTimeStep;
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < bodies.Length; i++)
        {
            bodies[i].UpdateVelocity(bodies,Universe.Instance.physicsTimeStep);
        }
        for (int i = 0; i < bodies.Length; i++)
        {
            bodies[i].UpdatePosition(Universe.Instance.physicsTimeStep);
        }
    }
}
