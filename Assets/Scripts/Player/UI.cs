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
    [SerializeField] private TextMeshProUGUI diffTxt;
    [SerializeField] private TextMeshProUGUI finDiffTxt;
    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject pausemenu;
    [SerializeField] private GameObject pausebtn;
    [SerializeField] private GameObject finPanel;
    [SerializeField] private GameObject retry;
    [SerializeField] private GameObject next;
    [SerializeField] private TextMeshProUGUI finTimeText;
    [SerializeField] private float raceTime;
    [SerializeField] private float maxfade;
    private bool finished =false;
    private float fadeTimer;
    private bool fading =false;

    private void Awake()
    {
        if (Instance == null)
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
        if (!finished)
        {
            finished = true;
            Time.timeScale = 0;`
            hud.SetActive(false);
            finPanel.SetActive(true);
            if (complete)
            {
                var timetext = (Time.timeSinceLevelLoad).ToString("F3");
                var difference = timeSaver.Instance.CompareTimes(FindObjectsOfType<Checkpoint>().Length - 1);
                Debug.Log(difference);
                finTimeText.text = timetext;
                
                if (difference>=0f)
                {
                    finDiffTxt.text = "+"+ difference.ToString("F3");
                    finDiffTxt.color =Color.red;
                }
                else
                {
                    finDiffTxt.text = difference.ToString("F3");
                    finDiffTxt.color = Color.green;
                }
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
    }

    public void CheckpointTime(float diff)
    {
        diffTxt.gameObject.SetActive(true);
        diffTxt.text = diff.ToString("F3");
        fading = true;
        if (diff>=0f)
        {
            diffTxt.color =Color.red;
        }
        else
        {
            diffTxt.color = Color.green;
        }
        
    }

    private void Update()
    {
        if (Input.GetButtonDown("options"))
        {
            Pause();
        }

        timerTxt.text = (Time.timeSinceLevelLoad).ToString("F2");
        if (fading)
        {
            fadeTimer += Time.deltaTime;
            if (fadeTimer>=maxfade)
            {
                fadeTimer = 0f;
                fading = false;
                diffTxt.gameObject.SetActive(false);
            }
        }
    }

    public void UpdateHud(float BF, float LR, float DU)
    {
        if (BF >= 0)
        {
            fwd.fillAmount = BF;
            bck.fillAmount = 0f;
        }
        else
        {
            fwd.fillAmount = 0f;
            bck.fillAmount = Mathf.Abs(BF);
        }

        if (LR >= 0)
        {
            right.fillAmount = LR;
            lft.fillAmount = 0f;
        }
        else
        {
            right.fillAmount = 0f;
            lft.fillAmount = Mathf.Abs(LR);
        }

        if (DU >= 0)
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
    }

    public void NextTrack()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}