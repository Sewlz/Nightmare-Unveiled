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

    public float soundRadius = 10f;
    public Transform player;

    // Reference to the Movement script
    private Movement playerMovement;
    private bool isCollide = false;
    void Start()
    {
        Controller = GetComponent<CharacterController>();
        playerMovement = GetComponent<Movement>();
    }

    void FixedUpdate()
    {
        Step();
        if (Input.GetKey(KeyCode.LeftShift) && isCollide == true)
        {
            Debug.Log("Keydown-activated");
            DetectEnemies();
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<SphereCollider>() != null)
        {
            Debug.Log("Player footstep detected in the detection zone.");
            isCollide = true;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<SphereCollider>() != null)
        {
            Debug.Log("Player footstep detected in the detection zone.");
            isCollide = true;
        }
    }
    void DetectEnemies()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, soundRadius);
        foreach (var hitCollider in hitColliders)
        {
            enemyAI enemyAI = hitCollider.GetComponent<enemyAI>();
            if (enemyAI != null)
            {
                enemyAI.OnHearFootstep(transform.position);
            }
        }
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
