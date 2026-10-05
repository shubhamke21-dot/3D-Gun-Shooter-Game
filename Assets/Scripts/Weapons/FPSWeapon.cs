using UnityEngine;

public class FPSWeapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    public string weaponName = "Rifle";
    public float damage = 25f;
    public float range = 100f;
    public float fireRate = 0.1f;
    public int magazineSize = 30;
    public int reserveAmmo = 120;
    public float reloadTime = 1.5f;
    public float recoilAmount = 0.08f;

    [Header("References")]
    public Camera weaponCamera;
    public ParticleSystem muzzleFlash;
    public Animator weaponAnimator;

    private int currentAmmo;
    private bool isReloading;
    private float nextFireTime;
    private Vector3 originalLocalPosition;

    private void Start()
    {
        currentAmmo = magazineSize;
        if (weaponCamera == null)
            weaponCamera = Camera.main;

        originalLocalPosition = transform.localPosition;
    }

    private void Update()
    {
        if (isReloading) return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            if (currentAmmo > 0)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
            else
            {
                Reload();
            }
        }
    }

    public void Shoot()
    {
        currentAmmo--;

        if (muzzleFlash != null)
            muzzleFlash.Play();

        if (weaponCamera != null)
        {
            Ray ray = new Ray(weaponCamera.transform.position, weaponCamera.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, range))
            {
                EnemyAI enemy = hit.collider.GetComponent<EnemyAI>();
                if (enemy != null)
                    enemy.TakeDamage((int)damage);
            }
        }

        Vector3 recoil = new Vector3(0f, 0f, -recoilAmount);
        transform.localPosition = originalLocalPosition + recoil;
        Invoke(nameof(ResetRecoil), 0.04f);
    }

    private void ResetRecoil()
    {
        transform.localPosition = originalLocalPosition;
    }

    public void Reload()
    {
        if (isReloading || currentAmmo == magazineSize || reserveAmmo <= 0)
            return;

        isReloading = true;
        if (weaponAnimator != null)
            weaponAnimator.SetTrigger("Reload");

        Invoke(nameof(FinishReload), reloadTime);
    }

    private void FinishReload()
    {
        int neededAmmo = magazineSize - currentAmmo;
        int ammoToLoad = Mathf.Min(neededAmmo, reserveAmmo);
        currentAmmo += ammoToLoad;
        reserveAmmo -= ammoToLoad;
        isReloading = false;
    }

    public int CurrentAmmo => currentAmmo;
    public int ReserveAmmo => reserveAmmo;
    public bool IsReloading => isReloading;
}
