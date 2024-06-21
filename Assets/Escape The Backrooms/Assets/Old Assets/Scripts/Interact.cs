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
            if (!Interacting)
            {
                if (InteractIcon != null)
                {
                    InteractIcon.enabled = true;
                }

                // Change from Input.GetButtonDown("Interact") to Input.GetKeyDown(KeyCode.E)
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
    }
}

