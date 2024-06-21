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
    int randNum;
    public AudioSource walkAudio, growlAudio;
    public string deathScene;

    void Start()
    {
        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("waypoint");
        foreach (GameObject waypoint in waypoints)
        {
            destinations.Add(waypoint.transform);
        }

        walking = true;
        randNum = Random.Range(0, destinations.Count);
        currentDest = destinations[randNum];
        aiAnimation.SetTrigger("walk");
        walkAudio.Play();

        growlAudio.Stop();
    }

    void Update()
    {
        if (chasing)
        {
            ChasePlayer();
        }
        else if (walking)
        {
            WalkToDestination();
        }
        Vector3 direction = (player.position - transform.position).normalized;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, sightDistance))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                Debug.Log("Player detected in the detection zone.");
                StartChasing(player.transform.position);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered detection zone.");
            StartChasing(other.transform.position);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected in the detection zone.");
            StartChasing(other.transform.position);
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

        if (!ai.pathPending && ai.remainingDistance <= catchDistance)
        {
            Debug.Log("Player caught by the monster.");
            player.gameObject.SetActive(false);
            aiAnimation.ResetTrigger("walk");
            aiAnimation.ResetTrigger("idle");
            aiAnimation.ResetTrigger("sprint");
            growlAudio.Stop();
            walkAudio.Stop();
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
        randNum = Random.Range(0, destinations.Count);
        currentDest = destinations[randNum];
        walkAudio.Play();
        growlAudio.Stop();
    }

    IEnumerator chaseRoutine()
    {
        chaseTime = Random.Range(minChaseTime, maxChaseTime);
        yield return new WaitForSeconds(chaseTime);
        walking = true;
        chasing = false;
        randNum = Random.Range(0, destinations.Count);
        currentDest = destinations[randNum];
        growlAudio.Stop();
        walkAudio.Play();
    }

    IEnumerator deathRoutine()
    {
        yield return new WaitForSeconds(jumpscareTime);
        SceneManager.LoadScene(deathScene);
    }

    public void OnHearFootstep(Vector3 footstepPosition)
    {
        Debug.Log("Footstep heard at: " + footstepPosition);
        StartChasing(footstepPosition);
    }

    private void StartChasing(Vector3 targetPosition)
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
            Debug.Log("Started chasing towards: " + targetPosition);
        }
    }
}
