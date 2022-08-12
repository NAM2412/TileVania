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
    [SerializeField] Vector2 deathKick = new Vector2(20f,20f);
    [SerializeField]  AudioClip DyingSFX;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    float currentGravity;
    float onLadderGravity=0;
    bool isAlive = true;

    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        currentGravity = myRigidbody.gravityScale;
    }


    void Update()
    {
        if (!isAlive) {return;}
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
        
    }

    private void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            GetComponent<AudioSource>().PlayOneShot(DyingSFX);
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            myRigidbody.velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
        
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
        if (!isAlive) {return;}
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; } // Nếu chân Umi đang không ở trên mặt đất thì ko thực hiện nhảy
        if (value.isPressed) // nếu chạm thì thực hiện nhảy
        {
            myRigidbody.velocity = new Vector2(0f,jumpSpeed);              
        }
              
    }
    private void OnMove(InputValue value)
    {
        if (!isAlive) {return;}
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }
    private void OnFire (InputValue value) 
    {
        if(!isAlive) {return;}
        if (value.isPressed) // nếu chạm thì thực hiện nhảy
        {
              Instantiate(bullet, gun.position,transform.rotation);
        }
    }
    private void ClimbLadder()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) // Nếu Umi không chạm thang
        {
            myAnimator.SetBool("IsClimbing",false);
            myRigidbody.gravityScale = currentGravity; // set GravityScale khi ở dưới mặt đất
            return;
        } 
        else
        {

            Vector2 ClimbVelocity = new Vector2(myRigidbody.velocity.x, moveInput.y*climbSpeed);
            myRigidbody.velocity = ClimbVelocity;
            myRigidbody.gravityScale = onLadderGravity;
            bool IsPlayerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon; //xem nhân vật có đang ở trên thang hay không`?
            myAnimator.SetBool("IsClimbing",IsPlayerHasVerticalSpeed);
        }

    }
}
