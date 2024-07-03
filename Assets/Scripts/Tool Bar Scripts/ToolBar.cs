using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Toolbar : MonoBehaviour
{
    public Image[] slots;
    public Color selectedColor = Color.yellow;
    public Color defaultColor = Color.white;
    private int selectedIndex = 0;
    private PlayerInventory playerInventory; // Reference to player inventory

    void Start()
    {
        UpdateSelection();
        playerInventory = FindObjectOfType<PlayerInventory>(); // Find the PlayerInventory in the scene
        
    }

    void Update()
    {
        HandleInput();
        Debug.Log(playerInventory.ToString());
    }

    void HandleInput()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                selectedIndex = i;
                UpdateSelection();
                break;
            }
        }
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
/*        if (Input.GetMouseButtonDown(0))
        {
            UseSelectedItem();
        }*/
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
                Debug.Log("Adding item to slot: " + i);
                slots[i].sprite = item.itemIcon;
                slots[i].enabled = true;
                break;
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
                    energyDrink.Use();
                }
                
            }
        }
    }
}
