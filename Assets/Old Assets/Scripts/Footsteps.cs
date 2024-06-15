using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public CharacterController Controller;
    public AudioSource WalkSound;
    public AudioSource SprintSound;
    public float WalkDistance = 0.65f;
    public float SprintDistance = 0.80f;
    private float TimeBetween;

    // Reference to the Movement script
    private Movement playerMovement;

    void Start()
    {
        Controller = GetComponent<CharacterController>();
        playerMovement = GetComponent<Movement>(); // Get the Movement script
    }

    void FixedUpdate()
    {
        Step();
    }

    void Step()
    {
        bool isMoving = Input.GetButton("Horizontal") || Input.GetButton("Vertical");

        if (!isMoving || playerMovement.currentEnergy <= 0) // Stop sprinting if not moving or out of energy
        {
            if (WalkSound.isPlaying || SprintSound.isPlaying)
            {
                SprintSound.Stop();
                WalkSound.Stop();
            }
        }

        if (isMoving && Input.GetKey(KeyCode.LeftShift) && playerMovement.currentEnergy > 0)
        {
            TimeBetween += Time.deltaTime;
            if (TimeBetween > SprintDistance)
            {
                if (WalkSound.isPlaying)
                {
                    WalkSound.Stop();
                }
                if (!SprintSound.isPlaying)
                {
                    SprintSound.Play();
                    Debug.Log("Sprinting sound playing");
                }
                TimeBetween = 0f;
            }
        }
        else
        {
            TimeBetween += Time.deltaTime;
            if (TimeBetween > WalkDistance)
            {
                if (!WalkSound.isPlaying)
                {
                    WalkSound.Play();
                    Debug.Log("Walking sound playing");
                }
                TimeBetween = 0f;
            }
        }
    }
}
