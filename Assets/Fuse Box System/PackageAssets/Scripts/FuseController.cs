using System;
using UnityEngine;
using UnityEngine.Events;

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
    }

    void PoweredUp()
    {
        onPowerUp?.Invoke();
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
            return;
        }

        if (!fuse2Bool)
        {
            fuseObject2.SetActive(true);
            light2.GetComponent<Renderer>().material = greenButton;
            fuse2Bool = true;
            AudioManager_Fuse.instance.Play("ZapSFX");
            FusesEngaged();
            return;
        }

        if (!fuse3Bool)
        {
            fuseObject3.SetActive(true);
            light3.GetComponent<Renderer>().material = greenButton;
            fuse3Bool = true;
            AudioManager_Fuse.instance.Play("ZapSFX");
            FusesEngaged();
            return;
        }

        if (!fuse4Bool)
        {
            fuseObject4.SetActive(true);
            light4.GetComponent<Renderer>().material = greenButton;
            fuse4Bool = true;
            AudioManager_Fuse.instance.Play("ZapSFX");
            FusesEngaged();
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
        }
        #endregion
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Toolbar toolbar = other.GetComponent<Toolbar>();
            if (Input.GetKeyDown(KeyCode.E))
            {
                UseFuseFromToolbar(toolbar);
            }
        }
    }

    public void UseFuseFromToolbar(Toolbar toolbar)
    {
        int selectedIndex = toolbar.GetSelectedIndex();
        Item selectedItem = toolbar.GetItem(selectedIndex);

        if (selectedItem != null && selectedItem.isFuse)
        {
            toolbar.RemoveItem(selectedIndex);
            CheckFuseBox();
        }
    }
}
