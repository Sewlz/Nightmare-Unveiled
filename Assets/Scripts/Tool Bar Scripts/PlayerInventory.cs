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
            // for(int i=0; i<inventory.Count; i++){
            //     if(inventory[i].isEnDrink){
            //         Debug.Log("EnergyDink detected.");
            //     }
            // }
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
      public void removeFromInventory(int index){
        if(index < inventory.Count && inventory[index].isEnDrink){
            inventory.RemoveAt(index);
        }   
    }
    public bool EnergyDrinkCheck(){
       for(int i=0; i<inventory.Count; i++){
           if(inventory[i].isEnDrink){
               return true;
           }
       }
       return false;
    }
    //Fuse code
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
