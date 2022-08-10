using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 20f;
    float bulletHeight = 0f;
    Rigidbody2D Brigidbody;
    PLayerMovement player;
    float bulletDirection;
    void Start()
    {
        Brigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PLayerMovement>();
        bulletDirection = player.transform.localScale.x*bulletSpeed;
    }

    void Update()
    {
        Brigidbody.velocity = new Vector2(bulletDirection,bulletHeight);
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);            
        }
        Destroy(gameObject);
    }

}
