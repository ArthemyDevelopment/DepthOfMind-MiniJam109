using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsPool : SingletonManager<ItemsPool>
{
    public List<GameObject> ItemsInScene;
    public List<Transform> SpawnPositions;
    private Queue<GameObject> Pool = new Queue<GameObject>();
    private Queue<Transform> Spawns = new Queue<Transform>();
    public float timer;
    public bool isActive;


    private void OnEnable()
    {
    }

    private void Start()
    {
        init();
        Pool = ScriptsTools.ShuffleList(ItemsInScene);
        Spawns= ScriptsTools.ShuffleList(SpawnPositions);
        StartCoroutine(PoolTimer());
        
    }

    public void EnqueueItem(GameObject G, Transform T)
    {
        G.SetActive(false);
        Pool.Enqueue(G);
        Spawns.Enqueue(T);
    }

    public GameObject DequeuItem()
    {
        GameObject temp = Pool.Dequeue();
        temp.SetActive(true);
        return temp;
    }


    IEnumerator PoolTimer()
    {
        while (isActive)
        {
            Debug.Log("Loop timer");
            if (Pool.Count != 0)
            {
                Debug.Log("Pool has");
                Transform tempS = Spawns.Dequeue();
                GameObject tempG = DequeuItem();
                tempG.transform.position = tempS.position;
                tempG.GetComponent<ItemController>().spawn = tempS;
            }

            yield return ScriptsTools.GetWait(timer);
        } 
        
    }
    
    
}
