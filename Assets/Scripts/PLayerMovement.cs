using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PLayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 10f;

    CapsuleCollider2D myCapsuleCollider;
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    
    Animator myAnimator;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
    }


    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }

    private void FlipSprite()
    {
        bool IsPlayerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon; // so sánh xem nhân vật có đang di chuyển hay không`

        if (IsPlayerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x),1f);
        }
    }

    private void Run()
    {
            Vector2 playerVelocity = new Vector2(moveInput.x*runSpeed, myRigidbody.velocity.y);
            myRigidbody.velocity = playerVelocity;

            bool IsPlayerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon; // so sánh xem nhân vật có đang di chuyển hay không`
            myAnimator.SetBool("IsRunning",IsPlayerHasHorizontalSpeed);
    }

    private void OnJump (InputValue value) 
    {
        if (!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; } // Kiểm tra xem Umi có chạm đất hay không?
        if (value.isPressed) // nếu chạm thì thực hiện nhảy
        {
            // do stuff
            myRigidbody.velocity = new Vector2(0f,jumpSpeed);
                
        }
        
    }
    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    private void ClimbLadder()
    {
        if (!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) { return; } // Kiểm tra xem Umi có chạm thang hay không?

        Vector2 ClimbVelocity = new Vector2(myRigidbody.velocity.x, moveInput.y*climbSpeed);
        myRigidbody.velocity = ClimbVelocity;

    }
}
