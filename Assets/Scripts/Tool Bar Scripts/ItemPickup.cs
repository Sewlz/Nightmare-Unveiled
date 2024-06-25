using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
            if (playerInventory != null)
            {
                bool pickedUp = playerInventory.PickUpItem(item);
                if (pickedUp)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
