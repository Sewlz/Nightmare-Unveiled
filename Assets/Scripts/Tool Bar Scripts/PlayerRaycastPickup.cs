using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerRaycastPickup : MonoBehaviour
{
    public float raycastDistance = 2f;  // Adjust this value based on your game's needs
    public LayerMask itemLayerMask;     // Set this to the layer used for items

    private Camera playerCamera;
    public PlayerInventory playerInventory;
    public Image crossHair;
    public TMP_Text itemDescription;
    public GameObject desCanvas;
    void Start()
    {
        playerCamera = Camera.main;
        // playerInventory = GetComponent<PlayerInventory>();
    }

    void Update()
    {
        changeCrossHairColor();
        if (Input.GetKeyDown(KeyCode.F))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, raycastDistance, itemLayerMask))
            {
                ItemPickup itemPickup = hit.collider.GetComponent<ItemPickup>();
                if (itemPickup != null)
                {
                    itemPickup.PickUp(playerInventory);
                }
            }
        }
    }
    void changeCrossHairColor()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, raycastDistance, itemLayerMask))
        {
            ItemPickup itemPickup = hit.collider.GetComponent<ItemPickup>();
            crossHair.color = Color.yellow;
            desCanvas.SetActive(true);
            itemDescription.text = itemPickup.GetName();
        }
        else
        {
            crossHair.color = Color.white;
            desCanvas.SetActive(false);
            itemDescription.text = "";
        }
    }
    public bool FuseBoxCheck(){
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
         if (Physics.Raycast(ray, out hit, raycastDistance, LayerMask.GetMask("objAnim")))
        {
            return true;
        }else{
            return false;
        }
    }
}