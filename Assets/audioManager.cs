using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public static audioManager Instance;
    [SerializeField] private AudioSource bgMusic;
    [SerializeField] private AudioSource aFx;
    [SerializeField] private AudioClip cpClips;
    [SerializeField] private AudioClip bgmusicClip;
    [SerializeField] private AudioClip failClip;
    [SerializeField] private AudioClip winClip;
    [Range(0, 1)] [SerializeField] private float MusicVol;
    [Range(0, 1)] [SerializeField] private float fxVol;
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
        PlayBG();
    }

    private void PlayBG()
    {
        bgMusic.clip = bgmusicClip;
        bgMusic.volume = MusicVol;
        bgMusic.Play();
        bgMusic.loop = true;
    }

    public void PlayCP()
    {
        aFx.clip = cpClips;
        aFx.Play();
        aFx.volume = fxVol;
    }public void PlayWin()
    {
        aFx.clip = winClip;
        aFx.Play();
        aFx.volume = fxVol;
    }public void PlayLose()
    {
        aFx.clip = failClip;
        aFx.Play();
        aFx.volume = fxVol;
    }
}
