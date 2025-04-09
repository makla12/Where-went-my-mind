using UnityEngine;

public class HelthUp : MonoBehaviour
{
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
                    if (playerHealth.Health < playerHealth.MaxHealth)
                    {
                        playerHealth.Heal(25);
                    }
                }
                
                Destroy(gameObject);
            }
        }
    }
}
