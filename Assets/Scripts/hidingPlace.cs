using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hidingPlace : MonoBehaviour
{
    public GameObject hideText, stopHideText;
    public GameObject normalPlayer, hidingPlayer;
    public Transform monsterTransform;
    private NoFOVDetect fovScript;
    private Death deathScript;
    private PatrolRandom patrolScript;
    private Movement playerMovementScript;
    private Lookscript playerLookScript;
    bool interactable, hiding;
    public float loseDistance;

    void Start()
    {
        interactable = false;
        hiding = false;

        // L?y các script c?n thi?t t? ð?i tý?ng quái v?t và ngý?i chõi
        fovScript = monsterTransform.GetComponent<NoFOVDetect>();
        deathScript = monsterTransform.GetComponentInParent<Death>();
        patrolScript = monsterTransform.GetComponentInParent<PatrolRandom>();

        // Gi? s? các script ði?u khi?n ðý?c g?n trên ð?i tý?ng ngý?i chõi
        playerMovementScript = normalPlayer.GetComponent<Movement>();
        playerLookScript = normalPlayer.GetComponentInChildren<Lookscript>(); // N?u Lookscript g?n trên camera
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            hideText.SetActive(true);
            interactable = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            hideText.SetActive(false);
            interactable = false;
        }
    }

    void Update()
    {
        if (interactable)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartHiding();
            }
        }

        if (hiding)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                StopHiding();
            }
        }
    }

    void StartHiding()
    {
        hideText.SetActive(false);
        hidingPlayer.SetActive(true);
        float distance = Vector3.Distance(monsterTransform.position, normalPlayer.transform.position);

        if (distance > loseDistance)
        {
            if (deathScript.Caught == false)
            {
                fovScript.enabled = false;
                patrolScript.enabled = false;
            }
        }

        stopHideText.SetActive(true);
        hiding = true;
        normalPlayer.SetActive(false);

        // Vô hi?u hóa các script ði?u khi?n
        if (playerMovementScript != null) playerMovementScript.enabled = false;
        if (playerLookScript != null) playerLookScript.enabled = false;

        interactable = false;
    }

    void StopHiding()
    {
        stopHideText.SetActive(false);
        normalPlayer.SetActive(true);
        hidingPlayer.SetActive(false);

        // Kích ho?t l?i phát hi?n và tu?n tra khi ngý?i chõi không c?n ?n
        fovScript.enabled = true;
        patrolScript.enabled = true;

        hiding = false;

        // Kích ho?t l?i các script ði?u khi?n
        if (playerMovementScript != null) playerMovementScript.enabled = true;
        if (playerLookScript != null) playerLookScript.enabled = true;
    }
}
