using UnityEngine;

public class ParticleCollisionHandler : MonoBehaviour
{
    public int damage = 1; 

    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyControler enemy = other.GetComponent<EnemyControler>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Debug.Log("hit: " + other.name);
            }
        }
    }
}
