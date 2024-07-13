using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Toolbar : MonoBehaviour
{
    public Image[] slots;
    public Color selectedColor = Color.yellow;
    public Color defaultColor = Color.white;
    private int selectedIndex = 0;
    public List<Item> inventory = new List<Item>();

    void Start()
    {
        UpdateSelection();
    }

    void Update()
    {
        HandleInput();
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
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].sprite == null)
            {
                slots[i].sprite = item.itemIcon;
                slots[i].enabled = true;
                inventory.Add(item);
                break;
            }
        }
    }

    public void RemoveItem(int index)
    {
        if (index < inventory.Count)
        {
            slots[index].sprite = null;
            slots[index].enabled = false;
            inventory.RemoveAt(index);
            UpdateSelection();
        }
    }

    public int GetSelectedIndex()
    {
        return selectedIndex;
    }

    public Item GetItem(int index)
    {
        if (index < inventory.Count)
        {
            return inventory[index];
        }
        return null;
    }

    void UseSelectedItem()
    {
        if (selectedIndex < inventory.Count)
        {
            Item selectedItem = inventory[selectedIndex];
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
                if (energyDrink != null)
                {
                    energyDrink.Use();
                }
            }
            if (selectedItem.isFuse)
            {
                // Interact with fuse box if needed
                FuseController fuseController = FindObjectOfType<FuseController>();
                if (fuseController != null)
                {
                    fuseController.UseFuseFromToolbar(this);
                }
            }
        }
    }
}
