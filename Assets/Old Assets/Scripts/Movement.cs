using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cameraTransform; // Reference to the main camera transform
    public float speed = 6f;
    public float sprintSpeed = 16f;
    public float crouchSpeed = 4f;
    public float gravity = -19.62f;
    private Vector3 velocity;
    private bool crouch = false;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    public float jumpHeight = 3;
    public Vector3 standingCameraPosition = new Vector3(0, 1.6f, 0); // Camera position when standing
    public Vector3 crouchingCameraPosition = new Vector3(0, 0.3f, 0.8f); // Camera position when crouching

    // Energy variables
    public Slider energySlider;
    public float maxEnergy = 100f;
    public float currentEnergy;
    public float energyDecreaseRate = 10f;
    public float energyIncreaseRate = 5f;

    // Animation
    private Animator animator;
    private float originalControllerHeight; // Original height of CharacterController

    void Start()
    {
        // Save the original height of the CharacterController
        originalControllerHeight = controller.height;

        // Initialize energy
        currentEnergy = maxEnergy;
        energySlider.maxValue = maxEnergy;
        energySlider.value = currentEnergy;

        // Animation
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Update energy slider
        energySlider.value = currentEnergy;

        // Check if the player is grounded to reset gravity
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        bool isMoving = move.sqrMagnitude > 0; // Check if the player is moving

        // Sprinting logic
        if (Input.GetKey(KeyCode.LeftShift) && currentEnergy > 0 && isMoving && !crouch)
        {
            if (move.sqrMagnitude > 1)
            {
                move.Normalize();
            }
            controller.Move(move * sprintSpeed * Time.deltaTime);
            currentEnergy -= energyDecreaseRate * Time.deltaTime; // Decrease energy
            if (currentEnergy < 0)
            {
                currentEnergy = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ToggleCrouch();
        }
        else
        {
            if (move.sqrMagnitude > 1)
            {
                move.Normalize();
            }
            float currentSpeed = crouch ? crouchSpeed : speed;
            controller.Move(move * currentSpeed * Time.deltaTime);
        }

        // Animation
        if (move == Vector3.zero)
        {
            // Idle
            animator.SetFloat("Speed", 0);
        }
        else if (!Input.GetKey(KeyCode.LeftShift))
        {
            // Walk
            animator.SetFloat("Speed", 0.5f);
        }
        else
        {
            animator.SetFloat("Speed", 1);
        }

        // Increase energy when not sprinting or not moving
        if (!Input.GetKey(KeyCode.LeftShift) || !isMoving)
        {
            if (currentEnergy < maxEnergy)
            {
                currentEnergy += energyIncreaseRate * Time.deltaTime;
                if (currentEnergy > maxEnergy)
                {
                    currentEnergy = maxEnergy;
                }
            }
        }

        // Create gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Jump function
        if (Input.GetButtonDown("Jump") && isGrounded && !crouch)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    void ToggleCrouch()
    {
        if (crouch)
        {
            // Stand up
            crouch = false;
            animator.SetBool("Crouch", false);
            controller.height = 4f;
            controller.center = Vector3.zero; // Reset center
            cameraTransform.localPosition = standingCameraPosition; // Set camera to standing position
        }
        else
        {
            // Crouch down
            crouch = true;
            animator.SetBool("Crouch", true);
            controller.height = 2f;
            controller.center = new Vector3(0f, -0.9f, 0f); // Adjust center for crouching
            cameraTransform.localPosition = crouchingCameraPosition; // Set camera to crouching position
        }
    }
}
