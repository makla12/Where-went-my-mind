using UnityEngine;

public class enemyControler : MonoBehaviour
{
    void die()
    {
        Destroy(gameObject);
    }

    void Start()
    {
        Invoke(nameof(die), 2);
    }
}
