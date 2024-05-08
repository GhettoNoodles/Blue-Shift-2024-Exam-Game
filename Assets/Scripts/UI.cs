using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI Instance;
    [SerializeField] private Image fwd;
    [SerializeField] private Image bck;
    [SerializeField] private Image right;
    [SerializeField] private Image lft;
    
    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void UpdateHud(float BF, float LR)
    {
        if (BF>0)
        {
            fwd.fillAmount = BF;
        }
        else
        {
            bck.fillAmount = Mathf.Abs(BF);
        }

        if (LR>0)
        {
            right.fillAmount = LR;
        }
        else
        {
            lft.fillAmount = Mathf.Abs(LR);
        }
    }
}
