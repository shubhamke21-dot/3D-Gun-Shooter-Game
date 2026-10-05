using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public FPSWeapon[] weapons;
    public int selectedWeaponIndex = 0;

    private void Start()
    {
        if (weapons == null || weapons.Length == 0)
        {
            weapons = GetComponentsInChildren<FPSWeapon>();
        }

        SelectWeapon(selectedWeaponIndex);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectWeapon(2);
    }

    public void SelectWeapon(int index)
    {
        if (weapons == null || weapons.Length == 0) return;
        if (index < 0 || index >= weapons.Length) return;

        selectedWeaponIndex = index;

        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i] != null)
                weapons[i].gameObject.SetActive(i == selectedWeaponIndex);
        }
    }

    public FPSWeapon GetCurrentWeapon()
    {
        if (weapons == null || weapons.Length == 0) return null;
        return weapons[selectedWeaponIndex];
    }
}
