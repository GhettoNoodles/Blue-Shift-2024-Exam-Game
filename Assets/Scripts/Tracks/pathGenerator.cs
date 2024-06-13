using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cinemachine;
using UnityEngine;
using Newtonsoft.Json;
using Unity.VisualScripting;

public class IntendedPath
{
    [JsonProperty("TrackName")] public string name;
    [JsonProperty("waypoints")] public Vector3[] waypoints;
    [JsonProperty("complexity")] public int comp;
}
public class pathGenerator : MonoBehaviour
{
    public static pathGenerator Instance;
    [SerializeField] private string TrackName;
    [SerializeField] private float recordingFreq;
    [SerializeField] private bool set_to_record;
    [SerializeField] private float max_distance_Border;
    [SerializeField] private ShipController ship;
    [SerializeField] private LineRenderer lr;
    
    private float recordingCooldown;
    private bool recordPath;
    
    private IntendedPath _currentPath = new IntendedPath();
    public string savesListPath = "";
    public string dataPath = "";
    public List<Vector3> waypoints = new List<Vector3>();
    
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        
        if (set_to_record)
        {
            Time.timeScale = 0;
        }
        else
        {
            LoadPath(TrackName);
            Time.timeScale = 1;
        }
        
    }

    private void Drawline()
    {
        var drawPoints = _currentPath.waypoints;
        float closestdist = Single.MaxValue;
        for (int i = 0; i < _currentPath.comp; i++)
        {
            var dist = Vector3.Distance(drawPoints[i], ship.transform.position);
            if ( dist<closestdist)
            {
                closestdist = dist;
            }
        }

        if (closestdist>max_distance_Border)
        {
            UI.Instance.Finish(false,"You went too far from the path");
        }
        lr.enabled = true;
        lr.positionCount = drawPoints.Length;
        lr.SetPositions(drawPoints);
        lr.startColor = Color.white;
        lr.endColor =Color.white;
        lr.widthMultiplier = 100;
    }

    public void LoadPath(string trackName)
    {
        dataPath = Application.streamingAssetsPath + "/paths/" + trackName + ".json";
        string jsonData = File.ReadAllText(dataPath);
        _currentPath = JsonConvert.DeserializeObject<IntendedPath>(jsonData);
    }
   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)&&recordPath)
        {
            Time.timeScale = 0f;
            recordPath = false;
            SavePath();
            Debug.Log("saved");
            LoadPath(TrackName);
            Drawline();
            Debug.Log("drawed");
        }
        if (Input.GetKeyDown(KeyCode.R)&&!recordPath)
        {
            Time.timeScale = 1f;
            recordPath = true;
        }

        
        if (recordPath)
        {
            recordingCooldown += Time.deltaTime;
            if (recordingCooldown>=recordingFreq)
            {
                waypoints.Add(ship.rb.position);
                recordingCooldown = 0;
            }
        }
        else
        {
            Drawline();
        }
    }

    private void SavePath()
    {
        var drawpoints = new Vector3[waypoints.Count];
        for (int i = 0; i < waypoints.Count; i++)
        {
            drawpoints[i] = waypoints[i];
        }
        var saveName = "1";
        dataPath = Application.streamingAssetsPath + "/paths/" + TrackName + ".json";
        Debug.Log("PATH: "+ dataPath);
        _currentPath.name = "track" + saveName;
        _currentPath.waypoints = drawpoints;
        _currentPath.comp = waypoints.Count;
        string output = JsonConvert.SerializeObject(_currentPath,new JsonSerializerSettings() {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });
        File.WriteAllText(dataPath,output);
    }
}
