using UnityEngine;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item itemPrefab;  // Reference to the Item prefab or scriptable object
    [Space(10)]
    [SerializeField][TextArea] public string noteTexts;

    public string GetName()
    {
        return itemPrefab.name;
    }

    public void PickUp(PlayerInventory playerInventory)
    {
        if (noteTexts != null)
        {
            // Create a new instance of Item
            Item newItem = Instantiate(itemPrefab);
            newItem.paragraph = noteTexts;

            // Attempt to pick up the new item
            bool pickedUp = playerInventory.PickUpItem(newItem, noteTexts);
            if (pickedUp)
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            Item newItem = Instantiate(itemPrefab);
            bool pickedUp = playerInventory.PickUpItem(newItem, noteTexts);
            if (pickedUp)
            {
               gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
            if (Input.GetKeyDown(KeyCode.F))
            {
                PickUp(playerInventory);
            }
        }
    }
}
