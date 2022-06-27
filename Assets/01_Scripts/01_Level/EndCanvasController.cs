using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCanvasController : MonoBehaviour
{
    public RectTransform Arrow;
    public RectTransform PlayPos;
    public RectTransform MenuPos;
    private bool state;

    private void Update()
    {
        if (Input.GetButtonDown("Vertical"))
        {
            SwapButtons();
        }

        if (Input.GetButtonDown("Action"))
        {
            if (state)
                SceneManager.LoadScene(0);
            else
                SceneManager.LoadScene(1);
        }
    }

    private void SwapButtons()
    {
        if (!state)
        {
            Arrow.position = MenuPos.position;
            state = true;
            
        }
        else
        {
            Arrow.position = PlayPos.position;
            state = false;
        }
            
        
    }
}
