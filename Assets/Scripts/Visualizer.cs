using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Visualizer : MonoBehaviour
{
    // The length of the arrow, in meters
    private float arrowLength = 1.0F;

    private Rigidbody _rigidbody;
    private Body body;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        body = GetComponent<Body>();
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        var position = transform.position;
        var velocity = _rigidbody.velocity;

        
        if (velocity.magnitude < 0.1f) return;

        Handles.color = Color.red;
        Handles.ArrowHandleCap(0, position, Quaternion.LookRotation(velocity), Mathf.Clamp(velocity.magnitude,1f,50f), EventType.Repaint);
        Handles.color = Color.blue;
        Handles.DrawLine(position,body.closestPos,2);
       // Debug.Log("drawing");
    }
}
