using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryCanvas; // Reference to the independent Inventory Canvas

    private void Start()
    {
        inventoryCanvas.SetActive(false); // Hide the inventory canvas at start
    }

    public void OpenInventory()
    {
        inventoryCanvas.SetActive(true); // Show the inventory canvas
    }

    public void CloseInventory()
    {
        inventoryCanvas.SetActive(false); // Hide the inventory canvas
    }
}
