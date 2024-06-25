using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
            if (playerInventory != null)
            {
                if (Input.GetKeyDown(KeyCode.F))
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
}
