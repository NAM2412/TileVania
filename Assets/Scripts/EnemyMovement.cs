using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float EmoveSpeed = 1f;
    Rigidbody2D Erigibody;
    BoxCollider2D EboxCollider;
    SpriteRenderer Esprite;
    void Start()
    {
       Erigibody = GetComponent<Rigidbody2D>();
       EboxCollider = GetComponent<BoxCollider2D>();
       Esprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Erigibody.velocity = new Vector2(EmoveSpeed,0f);
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        EmoveSpeed = -EmoveSpeed;
        FlipEnemyFacing();
    }

    private void FlipEnemyFacing()
    {
         transform.localScale = new Vector2 (-(Mathf.Sign(Erigibody.velocity.x)),1f);
    }
}
