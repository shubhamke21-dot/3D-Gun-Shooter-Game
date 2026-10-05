using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Camera playerCamera;
    public Transform weaponHolder;
    public Gun[] weapons;
    public int selectedWeaponIndex = 0;

    private void Start()
    {
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }

        if (weaponHolder == null)
        {
            GameObject holder = new GameObject("WeaponHolder");
            holder.transform.SetParent(transform);
            holder.transform.localPosition = Vector3.zero;
            weaponHolder = holder.transform;
        }

        InitializeWeapons();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectWeapon(2);

        if (Input.GetButtonDown("Fire1"))
        {
            TryShoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadCurrentWeapon();
        }
    }

    private void InitializeWeapons()
    {
        if (weapons == null || weapons.Length == 0)
        {
            weapons = new Gun[1];
            GameObject defaultWeapon = new GameObject("Pistol");
            defaultWeapon.transform.SetParent(weaponHolder);
            defaultWeapon.transform.localPosition = new Vector3(0.4f, -0.35f, 0.8f);
            defaultWeapon.transform.localRotation = Quaternion.identity;

            Gun gun = defaultWeapon.AddComponent<Gun>();
            gun.playerCamera = playerCamera;
            gun.gunData = new GunData
            {
                id = "pistol",
                weaponName = "Pistol",
                damage = 20,
                ammoPerMag = 12,
                reserveAmmo = 48,
                fireRate = 0.2f,
                range = 30f,
                reloadTime = 1.2f
            };
            gun.Initialize();
            weapons[0] = gun;
        }

        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i] != null)
            {
                weapons[i].gameObject.SetActive(i == selectedWeaponIndex);
            }
        }
    }

    public void SelectWeapon(int index)
    {
        if (weapons == null || index < 0 || index >= weapons.Length) return;

        selectedWeaponIndex = index;

        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i] != null)
            {
                weapons[i].gameObject.SetActive(i == selectedWeaponIndex);
            }
        }
    }

    public void TryShoot()
    {
        if (weapons == null || weapons.Length == 0) return;
        if (weapons[selectedWeaponIndex] != null)
        {
            weapons[selectedWeaponIndex].TryShoot();
        }
    }

    public void ReloadCurrentWeapon()
    {
        if (weapons == null || weapons.Length == 0) return;
        if (weapons[selectedWeaponIndex] != null)
        {
            weapons[selectedWeaponIndex].Reload();
        }
    }

    public int GetCurrentAmmo()
    {
        if (weapons == null || weapons.Length == 0 || weapons[selectedWeaponIndex] == null)
            return 0;
        return weapons[selectedWeaponIndex].CurrentAmmo;
    }

    public int GetReserveAmmo()
    {
        if (weapons == null || weapons.Length == 0 || weapons[selectedWeaponIndex] == null)
            return 0;
        return weapons[selectedWeaponIndex].ReserveAmmo;
    }
}

[System.Serializable]
public class GunData
{
    public string id;
    public string weaponName;
    public int damage;
    public int ammoPerMag;
    public int reserveAmmo;
    public float fireRate;
    public float range;
    public float reloadTime;
}

public class Gun : MonoBehaviour
{
    public GunData gunData;
    public Camera playerCamera;
    public int CurrentAmmo { get; private set; }
    public int ReserveAmmo { get; private set; }
    public bool IsReloading { get; private set; }

    private float nextFireTime;

    public void Initialize()
    {
        if (gunData == null) return;
        CurrentAmmo = gunData.ammoPerMag;
        ReserveAmmo = gunData.reserveAmmo;
    }

    public void TryShoot()
    {
        if (gunData == null) return;
        if (IsReloading) return;
        if (CurrentAmmo <= 0)
        {
            Reload();
            return;
        }

        if (Time.time < nextFireTime) return;

        nextFireTime = Time.time + gunData.fireRate;
        CurrentAmmo--;

        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }

        if (playerCamera != null)
        {
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, gunData.range))
            {
                EnemyAI enemy = hit.collider.GetComponent<EnemyAI>();
                if (enemy != null)
                {
                    enemy.TakeDamage(gunData.damage);
                }
            }
        }
    }

    public void Reload()
    {
        if (gunData == null || IsReloading || ReserveAmmo <= 0 || CurrentAmmo == gunData.ammoPerMag) return;

        StartCoroutine(ReloadRoutine());
    }

    private System.Collections.IEnumerator ReloadRoutine()
    {
        IsReloading = true;
        yield return new WaitForSeconds(gunData.reloadTime);

        int neededAmmo = gunData.ammoPerMag - CurrentAmmo;
        int ammoToLoad = Mathf.Min(neededAmmo, ReserveAmmo);
        CurrentAmmo += ammoToLoad;
        ReserveAmmo -= ammoToLoad;

        IsReloading = false;
    }
}
