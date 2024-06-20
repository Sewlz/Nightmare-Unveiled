using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    public float InteractDistance = 3f;
    public LayerMask interactLayer;

    public Image InteractIcon;

    public bool Interacting;

    private bool canInteract = true; // Added to prevent multiple interactions per frame

    // Start is called before the first frame update
    void Start()
    {
        if (InteractIcon != null)
        {
            InteractIcon.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, InteractDistance, interactLayer))
        {
            if (!Interacting && canInteract) // Only allow interaction if not already interacting and canInteract is true
            {
                if (InteractIcon != null)
                {
                    InteractIcon.enabled = true;
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (hit.collider.tag == "Note")
                    {
                        NoteUI noteUI = hit.collider.GetComponent<NoteUI>();
                        if (noteUI.NoteImage.enabled == false)
                        {
                            noteUI.ShowNoteImage();
                        }
                        else
                        {
                            noteUI.HideNoteImage();
                        }
                        canInteract = false; // Set canInteract to false to prevent further interaction this frame
                    }
                }
            }
        }
        else
        {
            if (InteractIcon != null)
            {
                InteractIcon.enabled = false;
            }
        }

        // Reset canInteract at the end of the frame to allow interaction again next frame
        canInteract = true;
    }
}
