using System.Collections.Generic;
using TMPro;
using UnityEngine;

class Weapon
{
    public GameObject visualPrefab;
    public GameObject weaponPrefab;
    public GameObject weapon;
}

public class InventoryControler : MonoBehaviour
{
    private int currentWeaponIndex = 0;
    public TextMeshProUGUI ammoText;
    [SerializeField] private GameObject weaponStartVisualPrefab;
    [SerializeField] private GameObject weaponStartPrefab;
    [SerializeField] private GameObject weaponPickupPrefab;
    public int maxWeapons = 2;
    private List<Weapon> weapons = new();

    public int GetWeaponsCount()
    {
        return weapons.Count;
    }

    public void AddWeapon(GameObject visualPrefab, GameObject weaponPrefab)
    {
        if (weapons.Count >= maxWeapons)
        {
            Debug.Log("Inventory is full. Cannot add more weapons.");
            return;
        }
        Weapon newWeapon = new()
        {
            visualPrefab = visualPrefab,
            weaponPrefab = weaponPrefab,
            weapon = Instantiate(weaponPrefab, Camera.main.transform)
        };
        GunSystem gunSystem = newWeapon.weapon.GetComponent<GunSystem>();
        gunSystem.fpsCam = Camera.main;
        gunSystem.cameraShake = Camera.main.GetComponent<CameraShake>();
        gunSystem.MuzzleFlash = Camera.main.GetComponentInChildren<ParticleSystem>();
        gunSystem.text = ammoText;
        weapons.Add(newWeapon);
        weapons[currentWeaponIndex].weapon.SetActive(false);
        currentWeaponIndex = weapons.Count - 1;
        weapons[currentWeaponIndex].weapon.SetActive(true);
    }

    public void ReplaceWeapon(GameObject newVisualPrefab, GameObject newWeaponPrefab, Vector3 oldWeaponPosition)
    {
        GameObject gunPickUp = Instantiate(weaponPickupPrefab, oldWeaponPosition, Quaternion.identity);
        gunPickUp.GetComponent<GunPickUp>().gunVisualPrefab = weapons[currentWeaponIndex].visualPrefab;
        gunPickUp.GetComponent<GunPickUp>().gunPrefab = weapons[currentWeaponIndex].weaponPrefab;
        Destroy(weapons[currentWeaponIndex].weapon);
        Weapon newWeapon = new()
        {
            visualPrefab = newVisualPrefab,
            weaponPrefab = newWeaponPrefab,
            weapon = Instantiate(newWeaponPrefab, Camera.main.transform)
        };
        GunSystem gunSystem = newWeapon.weapon.GetComponent<GunSystem>();
        gunSystem.fpsCam = Camera.main;
        gunSystem.cameraShake = Camera.main.GetComponent<CameraShake>();
        gunSystem.MuzzleFlash = Camera.main.GetComponentInChildren<ParticleSystem>();
        gunSystem.text = ammoText;
        weapons[currentWeaponIndex] = newWeapon;
    }

    public void SwitchWeapon(int weaponIndex)
    {
        if (weaponIndex < 0 || weaponIndex >= weapons.Count)
        {
            Debug.Log("Invalid weapon index. Cannot switch to weapon.");
            return;
        }
        weapons[currentWeaponIndex].weapon.SetActive(false);
        currentWeaponIndex = weaponIndex;
        weapons[currentWeaponIndex].weapon.SetActive(true);
    }
    

    void Start()
    {
        AddWeapon(weaponStartVisualPrefab, weaponStartPrefab);
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Alpha1)){
            SwitchWeapon(0);
        }
        if(Input.GetKey(KeyCode.Alpha2)){
            SwitchWeapon(1);
        }
    }
}
