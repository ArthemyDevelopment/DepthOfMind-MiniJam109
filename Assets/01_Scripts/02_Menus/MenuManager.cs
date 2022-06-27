using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public RectTransform Arrow;
    public RectTransform PlayPos;
    public RectTransform CreditsPos;
    private bool state;
    public bool isActive;
    public bool inCredits;
     Animator anims;

    private void Start()
    {
        anims = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Vertical")&&isActive)
        {
            SwapButtons();
        }

        if (Input.GetButtonDown("Action"))
        {
            if (isActive)
            {
                if (state)
                    Credits();
                else
                    Play();
               
            }
            else if(inCredits)
                BackToMenu();
        }
    }

    private void SwapButtons()
    {
        if (!state)
        {
            Arrow.position = CreditsPos.position;
            state = true;
            
        }
        else
        {
            Arrow.position = PlayPos.position;
            state = false;
        }
            
        
    }

    public void Play()
    {
        anims.Play("PlayAnimation");
        isActive = false;
        
    }

    public void Credits()
    {
        anims.Play("CreditsAnimation");
        inCredits = true;
        isActive = false;
    }

    public void BackToMenu()
    {
        anims.Play("StartAnimation");
        inCredits = false;
        
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(1);
    }

    void SwitchMenu()
    {
        Arrow.position=PlayPos.position;
        isActive = true;
        state = false;
    }
}
