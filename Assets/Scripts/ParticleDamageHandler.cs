using UnityEngine;

public class ParticleCollisionHandler : MonoBehaviour
{
    public int damage;
    public ParticleSystem explosionEffect; 

    void OnParticleCollision(GameObject other)
    {
        InstantiateExplosion(other.transform.position);
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

    void InstantiateExplosion(Vector3 position)
    {
        Debug.LogWarning("Explosion!");
        if (explosionEffect != null)
        {
            ParticleSystem explosion = Instantiate(explosionEffect, position, Quaternion.identity);
            explosion.Play();
        }
        else
        {
            Debug.LogWarning("Explosion effect is not assigned!");
        }
    }
}
