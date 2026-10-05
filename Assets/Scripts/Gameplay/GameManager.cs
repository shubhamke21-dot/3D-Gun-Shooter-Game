using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    public InventorySystem inventory;
    public HUDController hud;

    private void Start()
    {
        if (player == null)
            player = FindObjectOfType<PlayerController>();

        if (inventory == null)
            inventory = FindObjectOfType<InventorySystem>();

        if (hud == null)
            hud = FindObjectOfType<HUDController>();

        if (inventory != null)
            inventory.AddItem("medkit", "Medkit", ItemType.Medkit, 2);
    }

    private void Update()
    {
        if (player != null && Input.GetKeyDown(KeyCode.E))
        {
            if (inventory != null)
                inventory.UseMedkit(player);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
