using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private AIPath agent;
    public Animator anim;
    public PlayerHealthNotifier HealthNotifier;
    public RuntimeAnimatorController[] Animations;
    public GameObject EnemyArea;
    private bool isChase;
    private string ActAnim;

    private void OnEnable()
    {
        agent = GetComponent<AIPath>();
        HealthNotifier.OnNotify += UpdateSprites;
        StopChase();
        EnemyArea.SetActive(true);
    }

    private void OnDisable()
    {
        HealthNotifier.OnNotify -= UpdateSprites;
    }

    void UpdateSprites(int i)
    {
        if (i <= 0)
        {
            StopChase();
            ActAnim="Idle";
        }
        else if (i < 4)
            anim.runtimeAnimatorController = Animations[0];
        else if(i<8)
            anim.runtimeAnimatorController = Animations[1];
        else if(i<=11)
            
            anim.runtimeAnimatorController = Animations[2];
        else
        {
            anim.runtimeAnimatorController = Animations[3];
            StopChase();
            ActAnim="Idle";
        }
            
        anim.Play(ActAnim);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.CompareTag("UI/Circle")&&isChase)
        {
            anim.Play("Walk");
            ActAnim="Walk";
        }

        if (col.CompareTag("UI/Dark")&&isChase)
        {
            anim.Play("OnDark");
            ActAnim = "OnDark";
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {

        if (col.CompareTag("UI/Circle")&&isChase)
        {
            anim.Play("OnDark");
            ActAnim = "OnDark";
        }
    }


    public void ChasePlayer()
    {
        agent.isStopped = false;
        //agent.destination = PlayerManager.current.gameObject.transform.position;
        agent.SearchPath();
        isChase = true;
    }

    public void StopChase()
    {
        agent.isStopped = true;
        //agent.destination=Vector3.positiveInfinity;
        agent.SetPath(null);
        isChase = false;

    }
    
    
}
