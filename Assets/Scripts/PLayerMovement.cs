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
