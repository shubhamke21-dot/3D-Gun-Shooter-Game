using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    public InventorySystem inventory;
    public HUDController hud;

    void Start()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerController>();
        }

        if (inventory == null)
        {
            inventory = FindObjectOfType<InventorySystem>();
        }

        if (hud == null)
        {
            hud = FindObjectOfType<HUDController>();
        }

        inventory.AddItem("medkit", "Medkit", ItemType.Medkit, 2);
    }

    void Update()
    {
        if (player != null && Input.GetKeyDown(KeyCode.E))
        {
            inventory.UseMedkit(player);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
