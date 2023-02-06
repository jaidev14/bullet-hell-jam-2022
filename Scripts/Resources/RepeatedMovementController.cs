using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatedMovementController : MonoBehaviour
{
    public float speed = 8f;
    public float movementWidth = 2;
    [SerializeField] private bool isHorizontal = true;

    Rigidbody2D rigidBody;
    private float maxPosition;
    private float minPosition;
    private int direction = 1; 

    void Start()
    {
        this.rigidBody = GetComponent<Rigidbody2D>();
        float startPosition = isHorizontal ? transform.position.x : transform.position.y;
        maxPosition = startPosition + movementWidth;
        minPosition = startPosition - movementWidth;
    }

    void FixedUpdate()
    {
        float xVelocity = rigidBody.velocity.x;
        float yVelocity = rigidBody.velocity.y;

        if (isHorizontal) {
            if (transform.position.x > maxPosition || transform.position.x < minPosition) {
                ChangeDirection();
            }
            xVelocity = direction * speed;
        } else {
            if (transform.position.y > maxPosition || transform.position.y < minPosition) {
                ChangeDirection();
            }
            yVelocity = direction * speed;
        }
        this.rigidBody.velocity = new Vector2(xVelocity, yVelocity);
    }

    void ChangeDirection() {
        direction *= -1;
    }
}
