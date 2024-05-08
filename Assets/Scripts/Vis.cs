using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vis : MonoBehaviour
{
    [SerializeField] private int numSteps;
    [SerializeField] private float timestep;
    [SerializeField] private float width;
    [SerializeField] private LineRenderer lr;
    private Body[] bodies;
    private bool end = false;
    private void Start()
    {
        bodies = FindObjectsOfType<Body>();
    }

    public void RenderArc(Vector3 vel,Vector3 pos,float mass)
    {
        end = false;
        var drawpoints = new Vector3[numSteps];
        for (int step = 0; step < numSteps; step++)
        {
            if (!end)
            {
                vel += CalculateAcc(pos, mass) * timestep;
                Vector3 newPos = pos + vel * timestep;
                pos = newPos;
                foreach (var bd in bodies)
                {
                    if (bd.Contains(pos) && bd.immovable)
                    {
                        end = true;
                        break;
                    }
                }
            }

            drawpoints[step] = pos;
        }

        lr.enabled = true;
        lr.positionCount = drawpoints.Length;
        lr.SetPositions(drawpoints);
        lr.startColor = Color.red;
        lr.endColor =Color.red;
        lr.widthMultiplier = width;
    }

    private Vector3 CalculateAcc(Vector3 pos, float mass)
    {
        Vector3 acc = Vector3.zero;
        foreach (var body in bodies)
        {
            if (body.immovable)
            {
                Vector3 forceDir = (body.transform.position - pos).normalized;
                float sqrDist = (body.transform.position - pos).sqrMagnitude;
                acc += forceDir * Universe.Instance.gravitationalConst * body.mass / sqrDist;
            }
        }
        return acc;
    }
}
