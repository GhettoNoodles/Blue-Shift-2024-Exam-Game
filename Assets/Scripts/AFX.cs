using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AFX : MonoBehaviour
{
   public static AFX Instance;
   [SerializeField] private AudioSource sourcePrefab;

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

   public void PlayClip(AudioClip clip, Transform spawnTransform, float volume)
   {
      AudioSource src = Instantiate(sourcePrefab, spawnTransform.position, Quaternion.identity);
      src.volume = volume;
      src.clip = clip;
      src.Play();
      src.loop = true;
      //float cliplength = src.clip.length;
      //Destroy(src,cliplength);
   }
}
