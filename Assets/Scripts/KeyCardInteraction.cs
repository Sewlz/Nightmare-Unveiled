using UnityEngine;

public class KeyCardInteraction : MonoBehaviour
{
    public GameObject keyCard;  // GameObject �?i di?n cho key card
    public float keyCardDistance = 1.5f;  // Kho?ng c�ch gi?a player v� key card �? t��ng t�c

    private bool hasKeyCard = false;  // Bi?n �? x�c �?nh xem ng�?i ch�i �? nh?n key card hay ch�a

    private void Update()
    {
        // Ki?m tra n�t �? nh?n key card (v� d?: nh?n E)
        if (Input.GetKeyDown(KeyCode.E) && !hasKeyCard)
        {
            // Ki?m tra kho?ng c�ch gi?a player v� key card
            float distanceToKeyCard = Vector3.Distance(transform.position, keyCard.transform.position);
            if (distanceToKeyCard <= keyCardDistance)
            {
                // Nh?n key card v� c?p nh?t tr?ng th�i
                hasKeyCard = true;
                Debug.Log("Key card obtained!");

                // ?n key card sau khi nh?n
                keyCard.SetActive(false);

                // C� th? b? sung hi?u ?ng ho?c �m thanh khi nh?n key card
            }
            else
            {
                Debug.Log("You need to be closer to the key card to pick it up.");
            }
        }
    }

    // Ph��ng th?c �? ki?m tra xem ng�?i ch�i �? nh?n key card hay ch�a
    public bool HasKeyCard()
    {
        return hasKeyCard;
    }
}
