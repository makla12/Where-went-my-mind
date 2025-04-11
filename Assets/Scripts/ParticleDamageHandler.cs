using UnityEngine;
using System.Collections.Generic;

public class ParticleCollisionHandler : MonoBehaviour
{
    public int damage;
    public ParticleSystem explosionEffect;

    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    void OnParticleCollision(GameObject other)
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
            int numCollisionEvents = ps.GetCollisionEvents(other, collisionEvents);

            if (numCollisionEvents > 0)
            {
                Vector3 hitPosition = collisionEvents[0].intersection; 
                InstantiateExplosion(hitPosition);
            }
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
        if (explosionEffect != null)
        {
            ParticleSystem explosion = Instantiate(explosionEffect, position, Quaternion.identity);
            explosion.Play();
            Destroy(explosion.gameObject, explosion.main.duration);
        }
        else
        {
            Debug.LogWarning("Explosion effect is not assigned!");
        }
    }
}
