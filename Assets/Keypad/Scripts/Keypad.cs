using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace NavKeypad
{
    public class Keypad : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField] private UnityEvent onAccessGranted;
        [SerializeField] private UnityEvent onAccessDenied;
        [Header("Combination Code (4 Numbers Max)")]
        public int keypadCombo;
        [Header("Note")]
        public List<GameObject> lstNote;
        ItemPickup itemPickup;

        public UnityEvent OnAccessGranted => onAccessGranted;
        public UnityEvent OnAccessDenied => onAccessDenied;

        [Header("Settings")]
        [SerializeField] private string accessGrantedText = "Granted";
        [SerializeField] private string accessDeniedText = "Denied";

        [Header("Visuals")]
        [SerializeField] private float displayResultTime = 1f;
        [Range(0, 5)]
        [SerializeField] private float screenIntensity = 2.5f;
        [Header("Colors")]
        [SerializeField] private Color screenNormalColor = new Color(0.98f, 0.50f, 0.032f, 1f); 
        [SerializeField] private Color screenDeniedColor = new Color(1f, 0f, 0f, 1f);
        [SerializeField] private Color screenGrantedColor = new Color(0f, 0.62f, 0.07f);
        [Header("SoundFx")]
        [SerializeField] private AudioClip buttonClickedSfx;
        [SerializeField] private AudioClip accessDeniedSfx;
        [SerializeField] private AudioClip accessGrantedSfx;
        [Header("Component References")]
        [SerializeField] private Renderer panelMesh;
        [SerializeField] private TMP_Text keypadDisplayText;
        [SerializeField] private AudioSource audioSource;


        private string currentInput;
        private bool displayingResult = false;
        private bool accessWasGranted = false;

        private void Awake()
        {
            string random = "";
            int i = 0;
            int randomNum = 1;

            ClearInput();
            panelMesh.material.SetVector("_EmissionColor", screenNormalColor * screenIntensity);

            GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
            GameObject[] waypoints = GameObject.FindGameObjectsWithTag("RandomNote");
            List<GameObject> waypointList = new List<GameObject>(waypoints);

            foreach (GameObject item in items)
            {
                if(item.GetComponent<ItemPickup>().itemPrefab.isNote){
                    i++;
                    lstNote.Add(item);
                    itemPickup = item.GetComponent<ItemPickup>();
                    randomNum = UnityEngine.Random.Range(1, 10);
                    itemPickup.noteTexts = i + ": " + randomNum;
                    random += randomNum.ToString();

                    int randomWaypoint = UnityEngine.Random.Range(0, waypointList.Count);
                    GameObject waypoint = waypointList[randomWaypoint];
                    Vector3 waypointPosition = waypoint.transform.position;
                    item.transform.position = waypointPosition;
                    waypointList.RemoveAt(randomWaypoint);
                }
            }
            keypadCombo = Int32.Parse(random);
        }



        public void AddInput(string input)
        {
            audioSource.PlayOneShot(buttonClickedSfx);
            if (displayingResult || accessWasGranted) return;
            switch (input)
            {
                case "enter":
                    CheckCombo();
                    break;
                default:
                    if (currentInput != null && currentInput.Length == 4)
                    {
                        return;
                    }
                    currentInput += input;
                    keypadDisplayText.text = currentInput;
                    break;
            }

        }
        public void CheckCombo()
        {
            if (int.TryParse(currentInput, out var currentKombo))
            {
                bool granted = currentKombo == keypadCombo;
                if (!displayingResult)
                {
                    StartCoroutine(DisplayResultRoutine(granted));
                }
            }
            else
            {
                Debug.LogWarning("Couldn't process input for some reason..");
            }

        }

        private IEnumerator DisplayResultRoutine(bool granted)
        {
            displayingResult = true;

            if (granted) AccessGranted();
            else AccessDenied();

            yield return new WaitForSeconds(displayResultTime);
            displayingResult = false;
            if (granted) yield break;
            ClearInput();
            panelMesh.material.SetVector("_EmissionColor", screenNormalColor * screenIntensity);

        }

        private void AccessDenied()
        {
            keypadDisplayText.text = accessDeniedText;
            panelMesh.material.SetVector("_EmissionColor", screenDeniedColor * screenIntensity);
            audioSource.PlayOneShot(accessDeniedSfx);
            onAccessDenied?.Invoke();
        }

        private void ClearInput()
        {
            currentInput = "";
            keypadDisplayText.text = currentInput;
        }

        private void AccessGranted()
        {
            accessWasGranted = true;
            keypadDisplayText.text = accessGrantedText;
            panelMesh.material.SetVector("_EmissionColor", screenGrantedColor * screenIntensity);
            audioSource.PlayOneShot(accessGrantedSfx);
            onAccessGranted?.Invoke();
        }

    }
}