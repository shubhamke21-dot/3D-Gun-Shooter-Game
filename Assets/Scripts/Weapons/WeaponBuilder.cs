using UnityEngine;

public class WeaponBuilder : MonoBehaviour
{
    public GameObject pistolPrefab;
    public GameObject riflePrefab;
    public GameObject shotgunPrefab;

    private void Start()
    {
        if (pistolPrefab == null)
        {
            pistolPrefab = new GameObject("Pistol");
            pistolPrefab.transform.SetParent(transform);
            pistolPrefab.transform.localPosition = new Vector3(0.45f, -0.35f, 1f);
            FPSWeapon pistolWeapon = pistolPrefab.AddComponent<FPSWeapon>();
            pistolWeapon.weaponName = "Pistol";
            pistolWeapon.damage = 20f;
            pistolWeapon.range = 45f;
            pistolWeapon.fireRate = 0.2f;
            pistolWeapon.magazineSize = 12;
            pistolWeapon.reserveAmmo = 48;
            pistolWeapon.reloadTime = 1.2f;
            pistolWeapon.recoilAmount = 0.05f;
            pistolWeapon.weaponCamera = Camera.main;
            pistolPrefab.SetActive(false);
        }

        if (riflePrefab == null)
        {
            riflePrefab = new GameObject("Rifle");
            riflePrefab.transform.SetParent(transform);
            riflePrefab.transform.localPosition = new Vector3(0.45f, -0.35f, 1f);
            FPSWeapon rifleWeapon = riflePrefab.AddComponent<FPSWeapon>();
            rifleWeapon.weaponName = "Rifle";
            rifleWeapon.damage = 30f;
            rifleWeapon.range = 90f;
            rifleWeapon.fireRate = 0.08f;
            rifleWeapon.magazineSize = 30;
            rifleWeapon.reserveAmmo = 120;
            rifleWeapon.reloadTime = 1.5f;
            rifleWeapon.recoilAmount = 0.06f;
            rifleWeapon.weaponCamera = Camera.main;
            riflePrefab.SetActive(false);
        }

        if (shotgunPrefab == null)
        {
            shotgunPrefab = new GameObject("Shotgun");
            shotgunPrefab.transform.SetParent(transform);
            shotgunPrefab.transform.localPosition = new Vector3(0.45f, -0.35f, 1f);
            FPSWeapon shotgunWeapon = shotgunPrefab.AddComponent<FPSWeapon>();
            shotgunWeapon.weaponName = "Shotgun";
            shotgunWeapon.damage = 45f;
            shotgunWeapon.range = 35f;
            shotgunWeapon.fireRate = 0.8f;
            shotgunWeapon.magazineSize = 6;
            shotgunWeapon.reserveAmmo = 36;
            shotgunWeapon.reloadTime = 2f;
            shotgunWeapon.recoilAmount = 0.1f;
            shotgunWeapon.weaponCamera = Camera.main;
            shotgunPrefab.SetActive(false);
        }

        WeaponSwitcher switcher = GetComponent<WeaponSwitcher>();
        if (switcher != null)
        {
            switcher.weapons = new FPSWeapon[]
            {
                pistolPrefab.GetComponent<FPSWeapon>(),
                riflePrefab.GetComponent<FPSWeapon>(),
                shotgunPrefab.GetComponent<FPSWeapon>()
            };
        }
    }
}
