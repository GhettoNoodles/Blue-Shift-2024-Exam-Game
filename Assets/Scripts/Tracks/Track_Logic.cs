using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Track_Logic : MonoBehaviour
{
    [SerializeField] private ShipController ship;
    [SerializeField] private LineRenderer lr;
    public float timer;
    [SerializeField] private List<Checkpoint> _checkpoints = new List<Checkpoint>();
    // Start is called before the first frame update
    

    private void ClosestPathPoint()
    {
       // var points = lr.GetPositions();
    }

    private void RemoveClosePathNodes()
    {
       // lr.
    }
}
