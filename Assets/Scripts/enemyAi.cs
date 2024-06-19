using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class emenyAI : MonoBehaviour
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
    public Vector3 rayCastOffSet;
    public string deathScene;

    void Start()
    {
        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("waypoint");
        foreach (GameObject waypoint in waypoints)
        {
            destinations.Add(waypoint.transform);
        }

        //walkAudio = GetComponent<AudioSource>();

        walking = true;
        randNum = Random.Range(0, destinations.Count);
        currentDest = destinations[randNum];
        aiAnimation.SetTrigger("walk");
        walkAudio.Play();

        //growlAudio = GetComponents<AudioSource>()[1];
        growlAudio.Stop();
    }

    void Update()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        RaycastHit hit;
        if(Physics.Raycast(transform.position, direction, out hit, sightDistance))
        {
            if(hit.collider.gameObject.tag == "Player")
            {
                Debug.Log("Phát hiện người chơi.");
                walking = false;
                StopCoroutine("stayIdle");
                StopCoroutine("chaseRoutine");
                StartCoroutine("chaseRoutine");
                growlAudio.Stop();
                walkAudio.Play();
                chasing = true;
            }
        }

        if(chasing)
        {
            dest = player.position;
            ai.destination = dest;
            ai.speed = chaseSpeed;
            aiAnimation.ResetTrigger("walk");
            aiAnimation.ResetTrigger("idle");
            aiAnimation.SetTrigger("sprint");

            if (!ai.pathPending && ai.remainingDistance <= catchDistance)
            {
                Debug.Log("Người chơi bị bắt bởi quái vật.");
                player.gameObject.SetActive(false);
                aiAnimation.ResetTrigger("walk");
                aiAnimation.ResetTrigger("idle");
                aiAnimation.ResetTrigger("sprint");
                growlAudio.Stop();
                walkAudio.Stop();
                aiAnimation.SetTrigger("jumpscare");
                StartCoroutine(deathRoutine());
                Debug.Log("Vô hiệu hóa người chơi và dừng âm thanh.");
                chasing = false;
            }
        }

        if (walking == true)
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
}
