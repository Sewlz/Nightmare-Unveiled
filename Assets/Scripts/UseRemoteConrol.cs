using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UseRemoteConrol : MonoBehaviour
{
    public int numNote = 0;
    public float noteDistance;
    public Transform player;
    public GameObject note;
    public GameObject text;
    public TextMeshProUGUI textDistance;
    public float timeShow = 5f;

    public void ShowDistance()
    {
        GameObject[] notes = GameObject.FindGameObjectsWithTag("Item");
        note = notes[numNote];
        noteDistance = Mathf.Round(Vector3.Distance(player.position, note.transform.position));
        text.SetActive(true);
        textDistance.text = "Note distance: " + noteDistance + "m";
        StartCoroutine(DeactivateTextAfterTime(timeShow));
    }

    IEnumerator DeactivateTextAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        text.SetActive(false);
    }
}
