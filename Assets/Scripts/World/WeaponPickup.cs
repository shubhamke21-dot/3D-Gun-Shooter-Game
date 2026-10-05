using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public string itemId = "medkit";
    public string itemName = "Medkit";
    public ItemType type = ItemType.Medkit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InventorySystem inventory = other.GetComponentInChildren<InventorySystem>();
            if (inventory == null)
            {
                inventory = other.GetComponent<InventorySystem>();
            }

            if (inventory != null)
            {
                inventory.AddItem(itemId, itemName, type, 1);
                Destroy(gameObject);
            }
        }
    }
}
