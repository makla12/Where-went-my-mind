using UnityEngine;

public class GamblingMachine : MonoBehaviour
{
    public GameObject pickUp; // Prefab for the visual representation of the gambling machine

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, Camera.main.transform.position) < 3f)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Instantiate(pickUp, new Vector3(transform.position.x + 2, transform.position.y + 1, transform.position.z), transform.rotation);
            }
        }
    }
}