using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;


public class BestTime
{
    [JsonProperty("checkPointTimes")] public float[] times;
}
public class timeSaver : MonoBehaviour
{
    public static timeSaver Instance;
    
    
    
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
}
