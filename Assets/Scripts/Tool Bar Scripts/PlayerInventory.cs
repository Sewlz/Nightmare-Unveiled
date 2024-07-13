using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<Item> inventory = new List<Item>();
    public Toolbar toolbar;
    public Flashflight flashlight;

    public bool PickUpItem(Item item, string noteText)
    {
        if (inventory.Count < toolbar.slots.Length)
        {
            inventory.Add(item);
            toolbar.AddItemToSlot(item);
            if (item.isFlashlight)
            {
                flashlight = GetComponentInChildren<Flashflight>();
                if (flashlight != null)
                {
                    flashlight.flashLight.SetActive(false);
                }
            }
            return true;
        }
        else
        {
            Debug.Log("Inventory is full.");
            return false;
        }
    }

    public bool HasFuse()
    {
        return inventory.Exists(item => item.isFuse);
    }

    public void RemoveFuse()
    {
        Item fuseItem = inventory.Find(item => item.isFuse);
        if (fuseItem != null)
        {
            inventory.Remove(fuseItem);
            // Update toolbar UI if necessary
        }
    }

    public Item GetFuse()
    {
        return inventory.Find(item => item.isFuse);
    }
}
