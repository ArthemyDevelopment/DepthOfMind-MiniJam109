using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float PlayerSpeed;
    private PlayerState _actState;
    private Rigidbody2D rb;
    [HideInInspector]public SpriteRenderer sprite;
    public GameObject GhostTarget;
    private Animator anims;
    
    public PlayerState ActState
    {
        get => _actState;
        set
        {
            _actState = value;
            StateChange();
        }
    }

    private Vector2 MovDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite=GetComponent<SpriteRenderer>();
        anims = GetComponent<Animator>();

        rb.inertia = 0;
        
    }

    
    void Update()
    {
        if (ActState == PlayerState.Movement)
        {
            InputHandler();
            
        }
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    void InputHandler()
    {
        MovDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        GhostTarget.transform.localPosition = MovDirection*4;
        MovDirection *= PlayerSpeed;

    }

    void PlayerMovement()
    {
        if (MovDirection.magnitude != 0)
        {
            sprite.flipX = MovDirection.x < 0;
            if(MovDirection.y>0)
                anims.Play("WalkBack");
            else
                anims.Play("WalkFront");
            
            rb.MovePosition(transform.position+(Vector3)MovDirection);
        }
        else 
            anims.Play("Idle");
    }

    void StateChange()
    {
        switch (ActState)
        {
            case PlayerState.Movement:
                break;
            case PlayerState.Death:
                Debug.Log("Death");
                rb.velocity =Vector2.zero;
                rb.simulated = false;
                
                
                break;
            case PlayerState.Win:
                Debug.Log("Win");
                rb.velocity =Vector2.zero;
                rb.simulated = false;
                anims.Play("Idle");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }


}
