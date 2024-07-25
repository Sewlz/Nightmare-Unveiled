using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectSpawn : MonoBehaviour
{
    public GameObject monster;
    public Transform player;
    public GameObject[] objs;
    GameObject[] posObjs;
    public DeathManager deathManager;


    void Start()
    {
        objs = GameObject.FindGameObjectsWithTag("RandomObject");
        posObjs = GameObject.FindGameObjectsWithTag("waypointObject");
        List<GameObject> lstObj = new List<GameObject>(posObjs);

        foreach (GameObject obj in objs)
        {
            obj.SetActive(false);
            int randomWaypointIndex = UnityEngine.Random.Range(0, lstObj.Count);
            GameObject waypoint = lstObj[randomWaypointIndex];
            lstObj.RemoveAt(randomWaypointIndex);

            Vector3 incomingPos = waypoint.transform.position;
            Vector3 currentPos = obj.transform.position;
            currentPos.z = incomingPos.z;
            obj.transform.position = currentPos;
        }
        monster.SetActive(false);
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MonsterTrigger"))
        {
            monster.SetActive(true);
            deathManager.FindAllEnemies();
            if (objs.Length > 0)
            {
                foreach (GameObject obj in objs)
                {
                    obj.SetActive(true);
                }
            }
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("objAnim"))
        {
            Animator anim = other.GetComponent<Animator>();
            anim.SetTrigger("move");
        }
    }
}
