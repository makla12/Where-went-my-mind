using UnityEngine;

public class GunPickUp : MonoBehaviour
{
    public GameObject gunVisualPrefab; 
    public GameObject gunPrefab;
    private GameObject gunVisual; 
    private InventoryControler inventoryControler; 

    void Start()
    {
        gunVisual = Instantiate(gunVisualPrefab, transform);
        gunVisual.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f); 
        inventoryControler = GameObject.FindWithTag("Player").GetComponent<InventoryControler>();
    }

    void animateGun()
    {
        gunVisual.transform.position = new Vector3(transform.position.x, transform.position.y + Mathf.Sin(Time.time * 2) / 2, transform.position.z);
        gunVisual.transform.Rotate(new Vector3(0, 1, 0), 40f * Time.deltaTime);
    }

    void Update()
    {
        animateGun();
        if (Vector3.Distance(gunVisual.transform.position, Camera.main.transform.position) < 3f)
        {
            // Check for input to pick up the gun
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(inventoryControler.GetWeaponsCount() >= inventoryControler.maxWeapons)
                {
                    inventoryControler.ReplaceWeapon(gunVisualPrefab, gunPrefab, transform.position);
                }
                else
                {
                    inventoryControler.AddWeapon(gunVisualPrefab, gunPrefab);
                }
                Destroy(gameObject); 
            }
        }

    }
}
