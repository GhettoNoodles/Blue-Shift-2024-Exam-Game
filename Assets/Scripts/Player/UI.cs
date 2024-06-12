using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI Instance;
    [SerializeField] private Image fwd;
    [SerializeField] private Image bck;
    [SerializeField] private Image right;
    [SerializeField] private Image lft;
    [SerializeField] private Image up;
    [SerializeField] private Image down;
    [SerializeField] private TextMeshProUGUI velTxt;
    [SerializeField] private TextMeshProUGUI timerTxt;
    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject pausemenu;
    [SerializeField] private GameObject pausebtn;
    [SerializeField] private GameObject finPanel;
    [SerializeField] private GameObject retry;
    [SerializeField] private GameObject next;
    [SerializeField] private TextMeshProUGUI finTimeText;

    [SerializeField] private float raceTime;
    
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


    public void Finish(bool complete)
    {
        Time.timeScale = 0;
        hud.SetActive(false);
        finPanel.SetActive(true);
        if (complete)
        {
            var timetext = (Time.timeSinceLevelLoad).ToString("F3");
            finTimeText.text = timetext;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(next);
        }
        else
        {
            finTimeText.text = "Did not finish";
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(retry);
        }
       
      
       
    }

    private void Update()
    {
        if (Input.GetButtonDown("options"))
        {
            Pause();
        }
        
        timerTxt.text = (Time.timeSinceLevelLoad).ToString("F2");
    }

    public void UpdateHud(float BF, float LR,float DU)
    {
        if (BF>=0)
        {
            fwd.fillAmount = BF;
            bck.fillAmount = 0f;
        }
        else
        {
            fwd.fillAmount = 0f;
            bck.fillAmount = Mathf.Abs(BF);
        }

        if (LR>=0)
        {
            right.fillAmount = LR;
            lft.fillAmount = 0f;
        }
        else
        {
            right.fillAmount = 0f;
            lft.fillAmount = Mathf.Abs(LR);
        }

        if (DU>=0)
        {
            up.fillAmount = DU;
            down.fillAmount = 0f;
        }
        else
        {
            up.fillAmount = 0f;
            down.fillAmount = Mathf.Abs(DU);
        }
    }

    public void UpdateVelocity(float vel)
    {
        velTxt.text = Mathf.Floor(vel).ToString();
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pausemenu.SetActive(true);
        hud.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pausebtn);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pausemenu.SetActive(false);
        hud.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }public void NextTrack()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
