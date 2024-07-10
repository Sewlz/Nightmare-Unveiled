using UnityEngine;

public class DoorManager : MonoBehaviour // This script should be on the Door Trigger
{
    public GameObject CursorHover; // The hover cursor that should show when the player is looking at the door
    public Animation Door;
    public AudioSource DoorOpenSound;
    public AudioSource DoorCloseSound;

    private bool isDoorOpen = false; // To track if the door is open or closed

    private void OnMouseOver() // Activates when the player looks at the door
    {
        if (PlayerCasting.DistanceFromTarget < 4) // If the player IS close enough to the door..
        {
            CursorHover.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E)) // If the player presses E..
            {
                if (!isDoorOpen)
                {
                    OpenDoor();
                }
                else
                {
                    CloseDoor();
                }
            }
        }
        else // If the player is NOT close enough to the door
        {
            CursorHover.SetActive(false);
        }
    }

    private void OnMouseExit() // Activates when the player looks away from the door
    {
        CursorHover.SetActive(false);
    }

    public void OpenDoor()
    {
        GetComponent<BoxCollider>().enabled = false; // Turns off the player's ability to open the door again even though it's already open
        Door.Play("DoorOpen"); // Play the door open animation
        DoorOpenSound.Play(); // Play the door open sound
        isDoorOpen = true; // Mark the door as open
        GetComponent<BoxCollider>().enabled = true; // Re-enable the collider after animation
    }

    public void CloseDoor()
    {
        GetComponent<BoxCollider>().enabled = false; // Turns off the player's ability to close the door again even though it's already closed
        Door.Play("DoorClose"); // Play the door close animation
        DoorCloseSound.Play(); // Play the door close sound
        isDoorOpen = false; // Mark the door as closed
        GetComponent<BoxCollider>().enabled = true; // Re-enable the collider after animation
    }
}
