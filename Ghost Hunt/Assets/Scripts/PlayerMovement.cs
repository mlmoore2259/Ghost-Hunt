// Base code from https://empowerwrite.medium.com/unity-2024-input-system-tutorial-basic-platformer-movement-138a494c7003

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerGameInfo playerGameInfo;
    private PlayerControls controls;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    [Header("Movement Settings")]
    public float moveSpeed;
    int possessionFreq = 5;
    public int count;
    //public float jumpForce = 7f;
    public LayerMask groundLayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerGameInfo = GameObject.Find("PlayerGameInfo").GetComponent<PlayerGameInfo>();
    }

    private void Awake()
    {
        moveSpeed = 5f;
        count = 0;
        // Initialize the PlayerControls instance
        controls = new PlayerControls();

        // Subscribe to the movement and jump actions
        controls.Keyboard.Move.performed += ctx => moveInput =
          ctx.ReadValue<Vector2>();
        controls.Keyboard.Move.canceled += ctx => moveInput =
          Vector2.zero;

        //controls.Keyboard.Jump.performed += ctx => Jump();
    }

    private void Update()
    {
        count++;
        if (playerGameInfo.possessionLvl >= 100f && count == possessionFreq)
        {
            AddPossessionMovement();
        }
        else
        {
            MoveUpDown();
            MoveLeftRight();
        }
        // Reset count 
        if (count >= possessionFreq)
        {
            count = 0;
        }
    }

    private void OnEnable()
    {
        // Enable the input controls
        controls.Enable();
    }

    private void OnDisable()
    {
        // Disable the input controls when the player object is disabled
        controls.Disable();
    }

    private void MoveLeftRight()
    {
        // Apply horizontal movement based on input
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
    }

    private void MoveUpDown()
    {
        // Apply vertical movement based on input
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, moveInput.y * moveSpeed);
    }

    private void AddPossessionMovement()
    {
        // Add random movements when possessed
        rb.linearVelocity = new Vector2(Random.Range(-1f, 1f) * moveSpeed, Random.Range(-1f, 1f) * moveSpeed);
    }
}
