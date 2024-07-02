using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class NoteController : MonoBehaviour
{
    private bool isOpen;
    private Item currentNoteItem;
    [Header("UI Text")]
    [SerializeField] private GameObject noteCanvas;
    [SerializeField] private TMP_Text textArenaUI;
    [Space(10)]
    [SerializeField] private UnityEvent openEvent;
    [SerializeField] private UnityEvent closeEvent;

    public void ShowNote(Item item)
    {
        currentNoteItem = item;
        textArenaUI.text = currentNoteItem.Paragraph;
        noteCanvas.SetActive(true);
        /*openEvent.Invoke();*/
        isOpen = true;
    }

    public void DisableNote()
    {
        noteCanvas.SetActive(false);
        closeEvent.Invoke();
        isOpen = false;
    }

    private void Update()
    {
        if (isOpen)
        {
            if (Input.GetMouseButtonDown(0))
            {
                DisableNote();
            }
        }
    }
}
