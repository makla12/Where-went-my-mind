using UnityEngine;

public class EnemyAttackScript : MonoBehaviour
{
    public int normalDamage = 10; // Regular attack damage
    public int crushDamage = 25;  // Stronger crush attack damage
    public float attackCooldown = 2f; // Cooldown between normal attacks
    private bool canAttack = true;
    private bool hasCrashed = false; // Ensures CrushAttack happens only once per jump


     public void PerformAttack()
    {
        if (!canAttack) return;

        canAttack = false;

        // Find player and deal damage
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            if (player.TryGetComponent<PlayerControler>(out var playerHealth))
            {
                playerHealth.TakeDamage(normalDamage);
            }
        }

        // Reset attack after cooldown
        Invoke(nameof(ResetAttack), attackCooldown);
    }

    public void CrushAttack(GameObject player)
    {
        if (hasCrashed) return; // Prevent multiple crushes

        hasCrashed = true; // Mark the crash as done

        if (player.TryGetComponent<PlayerControler>(out var playerHealth))
        {
            playerHealth.TakeDamage(crushDamage);
        }

        Debug.Log("Crush Attack triggered!");
    }

    private void ResetAttack()
    {
        canAttack = true;
    }

    public void ResetCrush() // Allows a new crush attack after crawling
    {
        hasCrashed = false;
    }
}