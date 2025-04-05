using UnityEngine;

public class HelthUp : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 100 * Time.deltaTime, 0);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                if (player.TryGetComponent<PlayerControler>(out var playerHealth))
                {
                    if (playerHealth.health < playerHealth.maxHealth)
                    {
                        playerHealth.Heal(25);
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
