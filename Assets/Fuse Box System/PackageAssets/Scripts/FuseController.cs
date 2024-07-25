using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Progress;

public class FuseController : MonoBehaviour
{
    #region Fuse Booleans
    [Header("Fuse Booleans")]
    [SerializeField] private bool fuse1Bool;
    [SerializeField] private bool fuse2Bool;
    [SerializeField] private bool fuse3Bool;
    [SerializeField] private bool fuse4Bool;

    [SerializeField] private bool powerOn;
    #endregion

    #region Fuse Main Objects
    [Header("Fuse Main Objects")]
    [SerializeField] private GameObject fuseObject1;
    [SerializeField] private GameObject fuseObject2;
    [SerializeField] private GameObject fuseObject3;
    [SerializeField] private GameObject fuseObject4;
    #endregion

    #region Fuse Lights
    [Header("Fuse Lights")]
    [SerializeField] private GameObject light1;
    [SerializeField] private GameObject light2;
    [SerializeField] private GameObject light3;
    [SerializeField] private GameObject light4;
    #endregion

    #region Materials
    [Header("Materials")]
    [SerializeField] private Material greenButton;
    [SerializeField] private Material redButton;
    #endregion

    #region Events
    [Header("Events")]
    [SerializeField] private UnityEvent onPowerUp;
    #endregion

    public GameObject[] items;
    public GameObject[] waypoints;

    void Start()
    {
        #region Set Light Colour/Fuse Objects, if any fuses booleans are currently set
        if (fuse1Bool)
        {
            light1.GetComponent<Renderer>().material = greenButton;
            fuseObject1.SetActive(true);
        }

        if (fuse2Bool)
        {
            light2.GetComponent<Renderer>().material = greenButton;
            fuseObject2.SetActive(true);
        }

        if (fuse3Bool)
        {
            light3.GetComponent<Renderer>().material = greenButton;
            fuseObject3.SetActive(true);
        }

        if (fuse4Bool)
        {
            light4.GetComponent<Renderer>().material = greenButton;
            fuseObject4.SetActive(true);
        }
        #endregion


        items = GameObject.FindGameObjectsWithTag("Item");
        waypoints = GameObject.FindGameObjectsWithTag("RandomNote");
        List<GameObject> waypointList = new List<GameObject>(waypoints);

        foreach (GameObject item in items)
        {
            int randomWaypoint = UnityEngine.Random.Range(0, waypointList.Count);
            GameObject waypoint = waypointList[randomWaypoint];
            Vector3 waypointPosition = waypoint.transform.position;
            item.transform.position = waypointPosition;
            waypointList.RemoveAt(randomWaypoint);
        }
    }

    void PoweredUp()
    {
        onPowerUp.Invoke();
    }

    public void CheckFuseBox()
    {
        #region No Fuses Check
        if (!fuse1Bool && !fuse2Bool && !fuse3Bool && !fuse4Bool && !powerOn)
        {
            AudioManager_Fuse.instance.Play("ZapSFX");
        }
        #endregion

        if (!fuse1Bool)
        {
            fuseObject1.SetActive(true);
            light1.GetComponent<Renderer>().material = greenButton;
            fuse1Bool = true;
            AudioManager_Fuse.instance.Play("ZapSFX");
            FusesEngaged();
            Debug.Log("Fuses 1 are on!");
            return;
        }

        if (!fuse2Bool)
        {
            fuseObject2.SetActive(true);
            light2.GetComponent<Renderer>().material = greenButton;
            fuse2Bool = true;
            AudioManager_Fuse.instance.Play("ZapSFX");
            FusesEngaged();
            Debug.Log("Fuses 2 are on!");
            return;
        }

        if (!fuse3Bool)
        {
            fuseObject3.SetActive(true);
            light3.GetComponent<Renderer>().material = greenButton;
            fuse3Bool = true;
            AudioManager_Fuse.instance.Play("ZapSFX");
            FusesEngaged();
            Debug.Log("Fuses 3 are on!");
            return;
        }

        if (!fuse4Bool)
        {
            fuseObject4.SetActive(true);
            light4.GetComponent<Renderer>().material = greenButton;
            fuse4Bool = true;
            AudioManager_Fuse.instance.Play("ZapSFX");
            FusesEngaged();
            Debug.Log("Fuses 4 are on!");
        }
    }

    void FusesEngaged()
    {
        #region FusesEngaged Section
        if (fuse1Bool && fuse2Bool && fuse3Bool && fuse4Bool)
        {
            powerOn = true;
            GetComponent<AudioSource>().Play();
            PoweredUp();
            Debug.Log("Fuses are on!");
        }
        #endregion
    }
}
