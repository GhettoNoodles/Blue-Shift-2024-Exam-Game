using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool isFinish;
    public bool isCurrentCP;
    public bool isCleared = false;
    public Checkpoint nextCP;
    public float timeSnap;

    public void ClearCP()
    {
        if (isFinish)
        {
            if (isCurrentCP)
            {
                
                UI.Instance.Finish(true);
            }
            else
            {
                UI.Instance.Finish(false);
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
