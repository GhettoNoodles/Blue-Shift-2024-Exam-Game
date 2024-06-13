using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;


public class BestTime
{
    [JsonProperty("checkPointTimes")] public float[] times;
}

public class timeSaver : MonoBehaviour
{
    public static timeSaver Instance;
    [SerializeField] private List<Checkpoint> cps;
    [SerializeField] private string path;
    [SerializeField] private string trackName;


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
        path = Application.persistentDataPath + "/times/" + trackName + ".json";
    }

    private BestTime LoadTimes()
    {
        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            var oldTimes = JsonConvert.DeserializeObject<BestTime>(jsonData);
            Debug.Log("loaded from " + path);
            return oldTimes;
        }

        return null;
    }

    public float CompareTimes(int index)
    {
        var oldTimes = LoadTimes();
        if (oldTimes != null)
        {
            var diff = cps[index].timeSnap - oldTimes.times[index];
            if (index == cps.Count - 1&&diff<0)
            {
                SaveTimes();
            }

            return diff;
        }
        else
        if (cps.Count == index-1)
        {
            SaveTimes();
        }

        return 0f;
    }

    private void SaveTimes()
    {
        var newTimes = new BestTime();
        float[] timesnaps = new float[cps.Count];
        for (int i = 0; i < cps.Count; i++)
        {
            timesnaps[i] = cps[i].timeSnap;
        }

        newTimes.times = timesnaps;
        string output = JsonConvert.SerializeObject(newTimes);
        File.WriteAllText(path, output);
        Debug.Log("saved new time");
    }
}