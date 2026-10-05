using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public PlayerController player;
    public WeaponSwitcher weaponSwitcher;
    public Text healthText;
    public Text ammoText;
    public Text inventoryText;

    private void Start()
    {
        if (player == null)
            player = FindObjectOfType<PlayerController>();

        if (weaponSwitcher == null)
            weaponSwitcher = FindObjectOfType<WeaponSwitcher>();
    }

    private void Update()
    {
        if (player != null && healthText != null)
            healthText.text = "Health: " + player.currentHealth + " / " + player.maxHealth;

        if (weaponSwitcher != null && weaponSwitcher.GetCurrentWeapon() != null)
        {
            FPSWeapon currentWeapon = weaponSwitcher.GetCurrentWeapon();
            if (ammoText != null)
                ammoText.text = "Ammo: " + currentWeapon.CurrentAmmo + " / " + currentWeapon.ReserveAmmo;
        }

        if (inventoryText != null)
            inventoryText.text = "Inventory: Medkits";
    }
}
