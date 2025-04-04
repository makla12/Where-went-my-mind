using TMPro;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    private int health = 100;
    private int maxHealth = 100;
    
    public int coins = 0;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private GameObject deathScreen;

    private void Start() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        health = Mathf.Max(0, health);
    }

    public void Heal(int amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, maxHealth); // Prevent overhealing
    }

    private void Die()
    {
        Time.timeScale = 0;
        deathScreen.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        healthText.text = $"Health: {health}";
        if(health <= 0)
        {
            Die();
        }
        
    }
}
