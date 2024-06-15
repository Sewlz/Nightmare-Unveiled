using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import TextMeshPro namespace
using UnityEngine.UI;

public class NoteUI : MonoBehaviour
{
    public Image NoteImage;
    public AudioSource pickup;
    public AudioSource putdown;
    public GameObject Player;
    public Camera PlayerCam;
    public TextMeshProUGUI noteCountText; // TextMeshProUGUI to display the count of notes
    public static int noteCount = 0; // Static variable to keep track of note count across instances
    public static int totalNotesInScene = 0; // Static variable to keep track of total note count in the scene
    GameObject Cover;

    void Start()
    {
        NoteImage.enabled = false;
        UpdateNoteCountText(); // Initialize note count text
        if (totalNotesInScene == 0) // Check if totalNotesInScene has not been set yet
        {
            totalNotesInScene = GameObject.FindGameObjectsWithTag("Note").Length;
        }
    }

    public void ShowNoteImage()
    {
        pickup.Play();
        foreach (GameObject GateCover in GameObject.FindGameObjectsWithTag("Gates"))
        {
            Cover = GateCover;
            Cover.GetComponent<WallTrigger>().EscapeChance += .1f;
        }
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        noteCount++; // Increment the note count
        UpdateNoteCountText(); // Update the note count text
        Invoke("DestroyAfterPlay", .8f);
    }

    public void HideNoteImage()
    {
        //putdown.Play();
        //NoteImage.enabled = false;
        //Player.GetComponent<Movement>().enabled = true;
        //Player.GetComponent<Footsteps>().enabled = true;
        //PlayerCam.GetComponent<Lookscript>().enabled = true;
        //PlayerCam.GetComponent<AnimationTriggers>().enabled = true;
        //Destroy(gameObject);
    }

    public void DestroyAfterPlay()
    {
        Destroy(gameObject);
    }

    // Update the note count text
    void UpdateNoteCountText()
    {
        noteCountText.text = "Notes Collected: " + noteCount.ToString() + " / " + totalNotesInScene.ToString();
    }

    // Method to reset note count
    public static void ResetNoteCount()
    {
        noteCount = 0;
        // Ensure the text is updated immediately after resetting the count
        var noteUIInstances = FindObjectsOfType<NoteUI>();
        foreach (var noteUI in noteUIInstances)
        {
            noteUI.UpdateNoteCountText();
        }
    }

    // Draw gizmos over notes
    public float GizmosRadius = 1.0f;

    public virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, GizmosRadius);
    }
}

