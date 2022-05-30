using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Movment : MonoBehaviour
{
    private Vector3 targetPosition;
    private bool isMoving;
    private float speed;
    public static bool IsFacingRight { get; set; }
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public TextMeshProUGUI textMesh;

    public void Awake()
    {
        isMoving = false;
        speed = 1.15f;
        IsFacingRight = false;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        animator.speed = 0.83f;
    }


    void Update()
    {
        if (Input.GetMouseButton(0))
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
        if (targetPosition.x < transform.position.x && IsFacingRight) Flip();
        else if (targetPosition.x > transform.position.x && !IsFacingRight) Flip();

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (transform.position == targetPosition)
        {
            isMoving = false;
            animator.SetBool("IsMoving", false);
        }
        else
        {
            animator.SetBool("IsMoving", true);
        }
    }

    private void Flip()
    {
        IsFacingRight = !IsFacingRight;
        var playerScale = spriteRenderer.transform.localScale;
        playerScale.x *= -1;
        var textScale = textMesh.transform.localScale;
        textScale.x *= -1;

        textMesh.transform.localScale = textScale;
        transform.localScale = playerScale;
    }
}