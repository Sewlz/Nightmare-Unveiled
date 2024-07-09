using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorMap3 : MonoBehaviour
{
    public Animation Door;
    public AudioSource DoorOpenSound;
    public AudioSource DoorCloseSound;
    private bool isDoorOpen = false;

    public void OpenDoor()
    {
        GetComponent<BoxCollider>().enabled = false;
        Door.Play("DoorOpen");
        DoorOpenSound.Play();
        isDoorOpen = true;
        GetComponent<BoxCollider>().enabled = true;
    }

    public void CloseDoor()
    {
        GetComponent<BoxCollider>().enabled = false;
        Door.Play("DoorClose");
        DoorCloseSound.Play();
        isDoorOpen = false;
        GetComponent<BoxCollider>().enabled = true;
    }
}
