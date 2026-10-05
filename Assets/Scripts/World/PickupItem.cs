using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public PlayerController player;
    public WeaponManager weaponManager;
    public Text healthText;
    public Text ammoText;
    public Text inventoryText;

    void Start()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerController>();
        }

        if (weaponManager == null)
        {
            weaponManager = FindObjectOfType<WeaponManager>();
        }
    }

    void Update()
    {
        if (player != null)
        {
            healthText.text = "Health: " + player.currentHealth + " / " + player.maxHealth;
        }

        if (weaponManager != null)
        {
            ammoText.text = "Ammo: " + weaponManager.GetCurrentAmmo() + " / " + weaponManager.GetReserveAmmo();
        }

        if (inventoryText != null)
        {
            inventoryText.text = "Inventory: Medkits available";
        }
    }
}
