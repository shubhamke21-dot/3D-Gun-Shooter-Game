using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Ammo,
    Medkit,
    Key,
    Resource
}

[System.Serializable]
public class InventoryItem
{
    public string itemId;
    public string itemName;
    public ItemType type;
    public int quantity;
}

public class InventorySystem : MonoBehaviour
{
    private readonly List<InventoryItem> items = new List<InventoryItem>();

    public void AddItem(string itemId, string itemName, ItemType type, int quantity = 1)
    {
        foreach (InventoryItem item in items)
        {
            if (item.itemId == itemId)
            {
                item.quantity += quantity;
                return;
            }
        }

        items.Add(new InventoryItem
        {
            itemId = itemId,
            itemName = itemName,
            type = type,
            quantity = quantity
        });
    }

    public bool HasItem(string itemId)
    {
        foreach (InventoryItem item in items)
        {
            if (item.itemId == itemId && item.quantity > 0)
                return true;
        }
        return false;
    }

    public int GetItemCount(string itemId)
    {
        foreach (InventoryItem item in items)
        {
            if (item.itemId == itemId)
                return item.quantity;
        }
        return 0;
    }

    public bool ConsumeItem(string itemId, int quantity = 1)
    {
        foreach (InventoryItem item in items)
        {
            if (item.itemId == itemId)
            {
                if (item.quantity >= quantity)
                {
                    item.quantity -= quantity;
                    if (item.quantity <= 0)
                        items.Remove(item);
                    return true;
                }
            }
        }
        return false;
    }

    public void UseMedkit(PlayerController player)
    {
        if (player == null) return;

        if (ConsumeItem("medkit", 1))
        {
            player.Heal(35);
            Debug.Log("Used medkit");
        }
    }
}
