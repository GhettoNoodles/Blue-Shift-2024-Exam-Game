using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
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
    [SerializeField] private Image bronze;
    [SerializeField] private Image silver;
    [SerializeField] private Image gold;
    [SerializeField] private TextMeshProUGUI velTxt;
    [SerializeField] private TextMeshProUGUI timerTxt;
    [SerializeField] private TextMeshProUGUI diffTxt;
    [SerializeField] private TextMeshProUGUI finDiffTxt;
    [SerializeField] private TextMeshProUGUI bronzetxt;
    [SerializeField] private TextMeshProUGUI silvertxt;
    [SerializeField] private TextMeshProUGUI goldtxt;
    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject pausemenu;
    [SerializeField] private GameObject pausebtn;
    [SerializeField] private GameObject finPanel;
    [SerializeField] private GameObject retry;
    [SerializeField] private GameObject next;
    [SerializeField] private TextMeshProUGUI finTimeText;
    [SerializeField] private TextMeshProUGUI bestTimetxt;
    [SerializeField] private float raceTime;
    [SerializeField] private float maxfade;
    [SerializeField] private float bronzeTime;
    [SerializeField] private float silverTime;
    [SerializeField] private float authorTime;
    private bool finished = false;
    private float fadeTimer;
    private bool fading = false;

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
    private void Start()
    {
        InputSystem.PauseHaptics();
        bronzetxt.text = bronzeTime.ToString("F3");
        silvertxt.text = silverTime.ToString("F3");
        goldtxt.text = authorTime.ToString("F3");
    }


    public void Finish(bool complete, string msg)
    {
        if (!finished)
        {
            finished = true;
            Time.timeScale = 0;
            hud.SetActive(false);
            finPanel.SetActive(true);
            if (complete)
            {
                var currentTime = Time.timeSinceLevelLoad;
                var timetext = currentTime.ToString("F3");
                var difference = timeSaver.Instance.CompareTimes(FindObjectsOfType<Checkpoint>().Length - 1);
                Debug.Log(difference);
                finTimeText.text = timetext;

                if (difference >= 0f)
                {
                    finDiffTxt.text = "+" + difference.ToString("F3");
                    finDiffTxt.color = Color.red;
                }
                else
                {
                    finDiffTxt.text = difference.ToString("F3");
                    finDiffTxt.color = Color.green;
                }
                
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(next);
                EndStats(currentTime);
            }
            else
            {
                audioManager.Instance.PlayLose();
                finTimeText.text = "Did not finish";
                finDiffTxt.text = msg;
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(retry);
            }
            
        }
    }

    private void EndStats(float currentTime)
    {
        float bestTime =currentTime;
        if (timeSaver.Instance.oldTimes!=null)
        {
             bestTime = timeSaver.Instance.LoadTimes().times.Last();
        }
        
        bestTimetxt.text = "Your Best Time: "+bestTime.ToString("F3");
        if (bestTime<=authorTime)
        {
            bronze.color =Color.white;
            silver.color =Color.white;
            gold.color =Color.white;
        }else if (bestTime<=silverTime)
        {
            bronze.color =Color.white;
            silver.color =Color.white;
            gold.color =Color.black;
        }else if (bestTime<=bronzeTime)
        {
            bronze.color =Color.white;
            silver.color =Color.black;
            gold.color =Color.black;
        }
        
    }

    public void CheckpointTime(float diff)
    {
        if (!timeSaver.Instance.first)
        {
            diffTxt.gameObject.SetActive(true);
            fading = true;
            Debug.Log("UI diff: "+ diff.ToString("F3"));
            if (diff >= 0f)
            {
                Debug.Log("output diff: "+ diff.ToString("F3"));
                diffTxt.text = "+" + diff.ToString("F3");
                diffTxt.color = Color.red;
            }
            else
            {
                diffTxt.text = diff.ToString("F3");
                diffTxt.color = Color.green;
            }
        }
    }

    public void Vibrate(float dist)
    {
        if (dist < 150f)
        {
            Gamepad.current.SetMotorSpeeds(0.75f, 1f);

            InputSystem.ResumeHaptics();
        }
        else if (dist < 250)
        {
            Gamepad.current.SetMotorSpeeds(0.25f, 0.75f);

            InputSystem.ResumeHaptics();
        }
        else if (dist < 500)
        {
            Gamepad.current.SetMotorSpeeds(0.1f, 0.3f);
            InputSystem.ResumeHaptics();
        }
        else
        {
            InputSystem.PauseHaptics();
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
            if (fadeTimer >= maxfade)
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