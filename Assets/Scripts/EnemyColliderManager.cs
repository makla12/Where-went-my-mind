using UnityEngine;
using System.Collections;

public class EnemyColliderManager : MonoBehaviour
{
    public Collider normalCollider; // Collider before crashing
    public Collider crashedCollider; // Collider after crashing

    void Start()
    {
        crashedCollider.enabled = false; // Disable crash collider at start
    }

    public void ChangeCollider(float delay)
    {
        StartCoroutine(SwapCollidersAfterDelay(delay));
    }

    private IEnumerator SwapCollidersAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the crash duration

        normalCollider.enabled = false; // Disable original collider
        crashedCollider.enabled = true; // Enable crashed collider
    }
}
