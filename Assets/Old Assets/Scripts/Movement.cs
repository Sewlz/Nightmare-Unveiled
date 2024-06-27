using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float sprintspeed = 25f;
    public float gravity = -19.62f;
    Vector3 velocity;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    public float Jumpheight = 3;

    // Energy variables
    public Slider energySlider;
    public float maxEnergy = 100f;
    public float currentEnergy;
    public float energyDecreaseRate = 10f;
    public float energyIncreaseRate = 5f;

    //Animation
    private Animator animator;

    void Start()
    {
        // Initialize energy
        currentEnergy = maxEnergy;
        energySlider.maxValue = maxEnergy;
        energySlider.value = currentEnergy;

        //Animation
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Update energy slider
        energySlider.value = currentEnergy;

        // CHECKS THE PLAYER IS GROUNDED TO RESET GRAVITY
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // MOVEMENT
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        bool isMoving = move.sqrMagnitude > 0; // Check if the player is moving

        // Sprinting logic
        if (Input.GetKey(KeyCode.LeftShift) && currentEnergy > 0 && isMoving)
        {
            //Debug.Log("Sprinting");
            if (move.sqrMagnitude > 1)
            {
                move.Normalize();
            }
            controller.Move(move * sprintspeed * Time.deltaTime);
            currentEnergy -= energyDecreaseRate * Time.deltaTime; // Decrease energy
            if (currentEnergy < 0)
            {
                currentEnergy = 0;
            }
        }
        else
        {
            // Walking logic
            if (move.sqrMagnitude > 1)
            {
                move.Normalize();
            }
            controller.Move(move * speed * Time.deltaTime);
        }

        // Animation
        if(move == Vector3.zero)
        {
            //Idle
            animator.SetFloat("Speed", 0);
        }
        else if(!Input.GetKey(KeyCode.LeftShift))
        {
            //Walk
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

        // CREATES GRAVITY
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // JUMP FUNCTION
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(Jumpheight * -2f * gravity);
        }
    }
}