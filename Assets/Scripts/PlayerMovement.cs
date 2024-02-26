using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private Animator animator;
    [SerializeField] private Camera mainCamera;

    [SerializeField] private float movementSpeed;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(joystick.Horizontal * movementSpeed, joystick.Vertical * movementSpeed);
        mainCamera.transform.position = new Vector3(rb.transform.position.x, rb.transform.position.y, -10f);

        if(joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            animator.SetBool("Idle", false);
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("Idle", true);
        }
            
    }
}
