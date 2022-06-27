using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{

    public Image ItemSprite;
    public Sprite InLightSprite;
    public Sprite OnDarkSprite;
    [HideInInspector]public Transform spawn;
    
    
    
    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.CompareTag("Player"))
        {
            ItemsPool.current.EnqueueItem(this.gameObject,spawn);
        }

        if (col.CompareTag("UI/Circle"))
        {
            ItemSprite.sprite = InLightSprite;
        }

        if (col.CompareTag("UI/Dark"))
        {
            ItemSprite.sprite = OnDarkSprite;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {

        if (col.CompareTag("UI/Circle"))
        {
            ItemSprite.sprite = OnDarkSprite;
        }
    }
}
