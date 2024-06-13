using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private int index;
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
                audioManager.Instance.PlayWin();
                UI.Instance.Finish(true,"");
            }
            else
            {
                UI.Instance.Finish(false,"You missed some checkpoints");
            }
        }
        else if (isCurrentCP)
        {
            audioManager.Instance.PlayCP();
            isCleared = true;
            nextCP.isCurrentCP = true;
            isCurrentCP = false;
            var diff = timeSaver.Instance.CompareTimes(index);
            Debug.Log("CP diff: "+ diff.ToString("F3"));
            UI.Instance.CheckpointTime(diff);
        }
    }

}
