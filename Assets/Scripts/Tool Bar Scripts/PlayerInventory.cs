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
}
