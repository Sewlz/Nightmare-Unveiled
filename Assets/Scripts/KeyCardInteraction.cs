using UnityEngine;

public class KeyCardInteraction : MonoBehaviour
{
    public GameObject keyCard;  // GameObject ð?i di?n cho key card
    public float keyCardDistance = 1.5f;  // Kho?ng cách gi?a player và key card ð? týõng tác

    private bool hasKeyCard = false;  // Bi?n ð? xác ð?nh xem ngý?i chõi ð? nh?n key card hay chýa

    private void Update()
    {
        // Ki?m tra nút ð? nh?n key card (ví d?: nh?n E)
        if (Input.GetKeyDown(KeyCode.E) && !hasKeyCard)
        {
            // Ki?m tra kho?ng cách gi?a player và key card
            float distanceToKeyCard = Vector3.Distance(transform.position, keyCard.transform.position);
            if (distanceToKeyCard <= keyCardDistance)
            {
                // Nh?n key card và c?p nh?t tr?ng thái
                hasKeyCard = true;
                Debug.Log("Key card obtained!");

                // ?n key card sau khi nh?n
                keyCard.SetActive(false);

                // Có th? b? sung hi?u ?ng ho?c âm thanh khi nh?n key card
            }
            else
            {
                Debug.Log("You need to be closer to the key card to pick it up.");
            }
        }
    }

    // Phýõng th?c ð? ki?m tra xem ngý?i chõi ð? nh?n key card hay chýa
    public bool HasKeyCard()
    {
        return hasKeyCard;
    }
}
