using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float changeDirectionTime;
    
    private BoxCollider2D boxCollider2d;
    private Rigidbody2D rigidbody2D;

    private bool isLeft = true;
    private float timeCounter = 0f;
    private void Awake()
    {
        TryGetComponent(out boxCollider2d);
        TryGetComponent(out rigidbody2D);
    }

    private void Update()
    {
        if (timeCounter < changeDirectionTime)
        {
            timeCounter += Time.deltaTime;
            return;
        }

        isLeft = !isLeft;
        
        float newAngle = isLeft ? 0 : 180;
        transform.eulerAngles = new Vector2(0, newAngle);
        
        timeCounter = 0f;
    }

    private void FixedUpdate()
    {
        float walkDirection = isLeft ? -1 : 1;
        rigidbody2D.velocity = Vector2.right * speed * walkDirection;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            FindObjectOfType<GameController>().DefeatScene();
        }
    }
}
