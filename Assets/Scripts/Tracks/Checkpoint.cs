using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool isFinish;
    public bool isCurrentCP;
    public bool isCleared = false;
    public Checkpoint nextCP;
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    public void ClearCP()
    {
        if (isFinish)
        {
            if (isCurrentCP)
            {
                Debug.Log("finish");
                Debug.Log(timer);
            }
            else
            {
                Debug.Log("Cant Finish");
            }
        }
        else if (isCurrentCP)
        {
            isCleared = true;
            nextCP.isCurrentCP = true;
            isCurrentCP = false;
        }
    }

}
