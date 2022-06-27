using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : SingletonManager<PlayerManager>
{
    private PlayerController controller;
    private int _playerHeath=5;

    public RectTransform CanvasCircle;
    public CircleCollider2D CanvasCircleCollider;
    public PlayerHealthNotifier HealthNotifier;
    
    
    public int PlayerHeath { get => _playerHeath;
        set
        {
            _playerHeath = value;
            StartCoroutine(Invencibility());
            HealthCheck();
        } 
    }

    private bool isInvencible;

    void Awake()
    {
        init();
        controller = GetComponent<PlayerController>();
        HealthCheck();
    }

    private void HealthCheck()
    {
        if (PlayerHeath == 0)
        {
            OnPlayerDeath();
            controller.ActState = PlayerState.Death;
        }
        else if (PlayerHeath > 11)
        {
            OnPlayerWin();
            controller.ActState = PlayerState.Win;
        }
        UpdateLight();
        NotifyEnemies();
    }

    void UpdateLight()
    {
        int temp = PlayerHeath * 3;
        CanvasCircle.sizeDelta = new Vector2(temp, temp);
        CanvasCircleCollider.radius = (float)temp / 2;
    }

    void NotifyEnemies()
    {
        HealthNotifier.NotifyHealth(PlayerHeath);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy")&& !isInvencible)
        {
            PlayerHeath--;
            LevelManager.current.SoundDamage();
        }

        if (col.CompareTag("Item"))
        {
            PlayerHeath++;
            LevelManager.current.SoundHeal();
        }
    }

    IEnumerator Invencibility()
    {
        isInvencible = true;
        gameObject.layer = 10;
        controller.PlayerSpeed = .3f;
        for (int i = 0; i < 5; i++)
        {
            controller.sprite.enabled = false;
            yield return ScriptsTools.GetWait(.1f);
            controller.sprite.enabled = true;
            yield return ScriptsTools.GetWait(.1f);
        }
        controller.PlayerSpeed = 0.2f;
        gameObject.layer = 8;
        isInvencible = false;
    }

    public void OnPlayerDeath()
    {
        CanvasCircleCollider.enabled = false;
        LevelManager.current.PlayersDied();
        
    }

    public void OnPlayerWin()
    {
        CanvasCircle.parent.gameObject.SetActive(false);
        LevelManager.current.PlayerWon();
    }
}
