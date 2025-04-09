using UnityEngine;

public class GunPickUp : MonoBehaviour
{
    public GameObject gunVisualPrefab; 
    private GameObject gunVisual; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gunVisual = Instantiate(gunVisualPrefab, transform.position, Quaternion.identity);
    }

    void animateGun()
    {
        gunVisual.transform.position = new Vector3(transform.position.x, transform.position.y + Mathf.Sin(Time.time * 2) / 2, transform.position.z);
        // spawnedGun.transform.Rotate(new Vector3(0, 0, 1), 10f * Time.deltaTime);
    }

    void Update()
    {
        animateGun();
        if (Vector3.Distance(gunVisual.transform.position, Camera.main.transform.position) < 1f)
        {
            // Check for input to pick up the gun
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Add the gun to the player's inventory or equip it
                Debug.Log("Gun picked up!");
                Destroy(gunVisual); // Destroy the gun after picking it up
            }
        }

    }
}
