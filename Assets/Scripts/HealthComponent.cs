using UnityEngine;
using System;

public class HealthComponent : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    // Event for when health changes
    public event Action<float, float> OnHealthChanged; // (currentHealth, maxHealth)
    public event Action OnDeath; // Called when health reaches zero

    void Start()
    {
        currentHealth = maxHealth; // Initialize health
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Prevent going below 0

        // Call event to update UI or effects
        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Prevent overhealing

        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    private void Die()
    {
        OnDeath?.Invoke(); // Notify other scripts that this object has died
        Destroy(gameObject); // Remove object from the scene
    }
}
