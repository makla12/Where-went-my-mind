using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControler : MonoBehaviour
{
    public int health = 10;
    private List<Renderer> enemyRenderers = new List<Renderer>();
    private Dictionary<Renderer, Color> originalColors = new Dictionary<Renderer, Color>();

    private void Start()
    {
        // Find all Renderer components in the enemy's hierarchy
        enemyRenderers.AddRange(GetComponentsInChildren<Renderer>());
        
        // Store the original color of each Renderer
        foreach (var renderer in enemyRenderers)
        {
            originalColors[renderer] = renderer.material.color;
        }
    }

    private IEnumerator FlashRed()
    {
        // Change the color of all Renderers to red
        foreach (var renderer in enemyRenderers)
        {
            renderer.material.color = Color.red;
        }

        yield return new WaitForSeconds(0.3f);

        // Revert the color of all Renderers back to their original color
        foreach (var renderer in enemyRenderers)
        {
            renderer.material.color = originalColors[renderer];
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(FlashRed());
        }
    }
}