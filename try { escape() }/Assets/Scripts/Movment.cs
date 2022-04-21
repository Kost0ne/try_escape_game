using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movment : MonoBehaviour
{
    private Vector3 targetPosition;
    private bool isMoving;
    private float speed;
    private bool isFacingRight;

    public void Awake()
    {
        isMoving = false;
        speed = 3.0f;
        isFacingRight = false;
    }
    
    void Update()
    {
        if(Input.GetMouseButton(0)) 
            SetTargetPosition();
        if (isMoving) 
            Move();
    }

    void SetTargetPosition()
    {
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.y = transform.position.y;
        targetPosition.z = transform.position.z;
        isMoving = true;
    }

    private void Move()
    {
        if (targetPosition.x < transform.position.x && isFacingRight) Flip();
        else if (targetPosition.x > transform.position.x && !isFacingRight) Flip();
        
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if(transform.position == targetPosition)
            isMoving = false;
    }
    
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        var playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }
}
