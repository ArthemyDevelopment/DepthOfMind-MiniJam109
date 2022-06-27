using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyArea : MonoBehaviour
{
    public UnityEvent OnEnter;
    public UnityEvent OnExit;


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            OnEnter.Invoke();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            OnExit.Invoke();
    }
}
