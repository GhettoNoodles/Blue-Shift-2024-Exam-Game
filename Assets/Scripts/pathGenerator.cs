using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class pathGenerator : MonoBehaviour
{
    [SerializeField] private GameObject waypoint_prefab;
    [SerializeField] private bool recordPath;
    [SerializeField] private ShipController ship;
    [SerializeField] private LineRenderer lr;
    public List<GameObject> waypoints = new List<GameObject>();

    private void Start()
    {
        if (recordPath)
        {
            DisposeWaypoints();
        }
        else
        {
            Drawline();
        }
    }

    private void Drawline()
    {
        //var drawPoints = new Vector3[waypoints.list];
    }
    private void DisposeWaypoints()
    {
        foreach (var wp in waypoints)
        {
            Destroy(wp);
        }
        waypoints.Clear();
    }
    // Update is called once per frame
    void Update()
    {
        if (recordPath)
        {
           var waypoint = Instantiate(waypoint_prefab, ship.rb.position, Quaternion.identity);
            waypoints.Add(waypoint);
        }
        
        
    }
}
