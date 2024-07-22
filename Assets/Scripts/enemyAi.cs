using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class enemyAI : MonoBehaviour
{
    public NavMeshAgent ai;
    public List<Transform> destinations;
    public Animator aiAnimation;
    public float walkSpeed, chaseSpeed, minIdleTime, maxIdleTime,
        idleTime, sightDistance, catchDistance, chaseTime,
        minChaseTime, maxChaseTime, jumpscareTime;
    public bool walking, chasing;
    public Transform player;
    Transform currentDest;
    Vector3 dest;
    public AudioSource walkAudio, growlAudio, chaseAudio;
    public string deathScene;
    public float aiDistance;
    public GameObject hideText, stopHideText;
    public DeathManager deathManager;
    void Start()
    {
        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("waypoint");
        foreach (GameObject waypoint in waypoints)
        {
            destinations.Add(waypoint.transform);
        }

        walking = true;
        currentDest = destinations[Random.Range(0, destinations.Count)];
        aiAnimation.SetTrigger("walk");

        walkAudio.Play();
        growlAudio.Stop();
        deathManager = FindObjectOfType<DeathManager>();
    }

    void Update()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        RaycastHit hit;
        aiDistance = Vector3.Distance(player.position, this.transform.position);
        if (Physics.Raycast(transform.position, direction, out hit, sightDistance))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                Debug.Log("Player detected in the detection zone. RAYCASTHIT");
                StartChasing(player.transform.position);
            }
        }
        if (chasing)
        {
            ChasePlayer();
        }
        else if (walking)
        {
            WalkToDestination();
        }
    }

    void ChasePlayer()
    {
        dest = player.position;
        ai.destination = dest;
        ai.speed = chaseSpeed;
        aiAnimation.ResetTrigger("walk");
        aiAnimation.ResetTrigger("idle");
        aiAnimation.SetTrigger("sprint");
        
        if (!ai.pathPending && aiDistance <= catchDistance)
        {
            Debug.Log("Player caught by the monster.");
            player.gameObject.SetActive(false);
            aiAnimation.ResetTrigger("walk");
            aiAnimation.ResetTrigger("idle");
            hideText.SetActive(false);
            stopHideText.SetActive(false);
            aiAnimation.ResetTrigger("sprint");
            growlAudio.Stop();
            walkAudio.Stop();
            chaseAudio.Stop();
            aiAnimation.SetTrigger("jumpscare");
            StartCoroutine(deathRoutine());
            Debug.Log("Disabled player and stopped all sounds.");
            chasing = false;
        }
    }

    void WalkToDestination()
    {
        dest = currentDest.position;
        ai.destination = dest;
        ai.speed = walkSpeed;
        aiAnimation.ResetTrigger("sprint");
        aiAnimation.ResetTrigger("idle");
        aiAnimation.SetTrigger("walk");
        if (!ai.pathPending && ai.remainingDistance <= ai.stoppingDistance)
        {
            aiAnimation.ResetTrigger("sprint");
            aiAnimation.ResetTrigger("walk");
            aiAnimation.SetTrigger("idle");
            walkAudio.Stop();
            growlAudio.Play();
            StartCoroutine(stayIdle());
            walking = false;
        }
    }

    IEnumerator stayIdle()
    {
        idleTime = Random.Range(minIdleTime, maxIdleTime);
        yield return new WaitForSeconds(idleTime);
        walking = true;
        currentDest = destinations[Random.Range(0, destinations.Count)];
        walkAudio.Play();
        growlAudio.Stop();
    }

    IEnumerator chaseRoutine()
    {
        chaseTime = Random.Range(minChaseTime, maxChaseTime);
        yield return new WaitForSeconds(chaseTime);
        stopChase();
    }

    public void stopChase()
    {
        walking = true;
        chasing = false;
        currentDest = destinations[Random.Range(0, destinations.Count)];
        growlAudio.Stop();
        chaseAudio.Stop();
        walkAudio.Play();
    }

    IEnumerator deathRoutine()
    {
        yield return new WaitForSeconds(jumpscareTime);
        deathManager.TriggerDeath(player);
    }

    public void OnHearFootstep(Vector3 footstepPosition)
    {
        Debug.Log("Footstep heard at: " + footstepPosition);
        StartChasing(footstepPosition);
    }

    public void StartChasing(Vector3 targetPosition)
    {
        if (!chasing)
        {
            chasing = true;
            dest = targetPosition;
            ai.destination = dest;
            ai.speed = chaseSpeed;
            aiAnimation.ResetTrigger("walk");
            aiAnimation.ResetTrigger("idle");
            aiAnimation.SetTrigger("sprint");
            StartCoroutine("chaseRoutine");
            growlAudio.Stop();
            walkAudio.Play();
            chaseAudio.Play();
            Debug.Log("Started chasing towards: " + targetPosition);
        }
    }

    public void ChasingPlayer()
    {
        StartChasing(player.position);
    }
}
