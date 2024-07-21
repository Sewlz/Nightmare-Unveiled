using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class Toolbar : MonoBehaviour
{
    public Image[] slots;
    public TMP_Text[] slotsQuantity;
    public Color selectedColor = Color.yellow;
    public Color defaultColor = Color.white;
    private int selectedIndex = 0;
    private PlayerInventory playerInventory;

    void Start()
    {
        UpdateSelection();
        playerInventory = FindObjectOfType<PlayerInventory>(); 
        LoadToolbar();
    }

    void Update()
    {
        HandleInput();
         
        for (int i = 0; i < slotsQuantity.Length; i++)
        {
            if (slotsQuantity[i].text == "0")
            {
                RemoveItemFromSlot(i);
                break;  
            }
        } 
    }

    void HandleInput()
    {
        // Select slot with number keys
        for (int i = 0; i < slots.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                selectedIndex = i;
                UpdateSelection();
                break;
            }
        }

        // Select slot with mouse scroll
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            selectedIndex = (selectedIndex - 1 + slots.Length) % slots.Length;
            UpdateSelection();
        }
        else if (scroll < 0f)
        {
            selectedIndex = (selectedIndex + 1) % slots.Length;
            UpdateSelection();
        }

        // Use selected item with 'E' key
        if (Input.GetKeyDown(KeyCode.E))
        {
            UseSelectedItem();
        }
    }

    void UpdateSelection()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i == selectedIndex)
            {
                slots[i].color = selectedColor;
            }
            else
            {
                slots[i].color = defaultColor;
            }
        }
    }

    public void AddItemToSlot(Item item)
    { 
        if (item.isMultiple)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].sprite != null && slots[i].sprite == item.itemIcon)
                {
                    UpdateQuantity(i);
                    item.isAdded = true;
                    break;
                }
            }
        }
        if (!item.isAdded)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].sprite == null)
                {
                    // Debug.Log("Adding item to slot: " + i);
                    slots[i].sprite = item.itemIcon;
                    slots[i].enabled = true;
                    slotsQuantity[i].text = "1";
                    break;
                }
            }
        }
    }
    public void UpdateQuantity(int index)
    {
        slotsQuantity[index].text = (int.Parse(slotsQuantity[index].text) + 1).ToString();
    }
    public int GetSelectedIndex()
    {
        return selectedIndex;
    }
    public void RemoveItemFromSlot(int index){
        PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
        playerInventory.removeFromInventory(index); 
        // Debug.Log("Removing item from slot: " + index);
        slots[index].sprite = default;
        slotsQuantity[index].text = "0";
        UpdateToolbar();
    }
    void UpdateToolbar()
    {
       for (int i = 0; i < slots.Length - 1; i++)
        {
            if (slots[i].sprite == null && slots[i + 1].sprite != null)
            {
                slots[i].sprite = slots[i + 1].sprite;
                slotsQuantity[i].text = slotsQuantity[i + 1].text;

                slots[i + 1].sprite = null;
                slotsQuantity[i + 1].text = "0";
            }
        }
    }

    void UseSelectedItem()
    {
    if (selectedIndex < playerInventory.inventory.Count)
        {
            Item selectedItem = playerInventory.inventory[selectedIndex];
            if (selectedItem.isFlashlight)
            {
                Flashflight flashlight = FindObjectOfType<Flashflight>();
                if (flashlight != null)
                {
                    flashlight.ToggleFlashlight();
                }
            }
            if (selectedItem.isNote)
            {
                NoteController note = FindObjectOfType<NoteController>();
                if (note != null)
                {
                    note.ShowNote(selectedItem);
                }
            }
            if (selectedItem.isEnDrink)
            {
                EnergyDrink energyDrink = FindObjectOfType<EnergyDrink>();
                if (energyDrink != null) {
                    if(int.Parse(slotsQuantity[selectedIndex].text) > 0){
                        energyDrink.Use();
                        slotsQuantity[selectedIndex].text = (int.Parse(slotsQuantity[selectedIndex].text) - 1).ToString();
                    }else{
                        RemoveItemFromSlot(selectedIndex);
                    }
                }
            }
            if (selectedItem.isFuse)
            {
                FuseController fuseController = FindObjectOfType<FuseController>();
                if (fuseController != null)
                {
                    if(int.Parse(slotsQuantity[selectedIndex].text) > 0){
                        fuseController.CheckFuseBox();
                        slotsQuantity[selectedIndex].text = (int.Parse(slotsQuantity[selectedIndex].text) - 1).ToString();
                    }else{
                        RemoveItemFromSlot(selectedIndex);
                    }
                }
            }
            if (selectedItem.isRemote)
            {          
                UseRemoteConrol useRemoteConrol = FindObjectOfType<UseRemoteConrol>();
                if(useRemoteConrol != null)
                {
                    if(int.Parse(slotsQuantity[selectedIndex].text) > 0){
                        useRemoteConrol.ShowDistance();
                    }
                }
            }
        }
    }
    void LoadToolbar()
    {
        for (int i = 0; i < playerInventory.inventory.Count && i < slots.Length; i++)
        {
            Item item = playerInventory.inventory[i];
            if (item != null)
            {
                slots[i].sprite = item.itemIcon;
                slots[i].enabled = true;
                slotsQuantity[i].text = item.quantity.ToString();
            }
        }
    }
}
