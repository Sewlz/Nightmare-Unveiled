using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorMap3 : MonoBehaviour
{
    public Animation Door;
    public AudioSource DoorOpenSound;
    public AudioSource DoorCloseSound;

    public void OpenDoor()
    {
        Door.Play("DoorOpen");
        DoorOpenSound.Play();
    }

    public void CloseDoor()
    {
        Door.Play("DoorClose");
        DoorCloseSound.Play();
    }
}
