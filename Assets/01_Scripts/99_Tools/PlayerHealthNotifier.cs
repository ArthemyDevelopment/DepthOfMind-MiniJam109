using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerHealthNotifier : ScriptableObject
{
    public delegate void HealthNotification(int i);

    public HealthNotification OnNotify;
    
    public void NotifyHealth(int i)
    {
        OnNotify.Invoke(i);
    }
}
