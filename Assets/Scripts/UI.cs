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
    [SerializeField] private TextMeshProUGUI velTxt;
    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject pausemenu;
    [SerializeField] private GameObject pausebtn;
    
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

    private void Update()
    {
        if (Input.GetButtonDown("options"))
        {
            Pause();
        }
    }

    public void UpdateHud(float BF, float LR)
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
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
