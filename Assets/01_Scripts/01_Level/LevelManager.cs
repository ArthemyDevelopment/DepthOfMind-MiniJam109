using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : SingletonManager<LevelManager>
{

    public GameObject WinCanvas;
    public GameObject DeathCanvas;
    public AudioSource Music;
    public AudioSource WinMusic;
    public AudioSource LoseMusic;
    public AudioSource DamageSound;
    public AudioSource HealSound;
    
    private void Awake()
    {
        init();
    }

    public void PlayerWon()
    {
        WinCanvas.SetActive(true);
        Music.Stop();
        WinMusic.Play();
    }

    public void PlayersDied()
    {
        DeathCanvas.SetActive(true);
        Music.Stop();
        LoseMusic.Play();
    }

    public void SoundDamage()
    {
        Music.mute = true;
        DamageSound.Play();
        StartCoroutine(ResumeMusic(0.2f));
        
    }

    public void SoundHeal()
    {
        Music.mute = true;
        HealSound.Play();
        StartCoroutine(ResumeMusic(0.4f));
    }
    
    

    IEnumerator ResumeMusic(float t )
    {
        yield return ScriptsTools.GetWait(t);
        Music.mute = false;
    }
    
    
    
    
    
    
}
