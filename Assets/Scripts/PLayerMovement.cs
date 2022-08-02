using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PLayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    Vector2 moveInput;
    Rigidbody2D Myrigidbody;
    void Start()
    {
        Myrigidbody = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        Run();
        FlipSprite();
    }

    private void FlipSprite()
    {
        bool IsPlayerHasHorizontalSpeed = Mathf.Abs(Myrigidbody.velocity.x) > Mathf.Epsilon; // so sánh xem nhân vật có đang di chuyển hay không`

        if (IsPlayerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2 (Mathf.Sign(Myrigidbody.velocity.x),1f);
        }
    }

    private void Run()
    {
            Vector2 playerVelocity = new Vector2(moveInput.x*runSpeed, Myrigidbody.velocity.y);
            Myrigidbody.velocity = playerVelocity;
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }
}
