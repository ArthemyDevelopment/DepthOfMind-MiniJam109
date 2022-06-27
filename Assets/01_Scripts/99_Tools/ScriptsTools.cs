using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ScriptsTools
{
    public static float MapValues(float value, float from1, float to1, float from2, float to2) //Map a value based on two ranges
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }


    private static readonly Dictionary<float, WaitForSeconds> WaitDictionary = new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds GetWait(float time) //Get a wait for second element in case it was already used before, optimizing its use
    {
        if (WaitDictionary.TryGetValue(time, out var wait))return wait;

        WaitDictionary[time] = new WaitForSeconds(time);
        return WaitDictionary[time];
    }
    
    
    public static TComponent AddComponent<TComponent, TFirstArgument>  //Allow to use AddComponent with a parameter
        (this GameObject gameObject, TFirstArgument firstArgument)
        where TComponent : MonoBehaviour
    {
        Arguments<TFirstArgument>.First = firstArgument;

         
        var component = gameObject.AddComponent<TComponent>();
 
        Arguments<TFirstArgument>.First = default;
 
        return component;
    }
    
    public static float GetRotation(Transform originObject, Transform target) //Get the angle between 2 objects
    {
        return Mathf.Atan2(target.position.y - originObject.position.y,  target.position.x - originObject.position.x) * Mathf.Rad2Deg;
    }

    public static Queue<T> ShuffleList<T>(List<T> thisList) //Shuffle a list and returns it as queue
    {
        Queue<T> temp = new Queue<T>(thisList.OrderBy(a => Guid.NewGuid()).ToList());

        return temp;
    }

}

public static class Arguments<TFirstArgument>
{
    public static TFirstArgument First { get; internal set; }
    
}

public abstract class SingletonManager<T> : MonoBehaviour where T : SingletonManager<T>  //Automaticaly creates a singleton, the init method must be called on awake or similar
{
    public static T current; 
    
    
    public virtual void init()
    {
        if (current == null)
            current = this as T;
        else if (current != this)
        {
            Destroy(this);
        }
        
    }
}
